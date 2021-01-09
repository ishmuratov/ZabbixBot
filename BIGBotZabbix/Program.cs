using System.Collections.Generic;
using System.Threading;

namespace BIGBotZabbix
{
    class Program
    {
        static List<string> mails = new List<string>();

        static void Main(string[] args)
        {
            Logo.Print();

            int num = 0;

            int delay_time = AppSettings.MailCheckDelay; // delay for checking e'mail

            TelegramAPI API = new TelegramAPI();
            Bot ZabbixBot = new Bot(API);

            TimerCallback tm = new TimerCallback(Count);
            Timer timer = new Timer(tm, num, delay_time, delay_time);
            while (true)
            {
                ZabbixBot.GetMessage();
                if (mails.Count > 0)
                {
                    ZabbixBot.SendMessageAll(mails);
                    mails.Clear();
                }
            }
        }

        public static void Count(object obj)
        {
            Email mail = new Email();
            mails = mail.GetMail();
            Logger.SaveLog();
        }
    }
}
