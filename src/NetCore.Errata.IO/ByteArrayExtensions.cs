using System;
using System.IO;
using System.Security.Cryptography;


namespace Errata.IO
{
    public static partial class ByteArrayExtensions
    {


        public static MemoryStream ToMemoryStream(this Byte[] bytes)
        {
            var stream = new MemoryStream(bytes);
            return stream;
        }

        public static string Md5Hash(this Byte[] bytes)
        {
            var memoryStream = new MemoryStream(bytes);
            return memoryStream.CryptoHash<MD5CryptoServiceProvider>();
        }

        public static string Sha1Hash(this Byte[] bytes)
        {
            var memoryStream = new MemoryStream(bytes);
            return memoryStream.CryptoHash<SHA1CryptoServiceProvider>();
        }

        public static string Sha256Hash(this Byte[] bytes)
        {
            var memoryStream = new MemoryStream(bytes);
            return memoryStream.CryptoHash<SHA256CryptoServiceProvider>();
        }
        public static string Sha384Hash(this Byte[] bytes)
        {
            var memoryStream = new MemoryStream(bytes);
            return memoryStream.CryptoHash<SHA384CryptoServiceProvider>();
        }
        public static string Sha512Hash(this Byte[] bytes)
        {
            var memoryStream = new MemoryStream(bytes);
            
            return memoryStream.CryptoHash<SHA512CryptoServiceProvider>();
        }
    }
}
