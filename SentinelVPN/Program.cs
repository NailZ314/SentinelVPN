using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace VPN
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            SetCurrentProcessExplicitAppUserModelID("SentinelVPN");

            VPN.Classes.SubscriptionManager.Initialize();

            Form startupForm;

            if (UserStore.HasRegisteredUsers())
            {
                startupForm = new LoginForm();
            }
            else
            {
                startupForm = new RegistrationForm();
            }

            Application.Run(startupForm);
        }

        [DllImport("shell32.dll", SetLastError = true)]
        private static extern void SetCurrentProcessExplicitAppUserModelID(
            [MarshalAs(UnmanagedType.LPWStr)] string AppID);
    }
}
