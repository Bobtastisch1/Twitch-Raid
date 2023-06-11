using System.Security.Cryptography.X509Certificates;
using TwitchRaid.Controller;
using TwitchRaid.Handlers;
using TwitchRaid.Models;

namespace TwitchRaid
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Program start = new();
            
            Setting setting = start.Setup();

            Token token = new ();

            string tokennew = token.Tokenhandler(setting).Result;

            GetUser users = new();

            List<User> userId = users.GetUsers(setting, tokennew).Result;

            start.EveryChannelFollower(setting);
        }

        private Setting Setup()
        {
            string initPath = AppDomain.CurrentDomain.BaseDirectory + "Init.txt";
            TxtFileHandler filehandler = new();

            if (!filehandler.CheckIfFileExists(initPath))
            {
                filehandler.CreateTxtFile(initPath);
                filehandler.WriteFile(initPath);
            }

            Setting setting = filehandler.ReadFile(initPath);
            return setting;
        }

        private void EveryChannelFollower(Setting setting)
        {
            GetChannelFollower followers = new();

            List<Follower> follwerslogin = followers.GetChannelFollowers(setting).Result;
        }

    }
}