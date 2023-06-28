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

                setting.user_id = users[0].id;

                return users;
            }
            catch (Exception e)
            {
                Console.WriteLine("Get Users Error " + e.Message);
            }

            return null;
        }

        public async Task<List<User>> GetFavorite(Setting setting, string token)
        {
            List<User> FavoriteList = new List<User>();
            try
            {
                HttpClient client = new();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                client.DefaultRequestHeaders.Add("Client-Id", setting.ClientID);

                int FavoriteuserIdStartsAt = 0;
                int maxSize = 100;
                int totalFavorite = setting.Favorite.Count;

                while(FavoriteuserIdStartsAt < totalFavorite)
                {
                    string url = "https://api.twitch.tv/helix/users";
                    int maxID = Math.Min(maxSize, totalFavorite - FavoriteuserIdStartsAt);

                    for (int i = FavoriteuserIdStartsAt; i < FavoriteuserIdStartsAt + maxID; i++)
                    {
                        if (FavoriteuserIdStartsAt != i)
                        {
                            url += "&login=" + setting.Favorite[i];
                        }
                        else
                        {
                            url += "?login=" + setting.Favorite[i];
                        }
                    }

                    HttpResponseMessage res = await client.GetAsync(url);
                    res.EnsureSuccessStatusCode();
                    string result = await res.Content.ReadAsStringAsync();
                    UsersDTO favoriteDTO = JsonConvert.DeserializeObject<UsersDTO>(result);
                    List<User> favorite = favoriteDTO.data;
                    FavoriteList.AddRange(favoriteDTO.data);
                    FavoriteuserIdStartsAt += maxID;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Get Favorite Users Error " + e.Message);
            }

            return FavoriteList;
        }
    }
}
