using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using System.IO;
using System.Security.Cryptography;

namespace Sycade.BunqApi.Extensions
{
    public static class RSAExtensions
    {
        public static string ToPublicKeyPemString(this RSA rsa)
        {
            return ToPemString(DotNetUtilities.GetRsaPublicKey(rsa));
        }

        public static string ToPrivateKeyPemString(this RSA rsa)
        {
            return ToPemString(DotNetUtilities.GetRsaKeyPair(rsa));
        }

        public static RSA FromPublicKeyPemString(string pemString)
        {
            using (var pemStringReader = new StringReader(pemString))
            {
                var pemReader = new PemReader(pemStringReader);

                var rsaKeyParameters = (RsaKeyParameters)pemReader.ReadObject();

                return DotNetUtilities.ToRSA(rsaKeyParameters);
            }
        }


        private static string ToPemString<TObject>(TObject obj)
        {
            using (var pemStringWriter = new StringWriter())
            {
                var pemWriter = new PemWriter(pemStringWriter);
                pemWriter.WriteObject(obj);
                pemWriter.Writer.Flush();

                return pemStringWriter.ToString();
            }
        }
    }
}
