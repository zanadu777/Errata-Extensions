using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace Errata.IO
{
   public static partial class DirectoryInfoExtensions
    {

        public static string RipeMd160Hash(this DirectoryInfo directory)
        {
            return ExplorerSortedBytes(directory).RipeMd160Hash();
        }
    }
}
