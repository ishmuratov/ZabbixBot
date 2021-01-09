namespace BIGBotZabbix
{
    static class AppSettings
    {
        // Telegram settings
        public const string BotToken = "";
        public const int AdminID = 111111111;

        // Mail settings
        public const string MailServer = "imap.gmail.com";
        public const int MailServerPort = 993;
        public const string MailUser = "";
        public const string MailPassword = "";
        public const int MailCheckDelay = 180000;   // 3 min

        // Application settings
        public const string Version = "1.0.3";
        public const string AccessPassword = "password";

    }
}
