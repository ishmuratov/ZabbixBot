using RestSharp;

namespace BIGBotZabbix
{
    class TelegramAPI
    {
        readonly string API_URL = "https://api.telegram.org/bot" + AppSettings.BotToken + "/";

        public string sendAPIRequest(string _apiMethod, string _params)
        {
            RestClient RC = new RestClient();
            var Url = API_URL + _apiMethod + "?" + _params;
            var Request = new RestRequest(Url);
            var Response = RC.Get(Request);

            return Response.Content;
        }
    }

    public class APIResult
    {
        public Update[] Result { get; set; }
    }

    public class Update
    {
        public int update_id { get; set; }
        public Message message { get; set; }
    }

    public class Message
    {
        public Chat chat { get; set; }
        public string text { get; set; }
    }

    public class Chat
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
    }
}
