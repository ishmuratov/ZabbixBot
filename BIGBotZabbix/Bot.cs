using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BIGBotZabbix
{
    class Bot
    {
        TelegramAPI tAPI;
        private int lastUpdateId = 0;
        private TelegramMembers Users = (TelegramMembers)FileWorker.LoadFromFile("zabbixusers.dat");

        public Bot(TelegramAPI _api)
        {
            if (_api != null)
            {
                tAPI = _api;
            }
            else
            {
                tAPI = new TelegramAPI();
            }
        }

        public void SendMessage(string _text, int _idChat)
        {
            tAPI.sendAPIRequest("sendMessage", $"chat_id={_idChat}&text={BotHelpers.CheckText(_text)}");
        }

        public void SendMessageAll(string _text)
        {
            for (int i = 0; i < Users.GetCount(); i++)
            {
                tAPI.sendAPIRequest("sendMessage", $"chat_id={Users.GetUserID(i)}&text={BotHelpers.CheckText(_text)}");
            }
        }

        public void SendMessageAll(List<string> _mails)
        {
            foreach (string text in _mails)
            {
                for (int i = 0; i < Users.GetCount(); i++)
                {
                    tAPI.sendAPIRequest("sendMessage", $"chat_id={Users.GetUserID(i)}&text={BotHelpers.CheckText(text)}");
                }
            }
        }

        public void SendMessageAll(string _text, int authorID)
        {
            for (int i = 0; i < Users.GetCount(); i++)
            {
                if (Users.GetUserID(i) != authorID)
                {
                    tAPI.sendAPIRequest("sendMessage", $"chat_id={Users.GetUserID(i)}&text={BotHelpers.CheckText(_text)}");
                }
            }
        }

        public void GetMessage()
        {
            if (Users == null)
            {
                Users = new TelegramMembers();
            }

            var json = tAPI.sendAPIRequest("getUpdates", $"offset={lastUpdateId}");
            var apiResult = JsonConvert.DeserializeObject<APIResult>(json);

            if ((apiResult == null) || (apiResult.Result == null))
            {
                return;
            }

            foreach (var update in apiResult.Result)
            {
                if (!BotHelpers.CheckUpdate(update))
                {
                    continue;
                }

                User newUser = new User();
                newUser.ID = update.message.chat.id;
                newUser.FirstName = update.message.chat.first_name;
                newUser.LastName = update.message.chat.last_name;

                // log

                Logger.Log($"Получен апдейт от {update.message.chat.first_name} {update.message.chat.last_name} id {update.message.chat.id} текст: {update.message.text}");
                lastUpdateId = update.update_id + 1;

                // Commands

                if (CheckCommandsForBot(newUser, update.message.text))
                {
                    return;
                }

                // Check user

                if (Users.CheckUser(newUser))
                {
                    SendMessageAll($"{newUser.FirstName} {newUser.LastName} : {update.message.text}", newUser.ID);
                }
                else
                {
                    SendMessage("Введите пароль:", newUser.ID);
                }
            }
        }

        private bool CheckCommandsForBot(User _newUser, string _text)
        {
            // get info

            if (_text.ToLower().Contains("info"))
            {
                return InfoCommand();
            }

            // password for new users

            if (_text.Contains(AppSettings.AccessPassword))
            {
                return AccessCommand(_newUser);
            }

            // delete member

            if (_text.ToLower().Contains("delete "))
            {
                return DeleteCommand(_text);
            }

            // Ping host

            if (_text.ToLower().Contains("ping "))
            {
                return PingCommand(_newUser, _text);
            }
            return false;
        }

        private bool InfoCommand()
        {
            string msg = $"Members:\n";
            foreach (User anyUser in Users.GetUsers())
            {
                msg += $"{anyUser.ID} - {anyUser.FirstName} {anyUser.LastName}\n";
            }
            SendMessage($"{msg}", AppSettings.AdminID);
            return true;
        }

        private bool AccessCommand(User _newUser)
        {
            string result = Users.Add(_newUser);
            SendMessage(result, _newUser.ID);
            return true;
        }

        private bool DeleteCommand(string _text)
        {
            _text = _text.Replace("delete ", String.Empty);
            int deleteId = 0;
            bool tryDelete = int.TryParse(_text, out deleteId);
            if (tryDelete)
            {
                string result = Users.Delete(deleteId);
                SendMessage(result, AppSettings.AdminID);
                return true;
            }
            return false;
        }

        private bool PingCommand(User _newUser, string _text)
        {
            Host tmpHost = new Host(BotHelpers.GetHostNamefromText(_text));
            if (tmpHost.IP != string.Empty)
            {
                if (Pinger.PingHost(tmpHost))
                {
                    SendMessage($"Ping host <{tmpHost.IP}> - OK! Time: {tmpHost.pingTime} ms.", _newUser.ID);
                }
                else
                {
                    SendMessage($"Ping host <{tmpHost.IP}> - FAIL!", _newUser.ID);
                }
            }
            else
            {
                SendMessage($"Неверный формат команды PING.", _newUser.ID);
            }
            return true;
        }
    }
}
