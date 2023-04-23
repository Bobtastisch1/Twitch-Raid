using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchRaid
{
    public class ReadTxtFile
    {
        public static string ReadFile(string filePath)
        {
            string content = "";

            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    content = sr.ReadToEnd();
                }
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
