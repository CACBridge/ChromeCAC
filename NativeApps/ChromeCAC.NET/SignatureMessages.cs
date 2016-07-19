using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChromeCAC
{
    public class SignatureRequest
    {
        public string data { get; set; }
    }

    public class SignatureResponse
    {
        public string signature { get; set; }

        public string publicKey { get; set; }

        public string fullSig { get; set; }
    }
}
