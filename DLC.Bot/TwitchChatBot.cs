using System;
using TwitchLib.Client.Models;
using TwitchLib.Client.Events;
using TwitchLib.Client;
using TwitchLib.Communication.Clients;
using TwitchLib.Communication.Models;

namespace DLC.Bot
{
    internal class TwitchChatBot
    {
        readonly ConnectionCredentials credentials = new ConnectionCredentials(TwitchInfo.BotUser, TwitchInfo.BotToken);
        TwitchClient client;
        public string message;

        internal void Connect()
        {
            Console.WriteLine("Initializing The Bot...");

            var wsClient = new WebSocketClient(new ClientOptions { MessagesAllowedInPeriod = 100, ThrottlingPeriod = TimeSpan.FromSeconds(30) });

            client = new TwitchClient(wsClient);
            client.Initialize(credentials, TwitchInfo.ChannelName);


            client.OnConnected += Client_OnConnected;
            client.OnJoinedChannel += Client_OnJoinedChannel;
            client.OnMessageReceived += Client_OnMessageReceived;
            client.OnNewSubscriber += Client_OnNewSubscriber;
            client.OnReSubscriber += Client_OnReSubscriber;
            client.OnGiftedSubscription += Client_OnGiftedSubscription;
            client.OnBeingHosted += Client_OnBeingHosted;
            client.OnWhisperReceived += Client_OnWhisperReceived;
            client.OnRaidNotification += Client_OnRaidNotification;

            client.Connect();
        }

        private void Client_OnRaidNotification(object sender, OnRaidNotificationArgs e)
        {
            Console.WriteLine($"(RAID) {e.RaidNotification.DisplayName} has raided with {e.RaidNotification.MsgParamViewerCount} viewers");
            client.SendMessage(TwitchInfo.ChannelName, $"INCOMINGGGGGG raid of {e.RaidNotification.MsgParamViewerCount} viewers from {e.RaidNotification.DisplayName}");
            client.SendMessage(TwitchInfo.ChannelName, $"Chat, go drop a follow to {e.RaidNotification.DisplayName} for being awesome https://twitch.tv/{e.RaidNotification.DisplayName}");
        }

        private void Client_OnWhisperReceived(object sender, OnWhisperReceivedArgs e)
        {
            throw new NotImplementedException();
        }

        private void Client_OnBeingHosted(object sender, OnBeingHostedArgs e)
        {
            Console.WriteLine($"(HOST) {e.BeingHostedNotification.HostedByChannel} is hosting with {e.BeingHostedNotification.Viewers}");
            client.SendMessage(TwitchInfo.ChannelName, $"Thanks for bring {e.BeingHostedNotification.Viewers} viewers in with the Host {e.BeingHostedNotification.HostedByChannel}");
        }

        private void Client_OnGiftedSubscription(object sender, OnGiftedSubscriptionArgs e)
        {
            Console.WriteLine($"(GIFTED SUB) {e.GiftedSubscription.DisplayName} gifted a sub to {e.GiftedSubscription.MsgParamRecipientDisplayName}");
            client.SendMessage(TwitchInfo.ChannelName, $"itslitHype itslitHype {e.GiftedSubscription.DisplayName} thank you for gifting {e.GiftedSubscription.MsgParamRecipientDisplayName} a {e.GiftedSubscription.MsgParamSubPlan} sub! itslitHype itslitHype");
        }

        private void Client_OnReSubscriber(object sender, OnReSubscriberArgs e)
        {
            Console.WriteLine($"(RESUB) {e.ReSubscriber.DisplayName} has resubbed for {e.ReSubscriber.MsgParamCumulativeMonths} months!");
            Console.WriteLine($"Resub Message: {e.ReSubscriber.ResubMessage}");
            client.SendMessage(TwitchInfo.ChannelName, $"itslitHype itslitHype Thank you for the {e.ReSubscriber.MsgParamCumulativeMonths} month {e.ReSubscriber.SubscriptionPlan} resub {e.ReSubscriber.DisplayName} itslitHype itslitHype");
        }

        private void Client_OnNewSubscriber(object sender, OnNewSubscriberArgs e)
        {
            Console.WriteLine($"(NEW SUB) {e.Subscriber.DisplayName} has Subscribed!");

            client.SendMessage(TwitchInfo.ChannelName, $"itslitHype itslitHype Thank you for the sub {e.Subscriber.DisplayName}, you have been awared 300 LitCoins itslitHype itslitHype");

        }

        private void Client_OnMessageReceived(object sender, OnMessageReceivedArgs e)
        {
            if (e.ChatMessage.IsBroadcaster)
            {
                message += "(Broadcaster) ";
            }
            else if (e.ChatMessage.IsModerator)
            {
                message += "(Mod) ";
            }
            else if (e.ChatMessage.IsSubscriber)
            {
                message += "(Sub) ";
            }

            message += e.ChatMessage.Username + ": " + e.ChatMessage.Message;

            Console.WriteLine(message);

            message = "";

            if (e.ChatMessage.Message.StartsWith("hi", StringComparison.InvariantCultureIgnoreCase))
            {
                client.SendMessage(TwitchInfo.ChannelName, $"Hello {e.ChatMessage.DisplayName}");
            }
        }

        private void Client_OnJoinedChannel(object sender, OnJoinedChannelArgs e)
        {
            Console.WriteLine($"Joined {e.Channel}'s channel");
        }

        private void Client_OnConnected(object sender, OnConnectedArgs e)
        {
            Console.WriteLine("Connected to Twitch!");
        }

        internal void Disconnect()
        {
            Console.WriteLine("Disconnecting Bot...");
        }
    }
}
