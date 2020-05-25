using System;
using System.Drawing;
using System.Threading;
using Console = Colorful.Console;

namespace DLC.Bot
{
    class Program
    {
        public static bool Cancelled;
        private static TwitchChatBot bot;

        static void Main(string[] args)
        {
            Console.BackgroundColor = Color.FromArgb(51, 51, 51);
            Console.Title = "DLC Bot";
            Console.Clear();

            Console.WriteLine("DLC Bot has started turning zeros into ones");
            bot = new TwitchChatBot();

            bot.Connect();
            Console.WriteLine("Enter 'help' to get all commands.", Color.White);

            WaitForInput();
        }

        public static void WaitForInput()
        {
            while(!Cancelled)
            {
                string input = Console.ReadLine();

                switch(input.ToLower())
                {
                    case "stop":
                        Cancelled = true;

                        break;
                    case "reload":
                        //bot.ReloadConfig();

                        break;
                    case "help":
                        GetHelp();

                        break;
                    case "restart":
                        Restart();

                        break;
                    default:
                        break;
                }
            }

            bot.Disconnect();

            Console.WriteLine("DLC Bot is returning the zeros and ones to their default state");

            Thread.Sleep(500);
        }

        public static void GetHelp()
        {
            Console.WriteLine("=== Comands ===");
            Console.WriteLine("Commands that can be used:");
            Console.WriteLine("'online' => get all online streamers.");
            Console.WriteLine("'offline' => get all offline streamers.");
            Console.WriteLine("'hosting' => get all streamers that are hosting.");
            Console.WriteLine("'reload' => reload the configs.");
            Console.WriteLine("'restart' => restart the bot.");
            Console.WriteLine("'stop' => disconnect and stop the bot.");
        }

        public static void Restart()
        {
            bot.Disconnect();
            Thread.Sleep(2000);

            bot = new TwitchChatBot();
            //bot.RegisterFeaturesAsync();
            bot.Connect();
            WaitForInput();
        }
    }
}
