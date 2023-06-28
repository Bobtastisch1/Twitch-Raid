using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TwitchRaid.Models;

namespace TwitchRaid.Handlers
{
    public class TxtFileHandler
    {
        public bool CheckIfFileExists(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return false;
            }

            return true;
        }


        public void CreateTxtFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                using FileStream fs = File.Create(filePath);
            }
        }

        public void WriteFile(string filePath, string name)
        {
            if (name.Contains("Init.txt"))
            {
                using (StreamWriter sw = new(filePath, false))
                {
                    sw.WriteLine("ClientId:");
                    sw.WriteLine("ClientSecret:");
                    sw.WriteLine("YourStreamerName:");
                    sw.WriteLine("oauth:");
                    sw.WriteLine("OnlyFavorite: False");
                }
            }

            if (name.Contains("Ban.txt"))
            {
                using (StreamWriter sw = new(filePath, false))
                {
                    sw.WriteLine("Ban:");
                }
            }

            if (name.Contains("Favorite.txt"))
            {
                using (StreamWriter sw = new(filePath, false))
                {
                    sw.WriteLine("Favorite:");
                }
            }
        }

        public Setting ReadFile(string filePath)
        {
            var setting = new Setting();

            try
            {
                using var sr = new StreamReader(filePath);
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    var keyValue = line.Split(':');

                    if (keyValue.Length == 2)
                    {
                        var key = keyValue[0].Trim();
                        var value = keyValue[1].Trim();
                        switch (key)
                        {
                            case "ClientId":
                                setting.ClientID = value;
                                break;
                            case "ClientSecret":
                                setting.ClientSecret = value;
                                break;
                            case "YourStreamerName":
                                setting.YourStreamerName = value;
                                break;
                            case "oauth":
                                setting.oauth = value;
                                break;
                            case "OnlyFavorite":
                                setting.OnlyFavorite = value;
                                break;
                        }
                    }
                }

                using var srBan = new StreamReader(filePath.Replace("Init.txt", "Ban.txt"));
                setting.Ban = new List<string>();
                setting.Favorite = new List<string>();
                string lineBan;

                srBan.ReadLine(); //Skip First Line
                while ((lineBan = srBan.ReadLine()) != null)
                {
                    setting.Ban.Add(lineBan.Trim());
                }

                using var srFavorite = new StreamReader(filePath.Replace("Init.txt", "Favorite.txt"));
                setting.Ban = new List<string>();
                string lineFavorite;

                srFavorite.ReadLine(); //Skip First Line
                while ((lineFavorite = srFavorite.ReadLine()) != null)
                {
                    setting.Favorite.Add(lineFavorite.Trim());
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
            return setting;
        }
    }
}
