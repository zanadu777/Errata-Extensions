using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Errata.IO
{
    public static partial class FileInfoExtensions
    {

        public static string Hash(this FileInfo fileInfo, EHashCode hashCode)
        {
            switch (hashCode)
            {
                case EHashCode.Md5:
                    return fileInfo.OpenRead().CryptoHash<MD5CryptoServiceProvider>();

                case EHashCode.Sha1:
                    return fileInfo.Sha1Hash();
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
