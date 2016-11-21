using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallpaper_Swapper
{
    using Microsoft.Win32;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading.Tasks;

    namespace RandomWallpaperApplier
    {
        class Runner
        {
            const int SPI_SETDESKWALLPAPER = 20;
            const int SPIF_UPDATEINIFILE = 0x01;
            const int SPIF_SENDWININICHANGE = 0x02;
            const string ROOT_DIRECTORY = "D:/Pictures/Wallpapers";

            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

            static void Main(string[] args)
            {
                string wallpaperDirectory = findWallpapers(ROOT_DIRECTORY);

                changeWallpaper(wallpaperDirectory);

            }

            static string findWallpapers(string rootDirectory)
            {
                Random ranGen = new Random();

                string[] files = Directory.GetFiles(rootDirectory);
                List<string> allPaths = Directory.GetDirectories(rootDirectory).Concat<string>(files)
                                                                               .ToList<string>();

                string randomPath;
                do
                {
                    int randomIndex = ranGen.Next(0, allPaths.Count);
                    randomPath = allPaths[randomIndex];
                }
                // re-roll if mobile or dual wallpaper directory
                while (randomPath.Contains("Dual") || randomPath.Contains("Mobile"));

                // find if directory
                FileAttributes attr = File.GetAttributes(@randomPath);
                // if directory
                if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    return findWallpapers(randomPath);
                }
                // if file
                else
                {
                    return randomPath;
                }

            }

            static void changeWallpaper(String filePath)
            {
                SystemParametersInfo(SPI_SETDESKWALLPAPER,
                0,
                filePath,
                SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
            }
        }
    }
}
