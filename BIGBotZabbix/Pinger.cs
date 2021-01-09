using System;
using System.Net.NetworkInformation;

namespace BIGBotZabbix
{
    class Pinger
    {
        public static bool PingHost(Host _host)
        {
            bool pingable = false;
            Ping pinger = null;

            try
            {
                pinger = new Ping();
                PingReply reply = pinger.Send(_host.IP);
                pingable = reply.Status == IPStatus.Success;
                _host.pingTime = reply.RoundtripTime;
            }
            catch (PingException)
            {
                // Discard PingExceptions and return false;
            }
            finally
            {
                if (pinger != null)
                {
                    pinger.Dispose();
                }
            }

            return pingable;
        }
    }
}
