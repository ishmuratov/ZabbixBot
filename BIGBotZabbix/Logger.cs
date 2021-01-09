using System;
using System.Collections.Generic;
using System.Text;

namespace BIGBotZabbix
{
    public static class Logger
    {
        static List<String> LogMsg = new List<string>();

        public static void Log(string _msg)
        {
            LogMsg.Add(_msg);
        }

        public static void IOLog(string _msg)
        {
            Console.WriteLine(_msg);
        }

        public static void SaveLog()
        {
            StringBuilder sb = new StringBuilder();
            if (LogMsg != null)
            {
                if (LogMsg.Count > 1)
                {
                    foreach (string anyMsg in LogMsg)
                    {
                        sb.Append(anyMsg);
                        sb.Append(Environment.NewLine);
                    }
                }
                else
                {
                    sb.Append(LogMsg[0]);
                }
                FileWorker.WriteToFile(sb.ToString(), "log.txt");
                LogMsg.Clear();
            }
        }

    }
}
