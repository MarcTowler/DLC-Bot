using System;
using System.Collections.Generic;
using System.Text;
using TwitchLib.Api;

namespace DLC.Bot
{
    class API
    {
        private TwitchAPI tAPI;

        public void Connect()
        {
            tAPI = new TwitchAPI();

            tAPI.Settings.ClientId = TwitchInfo.ClientID;
            tAPI.Settings.AccessToken = TwitchInfo.ChannelOAuth;

            
        }


    }
}
