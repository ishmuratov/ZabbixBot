using System;

namespace BIGBotZabbix
{
    static class BotHelpers
    {
        public static bool CheckUpdate(Update _update)
        {
            if (_update == null || _update.message == null || _update.message.chat == null)
            {
                return false;
            }

            if (_update.message.text == null)
            {
                _update.message.text = String.Empty;
            }
            if (_update.message.chat.last_name == null)
            {
                _update.message.chat.last_name = "Unknown";
            }
            if (_update.message.chat.first_name == null)
            {
                _update.message.chat.first_name = "Unknown";
            }

            return true;
        }

        public static string GetHostNamefromText(string _text)
        {
            string[] inputData = _text.Split(' ');
            if (inputData.Length == 2)
            {
                return inputData[1];
            }
            return string.Empty;
        }

        public static string CheckText(string _text)
        {
            _text = _text.Replace("=", " равно ");
            _text = _text.Replace("+", " плюс ");
            if (_text.Contains("resolved at") || (_text.Contains("started at")))
                return add3hours(_text);
            else
                return _text;
        }

        // shit code

        static string add3hours(string _input)
        {
            if (_input.Contains("resolved at"))
            {
                DateTime time = Convert.ToDateTime(_input.Substring(29, 8));
                _input = _input.Remove(29, 8);
                time = time.AddHours(3);
                _input = _input.Insert(29, time.ToLongTimeString());
                return _input;
            }

            if (_input.Contains("started at"))
            {
                DateTime time = Convert.ToDateTime(_input.Substring(19, 8));
                _input = _input.Remove(19, 8);
                time = time.AddHours(3);
                _input = _input.Insert(19, time.ToLongTimeString());
                return _input;
            }
            return _input;
        }
    }
}
