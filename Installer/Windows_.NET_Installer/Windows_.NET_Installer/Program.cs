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
            SetupExtension();
        }

        static void SetupExtension()
        {
            //download from github
            WebClient Client = new WebClient();
            string tempFile = Path.GetTempFileName();
            Client.DownloadFile("https://cdn.rawgit.com/CACBridge/ChromeCAC/master/ChromeExtensionReleases/v0.1/ChromeExtension.crx", tempFile);

            Console.WriteLine("{0}",tempFile);
            //put it folder
            string newDir = @"C:\\User\\ChromeCAC\\extensionInstaller";
            Directory.Move(tempFile, newDir);

        }

        static void SetupNativeApp()
        {
            //intsall chromcac.net into appdata, registry points to this
            
            
            //edit manifest to contain path to exe, needs to be used for registry

        }

        static void SetupRegistry()
        {
            // look at resitry.reg

        }
    }
}
