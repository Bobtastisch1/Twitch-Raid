using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TwitchRaid.Models;

namespace TwitchRaid.Controller
{
    internal class GetChannelFollower
    {
        public async Task<List<Follower>> GetChannelFollowers(Setting setting)
        {
            try
            {         
                string url = "https://api.twitch.tv/helix/channels/followers?broadcaster_id=" + setting.user_id + "&first=100";
                HttpClient client = new();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", setting.oauth);
                client.DefaultRequestHeaders.Add("Client-Id", setting.ClientID);

                bool hasPagination = true;
                List<Follower> allFollowers = new List<Follower>();

                while (hasPagination)
                {
                    HttpResponseMessage res = await client.GetAsync(url);
                    res.EnsureSuccessStatusCode();
                    string result = await res.Content.ReadAsStringAsync();
                    FollowersDTO FollowersDTO = JsonConvert.DeserializeObject<FollowersDTO>(result);
                    List<Follower> followers = FollowersDTO.data;

                    allFollowers.AddRange(followers);

                    if (!string.IsNullOrEmpty(FollowersDTO.pagination.cursor))
                    {
                        url = url + "&after=" + FollowersDTO.pagination.cursor;
                    }
                    else
                    {
                        hasPagination = false;
                    }
                }

                return allFollowers;
            }
            catch (Exception e)
            {
                Console.WriteLine("Get Followers Error " + e.Message);
            }

            return null;
        }
    }
}
