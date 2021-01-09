using System;
using System.Collections.Generic;
using System.Linq;

namespace BIGBotZabbix
{
    [Serializable]
    class TelegramMembers : IData
    {
        List<User> Users { get; set; }

        public TelegramMembers()
        {
            Users = new List<User>();
        }

        public string Add(User _user)
        {
            User findUser = Users.Where(i => i.ID == _user.ID).FirstOrDefault();
            if (findUser == null)
            {
                Users.Add(_user);
                FileWorker.SaveToFile(this, "zabbixusers.dat");
                return "Пароль верный! Вы подписались на уведомления от BIG Zabbix.";
            }
            else
            {
                return "Ты уже являешься подписчиком...";
            }
        }

        public string Delete(int _id)
        {
            User findUser = Users.Where(i => i.ID == _id).FirstOrDefault();
            if (findUser != null)
            {
                Users.Remove(findUser);
                FileWorker.SaveToFile(this, "zabbixusers.dat");
                return "Пользователь успешно удалён!";
            }
            return "Пользователь с таким id не найден!";
        }

        public bool CheckUser(User _user)
        {
            User findUser = Users.Where(i => i.ID == _user.ID).FirstOrDefault();
            if (findUser == null)
            {
                return false;
                //return "Введите пароль:";
            }
            else
            {
                return true;
                //return $"{_user.FirstName} {_user.LastName}:   " + _text;
            }
        }

        public int GetCount()
        {
            return Users.Count;
        }

        public List<User> GetUsers()
        {
            return Users;
        }

        public int GetUserID(int _id)
        {
            if (_id > 0)
            {
                return Users[_id].ID;
            }
            else
            {
                return AppSettings.AdminID;
            }
        }

    }
}
