using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Errata.Text
{
   public static class StringsExtensions
    {
        public static IEnumerable<string> RemoveEmpty(this IEnumerable<string> lines, int maxBlankLines)
        {
            var reduced = new List<string>();
            var counter = 0;
            foreach (var line in lines)
            {
                 if (line.HasValue() || counter < maxBlankLines)
                     reduced.Add(line);

                if (line.HasValue())
                    counter = 0;
                else
                    counter++;
            }
            return reduced;
        }

        public static IEnumerable<string> RemoveEmpty(this IEnumerable<string> lines)
        {
            return lines.Where(line => line.HasValue()).ToList();
        }
    }
}
