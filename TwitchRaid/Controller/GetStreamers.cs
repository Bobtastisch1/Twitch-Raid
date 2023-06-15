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
            try
            {
                HttpClient client = new();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                client.DefaultRequestHeaders.Add("Client-Id", setting.ClientID);

                bool hasPagination = true;
                List<Streams> streamlist = new List<Streams>();
                int userIdStartsAt = 0;
                while (hasPagination)
                {
                    string url = "https://api.twitch.tv/helix/streams";
                    for (int i = userIdStartsAt; i < userIdStartsAt + 101; i++)
                    {

                        url += "?user_id=" + followerlist.follower[i].user_id;
                        Console.WriteLine(i);
                    }

                    HttpResponseMessage res = await client.GetAsync(url);
                    res.EnsureSuccessStatusCode();
                    string result = await res.Content.ReadAsStringAsync();
                    LiveStreamsDTO liveStreamsDTO = JsonConvert.DeserializeObject<LiveStreamsDTO>(result);

                    streamlist.AddRange(liveStreamsDTO.data);
                    Follower lastFollower = followerlist.follower.FindLast(f => f.user_id != null);
                    if (lastFollower != null)
                    {
                        if(liveStreamsDTO.pagination.cursor != null)
                        {
                            url = url + "&after=" + liveStreamsDTO.pagination.cursor;
                        }
                        userIdStartsAt += 101;
                    }
                    else
                    {
                        hasPagination = false;
                    }
                }
                return streamlist;
            }
            catch (Exception e)
            {
                Console.WriteLine("Get Users Error " + e.Message);
            }
            return null;     
        }

    }
}
