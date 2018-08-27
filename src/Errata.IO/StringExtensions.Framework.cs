using System.IO;
using System.Security.Cryptography;


namespace Errata.IO
{
  public static partial   class StringExtensions
    {
        public static string RipeMd160Hash(this string text)
        {
            var stream = text.ToStream();
            return stream.CryptoHash<RIPEMD160Managed>();
        }
    }
}
