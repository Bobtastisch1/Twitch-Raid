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

        public void WriteFile(string filePath)
        {
            using (StreamWriter sw = new(filePath, false))
            {
                sw.WriteLine("ClientId:");
                sw.WriteLine("ClientSecret:");
                sw.WriteLine("YourStreamerName:");
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
                        }
                    }
                }
            }
            catch (Exception e)
            {
                // Handle any errors that might occur
                Console.WriteLine("Error: " + e.Message);
            }
            return setting;
        }
    }
}
