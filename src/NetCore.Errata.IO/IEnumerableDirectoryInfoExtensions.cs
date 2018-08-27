using System.Collections.Generic;
using System.IO;

namespace Errata.IO
{
    public static class IEnumerableDirectoryInfoExtensions
    {
        public static List<FileInfo> GetFiles(this IEnumerable<DirectoryInfo> directories)
        {
            var files = new List<FileInfo>();
            foreach (var dir in directories)
            {
                files.AddRange(dir.GetFiles());
            }
            return files;
        }
    }
}
