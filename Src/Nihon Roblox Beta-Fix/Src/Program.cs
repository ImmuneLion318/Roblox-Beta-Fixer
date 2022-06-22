using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Diagnostics;
using System.Threading;
using System.IO.Compression;
using Nihon.Utility;

namespace Nihon
{
    internal class Program
    {
        static string Watermark = @" 
   _   _ _ _                 
  | \ | (_) |                
  |  \| |_| |__   ___  _ __  
  | . ` | | '_ \ / _ \| '_ \ 
  | |\  | | | | | (_) | | | |
  \_| \_/_|_| |_|\___/|_| |_|
                             
                           ";
        static void Main(string[] args)
        {
            Console.Title = "Nihon Roblox Beta Fix";
            Console.SetWindowSize(80, 20);

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(Watermark);
            Console.ForegroundColor = ConsoleColor.White;

            foreach (var Proc in Process.GetProcessesByName("RobloxPlayerBeta"))
                Proc.Kill();

            using (var Http = new WebClient { Proxy = null })
            {
                string Path = null, Version = Http.DownloadString("http://setup.rbxcdn.com/version");

                Utilities.Write("Latest Roblox Version : " + Version);

                string[] Directories = Directory.GetDirectories(Environment.GetEnvironmentVariable("LocalAppData") + "\\Roblox\\Versions");
                foreach (string Files in Directories)
                {
                    /* Thanks To Iskra For Correcting Me On This, The Else Statement Was Redundant. */
                    if (!File.Exists($"{Files}\\RobloxPlayerBeta.exe"))
                    {
                        Utilities.Write("Couldn't Find RobloxPlayerBeta.exe", true);
                        Thread.Sleep(3000);
                        Process.GetCurrentProcess().Kill();
                        break;
                    }

                    Path = Files;
                }
                string ExecutablePath = Path + "\\RobloxPlayerBeta.exe";

                Utilities.Write("Roblox Path : " + Path, true);
                Utilities.Write("Downloading Latest Roblox Client", true);

                Http.DownloadFile($"http://setup.roblox.com/{Version}-RobloxApp.zip", Path + "\\Temp.zip");

                if (File.Exists(ExecutablePath) && File.Exists(Path + "\\COPYRIGHT.txt"))
                {
                    File.Delete(ExecutablePath);
                    File.Delete(Path + "\\COPYRIGHT.txt");
                    Utilities.Write("Deleted Outdates Files.", true);
                }

                ZipFile.ExtractToDirectory(Path + "\\Temp.zip", Path);
                Utilities.Write("Finished Installation, Please Run Roblox And See If This Fix Worked.");
                /* Doesn't Delete Temp.zip Was Too Lazy To Do This Lol. */
            }
        }
    }
}
