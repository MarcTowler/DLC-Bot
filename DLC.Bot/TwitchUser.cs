using System;

namespace DLC.Bot
{
    public class TwitchUser
    {
        //public string Id { get; }
        public string Username { get; }
        public DateTime Update { get; set; }
        public bool IsEligable { get { return (DateTime.Now - Update).TotalMinutes >= 5; } }

        public TwitchUser(/*string id, */string username, DateTime update)
        {
            //Id = id;
            Username = username;
            Update = update;
        }
    }
}
