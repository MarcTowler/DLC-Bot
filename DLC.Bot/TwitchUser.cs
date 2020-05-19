using System;

namespace DLC.Bot
{
    class TwitchUser
    {
        public string user = "";
        public DateTime joinTime;

        public TwitchUser(string _user, DateTime Joined)
        {
            user = _user;
            joinTime = Joined;
        }
    }
}
