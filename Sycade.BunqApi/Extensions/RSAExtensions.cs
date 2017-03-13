using Sycade.BunqApi.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace Sycade.BunqApi.Extensions
{
    public static class RSAExtensions
    {
        private const int PemHeaderLength = 33;
        private const int ExponentLength = 3;
        private const int PemLineLength = 64;

        public static string ToPemString(this RSA rsa)
        {
            var data = GeneratePemData(rsa);

            var base64Data = Convert.ToBase64String(data.ToArray());

            var lines = new List<string>();

            for (int i = 0; i < base64Data.Length; i += PemLineLength)
                lines.Add(new string(base64Data.Skip(i).Take(PemLineLength).ToArray()));

            return $"-----BEGIN PUBLIC KEY-----\n{string.Join("\n", lines)}\n-----END PUBLIC KEY-----\n";
        }

        public static void FromPemString(this RSA rsa, string pemString)
        {
            var lines = pemString.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            lines.Remove(lines.First());
            lines.Remove(lines.Last());

            var base64Data = string.Join("", lines);
            var data = Convert.FromBase64String(base64Data);
            var keySizeBytes = GetKeySize(data);

            var modulus = data.Skip(PemHeaderLength).Take(keySizeBytes);
            var exponent = data.Reverse().Take(ExponentLength).Reverse();

            var rsaParams = new RSAParameters
            {
                Exponent = exponent.ToArray(),
                Modulus = modulus.ToArray()
            };

            rsa.ImportParameters(rsaParams);
        }


        private static byte[] GeneratePemData(RSA rsa)
        {
            var pemHeader = new List<byte>();
            var keySizeBytes = rsa.KeySize / 8;
            var rsaParams = rsa.ExportParameters(false);

            pemHeader.AddRange(new byte[] { 0x30, 0x82 });
            pemHeader.AddRange(BigEndianBitConverter.GetBytes((short)(keySizeBytes + 34)));
            pemHeader.AddRange(new byte[] { 0x30, 0x0d, 0x06, 0x09, 0x2a, 0x86, 0x48, 0x86, 0xf7, 0x0d, 0x01, 0x01, 0x01, 0x05, 0x00 });
            pemHeader.AddRange(new byte[] { 0x03, 0x82 });
            pemHeader.AddRange(BigEndianBitConverter.GetBytes((short)(keySizeBytes + 15)));
            pemHeader.AddRange(new byte[] { 0x00, 0x30, 0x82 });
            pemHeader.AddRange(BigEndianBitConverter.GetBytes((short)(keySizeBytes + 10)));
            pemHeader.AddRange(new byte[] { 0x02, 0x82 });
            pemHeader.AddRange(BigEndianBitConverter.GetBytes((short)(keySizeBytes + 1)));
            pemHeader.Add(0x00);
            pemHeader.AddRange(rsaParams.Modulus);
            pemHeader.AddRange(new byte[] { 0x02, (byte)rsaParams.Exponent.Length });
            pemHeader.AddRange(rsaParams.Exponent);

            return pemHeader.ToArray();
        }

        private static int GetKeySize(byte[] pemData) => BigEndianBitConverter.ToInt16(new byte[] { pemData[30], pemData[31] }, 0) - 1;
    }
}
