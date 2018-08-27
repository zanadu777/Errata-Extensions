using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Windows.Media.Imaging;
using Errata.Text;

namespace Errata.IO
{
  public static partial  class FileInfoExtensions
    {
        public static BitmapImage ToBitmap(this FileInfo fileInfo)
        {

            var bitmap = new BitmapImage();
            if (!fileInfo.Exists) return bitmap;

            var bytes = fileInfo.ReadAllBytes();
            var m = new MemoryStream(bytes);
            bitmap.BeginInit();
            bitmap.StreamSource = m;
            bitmap.EndInit();
            return bitmap;
        }

        public static string RipeMd160Hash(this FileInfo fileInfo)
        {
            return fileInfo.OpenRead().CryptoHash<RIPEMD160Managed>();
        }

        public static string Hash(this FileInfo fileInfo, EHashCode hashCode)
        {
            switch (hashCode)
            {
                case EHashCode.Md5:
                    return fileInfo.OpenRead().CryptoHash<MD5CryptoServiceProvider>();

                case EHashCode.Sha1:
                    return fileInfo.Sha1Hash();
                case EHashCode.RipeMd160:
                    return fileInfo.RipeMd160Hash();
                case EHashCode.Sha256:
                    return fileInfo.Sha256Hash();
                case EHashCode.Sha384:
                    return fileInfo.Sha384Hash();
                case EHashCode.Sha512:
                    return fileInfo.Sha512Hash();
                default:
                    throw new ArgumentOutOfRangeException(nameof(hashCode), hashCode, null);
            }
        }
    }
}
