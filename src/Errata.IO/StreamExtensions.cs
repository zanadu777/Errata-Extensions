using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Errata.IO
{
    public static class StreamExtensions
    {
        public static byte[] ToArray(this Stream stream)
        {  //Worth Reading: Reading binary data in C# -- http://www.yoda.arachsys.com/csharp/readbinary.html 
            var mStream = stream as MemoryStream;
            if (mStream != null)
                return mStream.ToArray();

            var buffer = new byte[1024];
            using (var memoryStream = new MemoryStream())
            {
                int bytesRead;
                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                    memoryStream.Write(buffer, 0, bytesRead);

                return memoryStream.ToArray();
            }
        }


        internal static string CryptoHash<T>(this Stream stream) where T : HashAlgorithm, new()
        {
            var crypto = new T();
            crypto.ComputeHash(stream);
            stream.Close();

            byte[] result = crypto.Hash;

            var strBuilder = new StringBuilder();
            foreach (byte t in result)
                strBuilder.Append(t.ToString("x2"));

            return strBuilder.ToString();
        }
    }
}
