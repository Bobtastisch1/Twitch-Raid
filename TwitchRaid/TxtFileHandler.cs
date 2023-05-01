using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace TwitchRaid
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
            if(!File.Exists(filePath))
            {
                using FileStream fs = File.Create(filePath);
            }
        }

        public void WriteFile(string filePath)
        {
            using(StreamWriter sw = new(filePath, false))
            {
                sw.WriteLine("ID:");
                sw.WriteLine("PW:");
            }
        }

        public string ReadFile(string filePath)
        {
            string content = "";

            try
            {
                using StreamReader sr = new(filePath);
                content = sr.ReadToEnd();
            }
            catch (Exception e)
            {
                // Handle any errors that might occur
                Console.WriteLine("Error: " + e.Message);
            }

            return content;
        }
    }
}
