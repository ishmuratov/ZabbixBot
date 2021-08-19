using System;

namespace BIGBotZabbix
{
    public static class Logo
    {
        public static void Print()
        {
            Console.WriteLine("+------------------------------------------+");
            Console.WriteLine("|                                          |");
            Console.WriteLine("|               BIGBotZabbix               |");
            Console.WriteLine($"|        Start: {DateTime.Now.ToString()}        |");
            Console.WriteLine("|                                          |");
            Console.WriteLine("+------------------------------------------+");
            Console.WriteLine($"|              Version: {AppSettings.Version}              |");
            Console.WriteLine("+------------------------------------------+");
            Console.WriteLine();
            Console.WriteLine($"AdminID: {AppSettings.AdminID}");
            Console.WriteLine($"Mail Server: {AppSettings.MailServer} Port: {AppSettings.MailServerPort}");
            Console.WriteLine($"Mail User: {AppSettings.MailUser}");
            Console.WriteLine($"Mail Check Delay: {AppSettings.MailCheckDelay / 1000} Seconds");
            Console.WriteLine($"Special Words: {AppSettings.SpecialFromWord} , {AppSettings.SpecialSubjectWord}");
        }

    }
}
