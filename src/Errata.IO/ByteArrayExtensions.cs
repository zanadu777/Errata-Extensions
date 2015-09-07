using System;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Media.Imaging;

namespace Errata.IO
{
    public static class ByteArrayExtensions
    {
        public static BitmapImage ToBitmap(this Byte[] bytes)
        {
            var bitmap = new BitmapImage();
            var m = new MemoryStream(bytes);
            bitmap.BeginInit();
            bitmap.StreamSource = m;
            bitmap.EndInit();
            return bitmap;
        }

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

        public static string RipeMd160Hash(this Byte[] bytes)
        {
            var memoryStream = new MemoryStream(bytes);
            return memoryStream.CryptoHash<RIPEMD160Managed>();
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
