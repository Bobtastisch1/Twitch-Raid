using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using TwitchRaid.Models;

namespace TwitchRaid.Controller
{
    internal class PostRaid
    {
        public async Task<List<Raid>> PostRaids(Setting setting, Streams to_broadcaster_id)
        {
            try
            {
                string url = "https://api.twitch.tv/helix/raids?from_broadcaster_id=" + setting.user_id + "&to_broadcaster_id=" + to_broadcaster_id.user_id;
                HttpClient client = new();
                
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", setting.oauth);
                client.DefaultRequestHeaders.Add("Client-Id", setting.ClientID);
                var res = await client.PostAsync(url, null);
                res.EnsureSuccessStatusCode();
                string result = await res.Content.ReadAsStringAsync();
                RaidDTO raidDTO = JsonConvert.DeserializeObject<RaidDTO>(result);
                List<Raid> raid = raidDTO.data;

                return raid;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Access_Token Error " + ex.Message);
                Console.WriteLine("This Streamer can't get Raided");
            }


            return null;
        }
    }
}
