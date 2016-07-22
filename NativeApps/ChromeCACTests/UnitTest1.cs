using System;
using ChromeCAC;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Security.Cryptography;

namespace ChromeCACTests
{
    [TestClass]
    public class UnitTest1
    {
        //MG: I signed "foobar" from ChromeCAC with my CAC.
        public SignatureResponse ValidSig
        {
            get
            {
                return new SignatureResponse
                {
                    publicKey = "MIIBCgKCAQEAqbiNs4keODB+5DESkQypMEgOFLA3laOqlDxZ8BJvyoUodx8VBnV3KOqY9WT8bPjhnpL7EPjiVM65/m5aDS8npT64bcdHsfAKYpgqrzRXrt119moDEENsr0zFPQ1T0NPtT0pUqbTFqiFENduUChU9d5W3KjAggsSixYaEgbdYw6zBn6OMlF9ZiE9DwU3tyZtK0NGaZfJILHUGL/pW9ZW45W50vZJPByteuVT0TL6pFrFxBDlUNun1TE5wLUIB77ddp0pwJXFUHPEYI26NBInjfHWS7Cuf4+rFNlsD2hfCA8/xKHWad5yF8ALDuda2qhSvITS+Ai5pfryONHQ9xFSnnQIDAQAB",
                    signature = "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"
                };
            }
        }
        
        [TestMethod]
        public void TestValidSignatureWithoutVerifyingCert()
        {
            ContentInfo contentInfo = new ContentInfo(Encoding.UTF8.GetBytes("foobar"));
                  
            SignedCms signedCms = new SignedCms(contentInfo,false);
            
            signedCms.Decode(Convert.FromBase64String(ValidSig.signature));
            
            // Check without verifying cert
            signedCms.CheckSignature(true);
        }

        /// <summary>
        /// If this method fails and TestValidSignatureWithoutVerifyingCert does not,
        /// you are probably missing the DOD certs
        /// Get them at https://militarycac.com/dodcerts.htm
        /// </summary>
        [TestMethod]
        public void TestValidSignatureAndCert()
        {
            ContentInfo contentInfo = new ContentInfo(Encoding.UTF8.GetBytes("foobar"));

            SignedCms signedCms = new SignedCms(contentInfo, false);

            signedCms.Decode(Convert.FromBase64String(ValidSig.signature));

            // Check without verifying cert
            signedCms.CheckSignature(true);

            // Check with verifying cert
            signedCms.CheckSignature(false);
        }

        [TestMethod]
        [ExpectedException(typeof(CryptographicException))]
        public void TestInvalidSignature()
        {
            ContentInfo contentInfo = new ContentInfo(Encoding.UTF8.GetBytes("foobar"));

            SignedCms signedCms = new SignedCms(contentInfo, true);

            // Mess up the signature
            signedCms.Decode(Convert.FromBase64String(ValidSig.signature.Replace('A', 'B')));

            // Check without verifying cert
            signedCms.CheckSignature(true);

            // Should throw an exception and stop
        }

        [TestMethod]
        [ExpectedException(typeof(CryptographicException))]
        public void TestInvalidContent()
        {
            // changed "foobar" to "modified content"
            ContentInfo contentInfo = new ContentInfo(Encoding.UTF8.GetBytes("modified content")); 

            SignedCms signedCms = new SignedCms(contentInfo, true);

            signedCms.Decode(Convert.FromBase64String(ValidSig.signature));

            // Check without verifying cert
            signedCms.CheckSignature(true);

            // Should throw an exception and stop
        }
    }
}
