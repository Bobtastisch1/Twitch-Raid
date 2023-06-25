using System.Collections.Generic;
using System.Linq;
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


            List <User> userId = users.GetUsers(setting, tokennew).Result;

            start.EveryChannelFollower(setting, tokennew);
        }

        private Setting Setup()
        {
            Console.ReadLine();
            string initPath = AppDomain.CurrentDomain.BaseDirectory + "Init.txt";
            string StreamerBanList = AppDomain.CurrentDomain.BaseDirectory + "Ban.txt";

            TxtFileHandler filehandler = new();

            if (!filehandler.CheckIfFileExists(initPath))
            {
                filehandler.CreateTxtFile(initPath);
                filehandler.WriteFile(initPath, "Init.txt");
            }

            if (!filehandler.CheckIfFileExists(StreamerBanList))
            {
                filehandler.CreateTxtFile(StreamerBanList);
                filehandler.WriteFile(StreamerBanList, "Ban.txt");
            }

            Setting setting = filehandler.ReadFile(initPath);

            if(setting.oauth == "" || setting.ClientSecret == "" || setting.ClientID == "" || setting.YourStreamerName == "")
            {
                Console.WriteLine("Fill in the Informations currectly in the Init.txt File ");
                Console.ReadLine();
            }

            return setting;
        }

        private async void EveryChannelFollower(Setting setting, string token)
        {
            GetChannelFollower followers = new();
            FollowerList followerlist = new();

            followerlist.follower  = followers.GetChannelFollowers(setting).Result;

            if(followerlist.follower == null  || followerlist.follower.Count == 0)
            {
                Console.WriteLine("You have No Followers o.O");
                Console.ReadLine();
            }

            BanRemove(setting, followerlist);

            StreamersLive(setting, token, followerlist);
        }

        private void BanRemove(Setting setting, FollowerList followerlist)
        {
            List<Follower> followersToRemove = new List<Follower>();

            foreach (var follower in followerlist.follower)
            {
                if (setting.Ban.Contains(follower.user_name))
                {
                    followersToRemove.Add(follower);
                }
            }

            foreach (var followerToRemove in followersToRemove)
            {
                followerlist.follower.Remove(followerToRemove);
            }
        }

        private void StreamersLive(Setting setting, string token, FollowerList followerlist)
        {
            GetStreamers streams = new();
            LiveStreamList liveStreamList = new ();
            liveStreamList.streamers = streams.GetLiveStreams(setting, token, followerlist).Result;

            if (liveStreamList.streamers == null || liveStreamList.streamers.Count == 0)
            {
                Console.WriteLine("No Body to Raid - _-");
                Console.ReadLine();
            }

             SelectRandomStreamer(setting, liveStreamList);
        }

        private async Task SelectRandomStreamer(Setting setting, LiveStreamList liveStreamList)
        {
            Random random = new ();
            int selectedStreamer = random.Next(liveStreamList.streamers.Count);

            string selectedStreamerUserName = liveStreamList.streamers[selectedStreamer].user_name;
            Console.WriteLine("Raiding: " + selectedStreamerUserName);

            PostRaid postRaid = new PostRaid();

            var raidList = postRaid.PostRaids(setting, liveStreamList.streamers[selectedStreamer]).Result;

            Console.ReadLine();
        }

    }
}