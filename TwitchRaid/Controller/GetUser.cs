using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TwitchRaid.Models;

namespace TwitchRaid.Controller
{
    internal class GetUser
    {
        public async Task<List<User>> GetUsers(Setting setting, string token)
        {
            try
            {
                string url = "https://api.twitch.tv/helix/users?login=" + setting.YourStreamerName;
                HttpClient client = new();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                client.DefaultRequestHeaders.Add("Client-Id", setting.ClientID);

                HttpResponseMessage res = await client.GetAsync(url);
                res.EnsureSuccessStatusCode();
                string result = await res.Content.ReadAsStringAsync();
                UsersDTO userDTO = JsonConvert.DeserializeObject<UsersDTO>(result);
                List<User> users = userDTO.data;

                return users;
            }
            catch (Exception e)
            {
                Console.WriteLine("Get Users Error " + e.Message);
            }

            return null;
        }
    }
}
