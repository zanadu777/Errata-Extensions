using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Errata.IO
{
  public static   partial class IEnumerableOfFileInfoExtensions
    {
        public static string RipeMd160Hash(this IEnumerable<FileInfo> files)
        {
            var fileArray = files.ToArray();
            fileArray.SortLikeExplorer();
            var bytes = fileArray.Bytes();
            return bytes.RipeMd160Hash();
        }
    }
}
