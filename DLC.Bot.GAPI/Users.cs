using RestSharp;
using System.Net;

namespace DLC.Bot.GAPI
{
    public class Users
    {
        RestClient client;
        string baseURL = "https://gapi.itslit.uk";

        public int GetPoints(string TwitchID)
        {
            client = new RestClient(baseURL);

            var request = new RestRequest($"user/getcoins/{TwitchID}/2/?user=discord_bot&token=eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ1c2VyIjoiZGlzY29yZF9ib3QiLCJsZXZlbCI6NH0.QzeOwIJPvCh6DHJ5MFyTaz9H1TOOawB-mcblfFqKuIs", DataFormat.Json);

            var response = client.Get(request);

            if(response.StatusCode != HttpStatusCode.OK)
            {
                return -1;
            }

            return int.Parse(response.Content);
        }


    }
}
