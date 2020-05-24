using System;
using System.Collections.Generic;
using TwitchLib.Api;
using TwitchLib.Api.Core.Models.Undocumented.Chatters;
using System.Timers;
using System.Linq;

namespace DLC.Bot
{
    class API
    {
        private TwitchAPI tAPI;
        private Timer _timer;
        static List<TwitchUser> Users = new List<TwitchUser>();

        public void Connect()
        {
            tAPI = new TwitchAPI();

            tAPI.Settings.ClientId = TwitchInfo.ClientID;
            tAPI.Settings.AccessToken = TwitchInfo.ChannelOAuth;

            getChatters();

            _timer = new Timer(60000); //once a minute is fine it's heavily cached
            _timer.Elapsed += onTimerElapsed;
            _timer.Start();
        }

        private void onTimerElapsed(object sender, ElapsedEventArgs e)
        {
            getChatters();
        }

        public List<TwitchUser> getChatters()
        {
            List<ChatterFormatted> chatters = tAPI.Undocumented.GetChattersAsync("itslittany").Result;

            foreach (var chatter in chatters)
            {
                TwitchUser tu = new TwitchUser(chatter.Username, DateTime.Now);
                var tmp = Users.FirstOrDefault(x => x.Username == chatter.Username);

                if(tmp == null)
                {
                    Users.Add(tu);

                    Console.WriteLine($"JOIN: {chatter.Username}");
                }
            }


            foreach(TwitchUser user in Users)
            {
                if(!chatters.Any(x => x.Username == user.Username))
                {
                    Users.Remove(user);

                    Console.WriteLine($"LEFT: {user.Username}");
                }
            }

            return Users;
        }

        public void getChatterID(string name)
        {
            var lst = new List<string>();

            lst.Add(name);

            var Users = tAPI.Helix.Users.GetUsersAsync(null, lst, TwitchInfo.ChannelOAuth).Result;

            //return "";
        }
    }
}
