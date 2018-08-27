using System.Text;

namespace Errata.Text
{
    public static class StringBuilderExtensions
    {
        public static StringBuilder RemoveRight(this StringBuilder sb, int count)
        {
            return sb.Remove(sb.Length - count, count);
        }


        public static StringBuilder RemoveLeft(this StringBuilder sb, int count)
        {
            return sb.Remove(0, count);
        }
    }
}
