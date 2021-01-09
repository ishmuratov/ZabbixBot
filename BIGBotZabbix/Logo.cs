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
        }

    }
}
