using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Errata.IO
{
  public static   partial class ByteArrayExtensions
    {
        public static BitmapImage ToBitmap(this Byte[] bytes)
        {
            var bitmap = new BitmapImage();
            var m = new MemoryStream(bytes);
            bitmap.BeginInit();
            bitmap.StreamSource = m;
            bitmap.EndInit();
            return bitmap;
        }

        public static string RipeMd160Hash(this Byte[] bytes)
        {
            var memoryStream = new MemoryStream(bytes);
            return memoryStream.CryptoHash<RIPEMD160Managed>();
        }
    }
}
