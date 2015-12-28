using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Errata.Text
{
    public static class RegexExtensions
    {
        public static string GroupValue(this Regex regex, string text, string groupName)
        {
            Match m = regex.Match(text);
            var extracted = m.Groups[groupName].Value;
            return extracted;
        }


        public static string GroupValue(this Regex regex, string text, int groupIndex)
        {
            Match m = regex.Match(text);
            var extracted = m.Groups[groupIndex].Value;
            return extracted;
        }


        public static List<string> GroupValues(this Regex regex, string text, string groupName)
        {
            var matches = regex.Matches(text);
            var values = new List<string>();
            foreach (Match m in matches)
            {
                var extracted = m.Groups[groupName].Value;
                if (!string.IsNullOrWhiteSpace(extracted))
                    values.Add(extracted);
            }

            return values;
        }


        public static List<string> GroupValues(this Regex regex, string text, int groupIndex)
        {
            var matches = regex.Matches(text);
            var values = new List<string>();
            foreach (Match m in matches)
            {
                var extracted = m.Groups[groupIndex].Value;
                if (!string.IsNullOrWhiteSpace(extracted))
                    values.Add(extracted);
            }

            return values;
        }
    }
}
