using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchRaid.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net.Http.Headers;
using System.IO;

namespace TwitchRaid.Controller
{
    internal class GetStreamers
    {
        public async Task<List<Streams>> GetLiveStreams(Setting setting, string token, FollowerList followerlist)
        {
            List<Streams> streamlist = new List<Streams>();
            try
            {
                HttpClient client = new();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                client.DefaultRequestHeaders.Add("Client-Id", setting.ClientID);
                
                int userIdStartsAt = 0;
                int maxSize = 100;
                int totalFollowers = followerlist.follower.Count;

                while (userIdStartsAt < totalFollowers)
                {

                    string url = "https://api.twitch.tv/helix/streams";
                    int maxID = Math.Min(maxSize, totalFollowers - userIdStartsAt);

                    for (int i = userIdStartsAt; i < userIdStartsAt + maxID; i++)
                    {
                        if(i != 0)
                        {
                            url += "&user_id=" + followerlist.follower[i].user_id;
                        }
                        else
                        {
                            url += "?user_id=" + followerlist.follower[i].user_id;
                        }
                    }

                    HttpResponseMessage res = await client.GetAsync(url);
                    res.EnsureSuccessStatusCode();
                    string result = await res.Content.ReadAsStringAsync();
                    LiveStreamsDTO liveStreamsDTO = JsonConvert.DeserializeObject<LiveStreamsDTO>(result);

                    streamlist.AddRange(liveStreamsDTO.data);
                    userIdStartsAt += maxID;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Get Users Error " + e.Message);
            }
            return streamlist;
        }

    }
}
