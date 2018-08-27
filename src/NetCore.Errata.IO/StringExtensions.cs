using System.IO;
using System.Security.Cryptography;

namespace Errata.IO
{
    public static partial class StringExtensions
    {
        public static Stream ToStream(this string text)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(text);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        public static string Md5Hash(this string text)
        {
            var stream = text.ToStream();
            return stream.CryptoHash<MD5CryptoServiceProvider>();
        }

        public static string Sha1Hash(this string text)
        {
            var stream = text.ToStream();
            return stream.CryptoHash<SHA1CryptoServiceProvider>();
        }

 
        public static string Sha256Hash(this string text)
        {
            var stream = text.ToStream();
            return stream.CryptoHash<SHA256CryptoServiceProvider>();
        }
        public static string Sha384Hash(this string text)
        {
            var stream = text.ToStream();
            return stream.CryptoHash<SHA384CryptoServiceProvider>();
        }
        public static string Sha512Hash(this string text)
        {
            var stream = text.ToStream();
            return stream.CryptoHash<SHA512CryptoServiceProvider>();
        }

    }
}
