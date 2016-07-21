using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ValidationTest
{
    public partial class Verify : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected string CheckSig()
        {
            var formData = Request.Form;
            var text = formData["txtSign"];
            var sig = formData["txtSig"];

            string output = "INVALID!";

            if (!string.IsNullOrEmpty(sig))
            {
                try
                {
                    ContentInfo contentInfo = new ContentInfo(Encoding.UTF8.GetBytes(text));

                    SignedCms signedCms = new SignedCms(contentInfo, true);

                    signedCms.Decode(Convert.FromBase64String(sig));

                    // This checks if the signature is valid, but doensn't actually verify the cert (TODO)
                    signedCms.CheckSignature(true);

                    output = "Signature valid.";

                    signedCms.CheckSignature(false);

                    output += "<br>Cert valid";
                }
                catch (Exception e)
                {
                    output += "<br>" + e.ToString();
                }
            }

            return output;
        }
    }
}