using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Windows_.NET_Installer
{
    class Program
    {
        const string ExtensionID = "";
        const string RegistryPath = "";
        static string NativeAppPath = "";

        static void Main(string[] args)
        {
            Console.WriteLine("Beginning install");
            // SetupExtension();
            SetupNativeApp();
        }

        static void SetupExtension()
        {
            //download from github
            WebClient Client = new WebClient();
            string tempFile = Path.GetTempFileName();
            Client.DownloadFile("https://cdn.rawgit.com/CACBridge/ChromeCAC/master/ChromeExtensionReleases/v0.1/ChromeExtension.crx", tempFile);

            Console.WriteLine("{0}", tempFile);
            //put it folder
            string newDir = @"C:\\User\\ChromeCAC\\extensionInstaller";
            Directory.Move(tempFile, newDir);

        }

        static void SetupNativeApp()
        {
            //intsall chromcac.net into appdata, registry points to this
            WebClient Client = new WebClient();
            string tempFile = Path.GetTempFileName();
            Client.DownloadFile("https://cdn.rawgit.com/CACBridge/ChromeCAC/tree/master/NativeApps/ChromeCAC.NET", tempFile); // DOES NOT WORK ZIP IT OR SOMETHING NEED DOWNLOAD DIRECTORy
            Console.WriteLine("{0}", tempFile);
            string newDir = @"C:\\";

            Directory.Move(tempFile, newDir);

            //edit manifest to contain path to exe, needs to be used for registry
            //comeback later lol

            string tempFile2 = Path.GetTempFileName();
            Client.DownloadFile("https://cdn.rawgit.com/CACBridge/ChromeCAC/master/Install/Windows/manifest.json", tempFile2);
            Console.WriteLine("{0}", tempFile2);
            string newDir2 = @"C:\\";

            Directory.Move(tempFile2, newDir2);
        }

        static void SetupRegistry()
        {
            // look at resitry.reg
            WebClient Client = new WebClient();
            string tempFile = Path.GetTempFileName();
            Client.DownloadFile("https://cdn.rawgit.com/CACBridge/ChromeCAC/master/Install/Windows/registry.reg", tempFile);
            Console.WriteLine("{0}", tempFile);

            string newDir = @"C:\\regisrty";

            Directory.Move(tempFile, newDir);
            StringBuilder newFile = new StringBuilder();
            string temp = "";
            string[] file = File.ReadAllLines(newDir);
            string manifestLocation = "";       //TODO save manifest somewhere
            foreach (string line in file)
            {
                if (line.Contains("@="))
                {
                    temp = line.Replace("@=\"C:\\PATH\\TO\\manifest.json\"", "@=" + manifestLocation);
                    newFile.Append(temp + "\r\n");

                    continue;
                }

            }
        }
    }
}
