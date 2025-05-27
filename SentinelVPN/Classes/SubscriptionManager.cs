using System;
using System.Data.SQLite;
using System.IO;

namespace VPN.Classes
{
    public static class SubscriptionManager
    {
        private static readonly string dbPath = "users.db";

        public static void Initialize()
        {
            if (!File.Exists(dbPath))
                SQLiteConnection.CreateFile(dbPath);

            using (var conn = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
            {
                conn.Open();
                string query = "CREATE TABLE IF NOT EXISTS subscriptions (email TEXT PRIMARY KEY, expiry_date TEXT)";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void AddSubscriptionDays(string email, int days)
        {
            Initialize();

            using (var conn = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
            {
                conn.Open();

                DateTime newExpiry;
                using (var checkCmd = new SQLiteCommand("SELECT expiry_date FROM subscriptions WHERE email = @Email", conn))
                {
                    checkCmd.Parameters.AddWithValue("@Email", email);
                    var result = checkCmd.ExecuteScalar();
                    if (result != null && DateTime.TryParse(result.ToString(), out var currentExpiry))
                    {
                        newExpiry = currentExpiry > DateTime.Today ? currentExpiry.AddDays(days) : DateTime.Today.AddDays(days);
                    }
                    else
                    {
                        newExpiry = DateTime.Today.AddDays(days);
                    }
                }

                using (var upsertCmd = new SQLiteCommand("REPLACE INTO subscriptions (email, expiry_date) VALUES (@Email, @Expiry)", conn))
                {
                    upsertCmd.Parameters.AddWithValue("@Email", email);
                    upsertCmd.Parameters.AddWithValue("@Expiry", newExpiry.ToString("yyyy-MM-dd"));
                    upsertCmd.ExecuteNonQuery();
                }
            }
        }

        public static int GetRemainingDays(string email)
        {
            Initialize();

            using (var conn = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
            {
                conn.Open();

                using (var cmd = new SQLiteCommand("SELECT expiry_date FROM subscriptions WHERE email = @Email", conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    var result = cmd.ExecuteScalar();

                    if (result != null && DateTime.TryParse(result.ToString(), out var expiryDate))
                    {
                        int daysLeft = (expiryDate - DateTime.Today).Days;
                        return daysLeft < 0 ? 0 : daysLeft;
                    }
                }
            }
            return 0;
        }

        public static DateTime? GetSubscriptionExpiry(string email)
        {
            Initialize();

            using (var conn = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("SELECT expiry_date FROM subscriptions WHERE email = @Email", conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    var result = cmd.ExecuteScalar();
                    if (result != null && DateTime.TryParse(result.ToString(), out var expiry))
                        return expiry;
                }
            }
            return null;
        }
    }
}