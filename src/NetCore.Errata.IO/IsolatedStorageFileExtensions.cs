﻿using System.IO;
using System.IO.IsolatedStorage;
using System.Text;

namespace Errata.IO
{
    public static class IsolatedStorageFileExtensions
    {
        public static string ReadAllText(this IsolatedStorageFile storageFile, string path)
        {
            return ReadAllText(storageFile, path, Encoding.Default);
        }


        public static string ReadAllText(this IsolatedStorageFile storageFile, string path, Encoding encoding)
        {
            var text = "";
            if (!storageFile.FileExists(path)) return null;

            using (var file = storageFile.OpenFile(path, FileMode.Open, FileAccess.Read))
            {
                using (var reader = new StreamReader(file, encoding))
                {
                    text = reader.ReadToEnd();
                }
            }
            return text;
        }






        public static void WriteAllText(this IsolatedStorageFile storageFile, string path, string text)
        {
            WriteAllText(storageFile, path, text, Encoding.Default);
        }


        public static void WriteAllText(this IsolatedStorageFile storageFile, string path, string text, Encoding encoding)
        {
            using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream(path, FileMode.Create, storageFile))
            {
                StreamWriter writer = new StreamWriter(stream, encoding);
                writer.Write(text);
                writer.Close();
            }
        }
    }
}
