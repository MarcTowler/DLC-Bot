using System;

namespace DLC.Bot
{
    class Program
    {
        static void Main(string[] args)
        {
            var bot = new TwitchChatBot();

            bot.Connect();

            Console.ReadLine();

            bot.Disconnect();
        }
    }
}
