using System.IO;

namespace BIGBotZabbix
{
    static class AppSettings
    {
        // Telegram settings
        public static readonly string BotToken = "";
        public static readonly int AdminID = 111111111;

        // Mail settings
        public static string MailServer = "imap.gmail.com";
        public static int MailServerPort = 993;
        public static string MailUser = "";
        public static string MailPassword = "";
        public static int MailCheckDelay = 180000;

        // Application settings
        public static string Version = "1.0.4";
        public static string AccessPassword = "password";
        public static string SpecialFromWord = "Zabbix";
        public static string SpecialSubjectWord = "Critical";

        static AppSettings()
        {
            if (File.Exists("settings.ini"))
            {
                string[] dataSettings = File.ReadAllLines("settings.ini");
                // Setup setting from file
                string[] BotTokenRaw = dataSettings[1].Split('=');
                BotToken = BotTokenRaw[1];
                string[] AdminIDRaw = dataSettings[2].Split('=');
                int.TryParse(AdminIDRaw[1], out int result);
                AdminID = result;
                string[] MailServerRaw = dataSettings[5].Split('=');
                MailServer = MailServerRaw[1];
                string[] MailServerPortRaw = dataSettings[6].Split('=');
                int.TryParse(MailServerPortRaw[1], out result);
                MailServerPort = result;
                string[] MailUserRaw = dataSettings[7].Split('=');
                MailUser = MailUserRaw[1];
                string[] MailPasswordRaw = dataSettings[8].Split('=');
                MailPassword = MailPasswordRaw[1];
                string[] MailCheckDelayRaw = dataSettings[9].Split('=');
                int.TryParse(MailCheckDelayRaw[1], out result);
                MailCheckDelay = result;
                string[] AccessPasswordRaw = dataSettings[12].Split('=');
                AccessPassword = AccessPasswordRaw[1];
                string[] SpecialFromWordRaw = dataSettings[13].Split('=');
                SpecialFromWord = SpecialFromWordRaw[1];
                string[] SpecialSubjectWordRaw = dataSettings[14].Split('=');
                SpecialSubjectWord = SpecialSubjectWordRaw[1];
            }
            CheckSettings();
        }

        static void CheckSettings()
        {
            if (string.IsNullOrEmpty(SpecialFromWord) || (string.IsNullOrWhiteSpace(SpecialFromWord)))
            {
                SpecialFromWord = "Zabbix";
            }
            if (string.IsNullOrEmpty(SpecialSubjectWord) || (string.IsNullOrWhiteSpace(SpecialSubjectWord)))
            {
                SpecialFromWord = "Critical";
            }
        }
    }
}
