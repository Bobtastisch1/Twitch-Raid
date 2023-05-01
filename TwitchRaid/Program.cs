namespace TwitchRaid
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Setup();
        }

        private static void Setup()
        {
            string initPath = "G://TwitchRaid//TwitchRaid//TwitchRaid//Init.txt";
            TxtFileHandler filehandler = new();

            if (!filehandler.CheckIfFileExists(initPath))
            {
                filehandler.CreateTxtFile(initPath);
                filehandler.WriteFile(initPath);
            }

        }
    }
}