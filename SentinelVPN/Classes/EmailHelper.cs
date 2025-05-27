using System;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;

namespace VPN
{
    public static class EmailHelper
    {
        public static bool SendVerificationCode(string toEmail, string code)
        {
            try
            {
                var fromEmail = "ersa00759@gmail.com";
                var fromPassword = "scos zaur rxdt yyyy";

                var smtp = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential(fromEmail, fromPassword),
                    EnableSsl = true
                };

                var fromAddress = new MailAddress(fromEmail, "SentinelVPN");
                var toAddress = new MailAddress(toEmail);

                var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = "Your VPN verification code",
                    Body = $"Your code is: {code}"
                };

                smtp.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to send verification email:\n" + ex.Message);
                return false;
            }
        }
    }
}