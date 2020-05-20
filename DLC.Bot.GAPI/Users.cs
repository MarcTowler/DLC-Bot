using RestSharp;
using System.Collections.Generic;
using System.Net;
using DLC.Bot;
using System;
using Newtonsoft.Json.Linq;

namespace DLC.Bot.GAPI
{
    public class Users
    {
        RestClient client;
        string baseURL = "http://gapi";

        public int GetPoints(string TwitchID)
        {
            client = new RestClient(baseURL);

            var request = new RestRequest($"user/getcoins/{TwitchID}/2", DataFormat.Json);
            request.AddHeader("user", GAPIinfo.user);
            request.AddHeader("token", GAPIinfo.key);

            var response = client.Get(request);

            if(response.StatusCode != HttpStatusCode.OK)
            {
                return -1;
            }

            return int.Parse(response.Content);
        }

        public bool AddPoints(string TwitchUsers, int points)
        {
            //TODO: POST request to GAPI to add points to user, need to check before hand if they are a sub and/or follower as that affects points awarded
            Console.WriteLine($"DEBUG: {TwitchUsers}");
            JObject jBody = new JObject();
            jBody.Add("result", "true");
            jBody.Add("id", TwitchUsers);
            jBody.Add("flag", 0);
            jBody.Add("amount", points);

            client = new RestClient(baseURL);

            var request = new RestRequest("user/updateCoins", Method.POST);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("user", GAPIinfo.user);
            request.AddHeader("token", GAPIinfo.key);
            request.AddParameter("application/json", jBody, ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            Console.WriteLine(response.Content);
            return true;
        }

        public bool register(string TwitchUser)
        {
            JObject rBody = new JObject();
            rBody.Add("id", TwitchUser);
            rBody.Add("name", TwitchUser);
            rBody.Add("flag", 0);

            var request = new RestRequest("user/register", Method.POST);
            request.AddHeader("user", GAPIinfo.user);
            request.AddHeader("token", GAPIinfo.key);
            request.AddHeader("Accept", "application/json");
            request.AddParameter("application/json", rBody, ParameterType.RequestBody);
            return true;
        }
    }
}
