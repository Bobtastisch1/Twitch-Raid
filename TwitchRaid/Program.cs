using TwitchRaid.Controller;
using TwitchRaid.Handlers;
using TwitchRaid.Models;

namespace TwitchRaid
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Setting setting = Setup();
            
            Program program = new();

            Token token = new ();

            string tokennew = token.Tokenhandler(setting).Result;

            GetUser users = new();

            List<User> usersnew = users.GetUsers(setting, tokennew).Result;
        }

        private static Setting Setup()
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

    }
}