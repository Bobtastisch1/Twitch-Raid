using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using TwitchRaid.Models;

namespace TwitchRaid.Controller
{
    internal class Token
    {
        private Int64 expries = 0;
        private TokenResponse tokenResponse;

        public async Task<string> Tokenhandler(Setting setting)
        {
            if (tokenResponse == null || expries < 5000)
            {
                try
                {
                    string url = "https://id.twitch.tv/oauth2/token";
                    HttpClient client = new();
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);
                    request.Headers.Add("Accept", "application/json");
                    request.Headers.Add("cache-cnotrol", "no-cache");
                    
                    string body = TokenBody(setting);

                    StringContent content = new StringContent(body, Encoding.UTF8, "application/x-www-form-urlencoded");

                    using var response = await client.PostAsync(url, content);
                    response.EnsureSuccessStatusCode();
                    string result = await response.Content.ReadAsStringAsync();
                    tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(result);
                    expries = tokenResponse.expires_in;

                    return tokenResponse.access_token;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Access_Token Error " + ex.Message);
                }
            }

            return tokenResponse.access_token;
        }

        public string TokenBody(Setting setting)
        {
            string paramsKeys;

            paramsKeys = "client_id=" + setting.ClientID + "&client_secret=" + setting.ClientSecret + "&grant_type=client_credentials";

            return paramsKeys;
        }

    }
}
