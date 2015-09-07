using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Errata.Text
{
    public static class StringExtensions
    {
        public static string Remove(this string text, string toBeRemoved)
        {
            return text.Replace(toBeRemoved, "");
        }

        public static string RemoveRegex(this string text, Regex rx)
        {
            return rx.Replace(text, "");
        }

        public static bool HasValue(this string text)
        {
            return !string.IsNullOrWhiteSpace(text);
        }

        public static string Repeat(this string text, int repetitions)
        {
            if (repetitions > 1)
            {
                var sb = new StringBuilder(text);
                for (int i = 0; i < repetitions; i++)
                {
                    sb.Append(text);
                }
                return sb.ToString();
            }
            return text;

        }


        public static List<string> Lines(this string text)
        {
            var lines = new List<string>();


            using (var sr = new StringReader(text))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                    lines.Add(line);
            }
            return lines;
        }

        public static  int LineCount(this string text)
        {
            var counter = 0;
            using (var sr = new StringReader(text))
            {
                while ((sr.ReadLine()) != null)
                    counter ++;
            }
            return counter;
        }

        public static string FirstToLower(this string text)
        {
            return text.Substring(0, 1).ToLower() + text.Substring((1));
        }

        public static string FirstToUpper(this string text)
        {
            return text.Substring(0, 1).ToUpper() + text.Substring((1));
        }



    }
}
