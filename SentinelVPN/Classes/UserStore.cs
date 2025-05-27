using System;
using System.Data.SQLite;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace VPN
{
    public static class UserStore
    {
        private static readonly string dbPath = "users.db";
        private static readonly string ConnectionString = $"Data Source={dbPath};Version=3;";

        static UserStore()
        {
            if (!File.Exists(dbPath))
            {
                SQLiteConnection.CreateFile(dbPath);
            }
            using (var conn = new SQLiteConnection("Data Source=" + dbPath))
            {
                conn.Open();
                string table = "CREATE TABLE IF NOT EXISTS users (email TEXT PRIMARY KEY, username TEXT, passwordHash TEXT)";
                new SQLiteCommand(table, conn).ExecuteNonQuery();
            }
        }

        public static bool RegisterUser(string email, string username, string password)
        {
            if (UserExists(email)) return false;

            string hash = HashPassword(password);

            using (var conn = new SQLiteConnection("Data Source=" + dbPath))
            {
                conn.Open();
                var cmd = new SQLiteCommand("INSERT INTO users (email, username, passwordHash) VALUES (@e, @u, @p)", conn);
                cmd.Parameters.AddWithValue("@e", email);
                cmd.Parameters.AddWithValue("@u", username);
                cmd.Parameters.AddWithValue("@p", hash);
                cmd.ExecuteNonQuery();
            }
            return true;
        }

        public static bool ValidateUser(string email, string password)
        {
            using (var conn = new SQLiteConnection("Data Source=" + dbPath))
            {
                conn.Open();
                var cmd = new SQLiteCommand("SELECT passwordHash FROM users WHERE email = @e", conn);
                cmd.Parameters.AddWithValue("@e", email);
                var result = cmd.ExecuteScalar();
                if (result == null) return false;
                return VerifyPassword(password, result.ToString());
            }
        }

        private static bool UserExists(string email)
        {
            using (var conn = new SQLiteConnection("Data Source=" + dbPath))
            {
                conn.Open();
                var cmd = new SQLiteCommand("SELECT 1 FROM users WHERE email = @e", conn);
                cmd.Parameters.AddWithValue("@e", email);
                return cmd.ExecuteScalar() != null;
            }
        }

        private static string HashPassword(string password)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }

        private static bool VerifyPassword(string password, string hash)
        {
            return HashPassword(password) == hash;
        }

        public static bool HasRegisteredUsers()
        {
            using (var conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM users";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    long count = (long)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        public static string GetUsername(string email)
        {
            using (var conn = new SQLiteConnection("Data Source=users.db;Version=3;"))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("SELECT username FROM users WHERE email = @Email", conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    return cmd.ExecuteScalar()?.ToString() ?? string.Empty;
                }
            }
        }

        public static bool UserExistsByEmail(string email)
        {
            using (var conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("SELECT COUNT(*) FROM users WHERE email = @Email", conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
            }
        }

        public static bool UserExistsByUsername(string username)
        {
            using (var conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("SELECT COUNT(*) FROM users WHERE username = @Username", conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
            }
        }
    }
}
