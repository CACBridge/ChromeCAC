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
    class CAC
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
            var cms = new SignedCms(content, false); // TODO detached config
            var signer = new CmsSigner();
            signer.IncludeOption = X509IncludeOption.EndCertOnly;

            cms.ComputeSignature(signer, false);
            var sig = cms.Encode();

            var cert = cms.Certificates[0];

            return new SignatureResponse
            {
                publicKey = Convert.ToBase64String(cert.PublicKey.EncodedKeyValue.RawData),
                signature = Convert.ToBase64String(sig),
                fullSig = null // TODO
            };
        }
    }
}
