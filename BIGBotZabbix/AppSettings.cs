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
                string[] RawData = dataSettings[1].Split('=');
                BotToken = RawData[1];
                RawData = dataSettings[2].Split('=');
                int.TryParse(RawData[1], out int result);
                AdminID = result;
                RawData = dataSettings[5].Split('=');
                MailServer = RawData[1];
                RawData = dataSettings[6].Split('=');
                int.TryParse(RawData[1], out result);
                MailServerPort = result;
                RawData = dataSettings[7].Split('=');
                MailUser = RawData[1];
                RawData = dataSettings[8].Split('=');
                MailPassword = RawData[1];
                RawData = dataSettings[9].Split('=');
                int.TryParse(RawData[1], out result);
                MailCheckDelay = result;
                RawData = dataSettings[12].Split('=');
                AccessPassword = RawData[1];
                RawData = dataSettings[13].Split('=');
                SpecialFromWord = RawData[1];
                RawData = dataSettings[14].Split('=');
                SpecialSubjectWord = RawData[1];
            }
            CheckSettings();
        }

        static void CheckSettings()
        {
            if (MailCheckDelay < 30000)
            {
                MailCheckDelay = 30000;
            }
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
