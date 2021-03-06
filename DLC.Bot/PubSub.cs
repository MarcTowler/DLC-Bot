﻿using DLC.Bot;
using System;
using System.Drawing;
using TwitchLib.PubSub;
using TwitchLib.PubSub.Events;

namespace DLC.Bot
{
    class PubSub
    {
        private static TwitchPubSub client;
        public void Connect()
        {
            client = new TwitchPubSub();

            client.OnPubSubServiceConnected += onPubSubServiceConnected;
            client.OnListenResponse += onListenResponse;
            client.OnStreamUp += onStreamUp;
            client.OnStreamDown += onStreamDown;
            client.OnBitsReceived += onBitsReceived;
            client.OnRewardRedeemed += onRewardRedeemed;
            client.OnFollow += onFollow;
            client.OnChannelSubscription += onSubscription;

            client.ListenToBitsEvents("57026834");
            client.ListenToFollows("57026834");
            client.ListenToRewards("57026834");
            client.ListenToSubscriptions("57026834");
            client.ListenToVideoPlayback("57026834");

            client.Connect();

        }

        private void onSubscription(object sender, OnChannelSubscriptionArgs e)
        {
            throw new NotImplementedException();
        }

        private void onFollow(object sender, OnFollowArgs e)
        {
            Console.WriteLine($"NEW FOLLOWER: {e.DisplayName}");
        }

        private void onRewardRedeemed(object sender, OnRewardRedeemedArgs e)
        {
            throw new NotImplementedException();
        }

        private void onBitsReceived(object sender, OnBitsReceivedArgs e)
        {
            Console.WriteLine($"BITS: {e.BitsUsed} from {e.Username}");
        }

        private static void onPubSubServiceConnected(object sender, EventArgs e)
        {
            Console.WriteLine("PUBSUB Connected");
            // SendTopics accepts an oauth optionally, which is necessary for some topics
            client.SendTopics(TwitchInfo.ChannelOAuth);
        }

        private static void onListenResponse(object sender, OnListenResponseArgs e)
        {
            if (!e.Successful)
            {
                throw new Exception($"Failed to listen! Response: {e.Response.Error}");
            }
            Console.WriteLine($"Listening to {e.Topic}", Color.Green);
        }

        private static void onStreamUp(object sender, OnStreamUpArgs e)
        {
            Console.WriteLine($"Stream just went up! Play delay: {e.PlayDelay}, server time: {e.ServerTime}");
        }

        private static void onStreamDown(object sender, OnStreamDownArgs e)
        {
            Console.WriteLine($"Stream just went down! Server time: {e.ServerTime}");
        }
    }
}