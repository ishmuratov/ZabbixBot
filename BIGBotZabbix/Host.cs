namespace BIGBotZabbix
{
    class Host
    {
        public string IP { get; set; }
        public long pingTime { get; set; }

        public Host(string _ip)
        {
            IP = _ip;
            pingTime = 0;
        }
    }
}
