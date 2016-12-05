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

                string[] paths = Directory.GetFiles(ROOT_DIRECTORY, "*", SearchOption.AllDirectories);

                Random rand = new Random();
                string selectedPath;

                // select random wallpaper. if thumbs.db or desktop.ini, ignore and try again
                do
                {
                    selectedPath = paths[rand.Next(paths.Length + 1)];

                } while (selectedPath.EndsWith(".ini") || selectedPath.EndsWith(".db"));

                ChangeWallpaper(selectedPath);
            }

            static void ChangeWallpaper(String filePath)
            {

                // registry keys will cause wallpaper to be fit. it works 
                // by performing the neccessary sacrificial rituals in order to summon
                // Bill Gates from his eternal slumber.
                // pulled from here: 
                //  http://stackoverflow.com/questions/1061678/change-desktop-wallpaper-using-code-in-net
                // http://stackoverflow.com/questions/25434659/fit-fill-image

                RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);

                key.SetValue(@"WallpaperStyle", "10");
                key.SetValue(@"TileWallpaper", "0");

                SystemParametersInfo(SPI_SETDESKWALLPAPER,
                0,
                filePath,
                SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
            }
        }
    }
}
