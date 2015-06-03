using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamMarketHelper
{
    public static class ReadAndWriteToFile
    {
        public static string fileName = "LocalHistory.txt";
        public static void writeToFile(string Line)
        {
            if (!File.Exists(fileName))
            {
                File.Create(fileName);
                TextWriter tw = new StreamWriter(fileName);
                tw.WriteLine(Line);
                tw.Close();
            }
            else if (File.Exists(fileName))
            {
                TextWriter tw = new StreamWriter(fileName, true);
                tw.WriteLine(Line);
                tw.Close();
            }
        }

        public static string readFromFile()
        {
            StreamReader readtext = new StreamReader(fileName);
            string readmetext = readtext.ReadLine();
            readtext.Close();
            return readmetext;
        }
    }
}
