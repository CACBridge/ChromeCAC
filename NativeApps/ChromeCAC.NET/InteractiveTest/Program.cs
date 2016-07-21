using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChromeCAC;
using System.Security.Cryptography.Pkcs;

namespace InteractiveTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = "foobar";
            var response = CAC.Sign(data);

            ContentInfo contentInfo = new ContentInfo(Encoding.UTF8.GetBytes("foobar"));

            //SignedCms signedCms = new SignedCms(contentInfo, true);
            SignedCms signedCms = new SignedCms(contentInfo,true);
            signedCms.Decode(Convert.FromBase64String(response.signature));            

            // This checks if the signature is valid, but doensn't actually verify the cert (TODO)
            signedCms.CheckSignature(true);



            signedCms.CheckSignature(false);

        }
    }
}
