using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Errata.IO
{
    public static partial class IEnumerableOfFileInfoExtensions
    {
     public static long Length(this IEnumerable<FileInfo> files)
     {
         return files.Sum(file => file.Length);
     }

        public static string Md5Hash(this IEnumerable<FileInfo> files)
        {
            var fileArray = files.ToArray();
            fileArray.SortLikeExplorer();
            var bytes = fileArray.Bytes();
            return bytes.Md5Hash();
        }

        public static string Sha1Hash(this IEnumerable<FileInfo> files)
        {
            var fileArray = files.ToArray();
            fileArray.SortLikeExplorer();
            var bytes = fileArray.Bytes();
            return bytes.Sha1Hash();
        }


        public static string Sha256Hash(this IEnumerable<FileInfo> files)
        {
            var fileArray = files.ToArray();
            fileArray.SortLikeExplorer();
            var bytes = fileArray.Bytes();
            return bytes.Sha256Hash();
        }
        public static string Sha384Hash(this  IEnumerable<FileInfo> files)
        {
            var fileArray = files.ToArray();
            fileArray.SortLikeExplorer();
            var bytes = fileArray.Bytes();
            return bytes.Sha384Hash();
        }
        public static string Sha512Hash(this IEnumerable<FileInfo> files)
        {
            var fileArray = files.ToArray();
            fileArray.SortLikeExplorer();
            var bytes = fileArray.Bytes();
            return bytes.Sha512Hash();
        }
    }
}
