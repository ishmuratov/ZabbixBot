using System;
using System.Collections.Generic;

namespace BIGBotZabbix
{
    class Email
    {
        public List<string> GetMail()
        {
            int MailCount = 0;
            List<string> Mails = new List<string>();

            try
            {
                using (AE.Net.Mail.ImapClient ic = new AE.Net.Mail.ImapClient(AppSettings.MailServer,
                        AppSettings.MailUser,
                        AppSettings.MailPassword, AE.Net.Mail.AuthMethods.Login,
                        AppSettings.MailServerPort, true))
                {
                    ic.SelectMailbox("INBOX");

                    // Note that you must specify that headersonly = false
                    // when using GetMesssages().
                    AE.Net.Mail.MailMessage[] mm = ic.GetMessages(0, 50, false);

                    foreach (AE.Net.Mail.MailMessage m in mm)
                    {
                        MailCount++;
                        if (m.From.ToString().Contains(AppSettings.SpecialFromWord))
                        {
                            Mails.Add(m.Body);
                            ic.DeleteMessage(m);
                        }
                        if (m.Subject.ToString().Contains(AppSettings.SpecialSubjectWord))
                        {
                            Mails.Add(m.Subject);
                            ic.DeleteMessage(m);
                        }
                        else
                        {
                            Logger.Log(m.From.ToString());
                            Logger.Log(m.Body.ToString());
                            ic.DeleteMessage(m);
                        }
                    }
                    Logger.Log($"::: {DateTime.Now} Mail count: {MailCount}");
                    ic.Dispose();
                }
            }
            catch (Exception _ex)
            {
                Logger.Log($"::: {DateTime.Now} Error: " + _ex.Message);
            }

            return Mails;
        }
    }
}
