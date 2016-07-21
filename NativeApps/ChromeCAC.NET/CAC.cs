using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ChromeCAC
{
    public class CAC
    {
        //public static X509Certificate2 PickCertificate()
        //{
        //    X509Store my = new X509Store(StoreName.My, StoreLocation.CurrentUser);

        //    my.Open(OpenFlags.ReadOnly);

        //    return X509Certificate2UI.SelectFromCollection(my.Certificates, "Select a certificate", "Choose the certificate you wish to sign with", X509SelectionFlag.SingleSelection)[0];
        //}

        public static SignatureResponse Sign(string text)
        {
            var bytes = Encoding.UTF8.GetBytes(text);

            return Sign(bytes);
        }

        public static SignatureResponse Sign(byte[] data)
        {
            // TODO:
            // padding configuration
            // algorithm configuration
            // encoding configuration
            /*
            SHA1Managed sha1 = new SHA1Managed();
            byte[] hash = sha1.ComputeHash(data);

            var sig = csp.SignHash(hash, CryptoConfig.MapNameToOID("SHA1"));
            //sig = csp.SignData(Encoding.UTF8.GetBytes(text), CryptoConfig.MapNameToOID("SHA1"));

            MessageBox.Show("SignData");
            */

            var content = new ContentInfo(data);
            var cms = new SignedCms(content, true); // TODO detached config
            var signer = new CmsSigner();
            signer.IncludeOption = X509IncludeOption.EndCertOnly;

            cms.ComputeSignature(signer, false);
            var sig = cms.Encode();

            //ensure my signature is correct before continuing.
            cms.CheckSignature(true);
            
            var newCMS = new SignedCms(content, false);
            newCMS.Decode(sig);
            newCMS.CheckSignature(true);

            

            var cert = cms.Certificates[0];
            CheckSig(sig, data);
            return new SignatureResponse
            {
                publicKey = Convert.ToBase64String(cert.PublicKey.EncodedKeyValue.RawData),
                signature = Convert.ToBase64String(sig),
                fullSig = null // TODO
            };
        }

        public static void CheckSig(byte[] sig, byte[] data)
        {
            ContentInfo contentInfo = new ContentInfo(data);

            SignedCms signedCms = new SignedCms(contentInfo, true);

            signedCms.Decode(sig);

            // This checks if the signature is valid, but doensn't actually verify the cert (TODO)
            signedCms.CheckSignature(true);
            
            signedCms.CheckSignature(false);
                 
        }

    }
}
