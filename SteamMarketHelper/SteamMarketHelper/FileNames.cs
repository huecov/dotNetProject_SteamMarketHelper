using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamMarketHelper
{
    public class FileNames
    {
        public FileNames() { }

        public string[] retriveFileNames(string pathToDirectory)
        {
            string[] pngFiles = Directory.GetFiles(pathToDirectory, "*.png")
                                     .Select(path => Path.GetFileName(path))
                                     .ToArray();
            for (int i = 0; i < pngFiles.Length; i++ )
            {
                if (pngFiles[i].Contains('.'))
                    pngFiles[i] = pngFiles[i].Substring(0, pngFiles[i].LastIndexOf('.'));
            }

            return pngFiles;
        }
    }
}
