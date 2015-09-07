using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Windows.Media.Imaging;
using Errata.Text;

namespace Errata.IO
{
    public static class FileInfoExtensions
    {
        public static BitmapImage ToBitmap(this FileInfo fileInfo)
        {

            var bitmap = new BitmapImage();
            if (!fileInfo.Exists) return bitmap;

            var bytes = fileInfo.ReadAllBytes();
            var m = new MemoryStream(bytes);
            bitmap.BeginInit();
            bitmap.StreamSource = m;
            bitmap.EndInit();
            return bitmap;
        }

        public static Byte[] ReadAllBytes(this FileInfo fileInfo)
        {
            return File.ReadAllBytes(fileInfo.FullName);
        }

        public static string ReadAllText(this FileInfo fileInfo)
        {
            return File.ReadAllText(fileInfo.FullName);
        }

        public static string[] ReadAllLines(this FileInfo fileInfo)
        {

            return File.ReadAllLines(fileInfo.FullName);
        }

        public static string NameWithoutExtension(this FileInfo fileInfo)
        {
            try
            {
                var fullName = fileInfo.FullName;

                return Path.GetFileNameWithoutExtension(fullName);

            }
            catch (Exception ex)
            {

            }
            return "";

        }

        public static FileInfo AppendLine(this FileInfo fileinfo, string text)
        {
            var lines = new string[] { text };
            File.AppendAllLines(fileinfo.FullName, lines);

            return fileinfo;
        }

        public static bool HasBitmapExtension(this FileInfo fileInfo)
        {

            var imageExtensions = new List<string> { ".png", ".jpg" };
            return imageExtensions.Contains(fileInfo.Extension);
        }

        public static FileInfo Increment(this FileInfo fileInfo)
        {
            var path = fileInfo.FullName;

            var numberFinder = @"(?<number>\d*)\..*?$";
            var rx = new Regex(numberFinder);
            var m = rx.Match(path);
            var numberstring = m.Groups["number"].Value;
            if (numberstring.HasValue())
            {
                var number = int.Parse(numberstring);
                var next = number + 1;
                var nextString = number.ToString();
                var zerosToPad = numberstring.Length - nextString.Length;
                if (zerosToPad == 1)
                {
                    nextString = "0" + nextString;
                }
                else if (zerosToPad > 1)
                {
                    var padding = "0".Repeat(zerosToPad - 1);
                    nextString = padding + nextString;
                }
                var nextPath = rx.Replace(path, nextString) + Path.GetExtension(path);
                return new FileInfo(nextPath);

            }
            else
            {
                var nextPath = rx.Replace(path, "2") + Path.GetExtension(path);
                return new FileInfo(nextPath);
            }


        }

        public static FileInfo Rename(this FileInfo fileinfo, string name, bool ignoreExtension = true)
        {
            var oldPath = fileinfo.FullName;
            var newPath = FileName.Rename(fileinfo.FullName, name, ignoreExtension);


            if (oldPath == newPath)
                return fileinfo;

            if (oldPath.ToUpper() == newPath.ToUpper()) //needed as file system is case insensitive
            {
                var parent = fileinfo.Directory;
                var tempPath = parent.GetAvailablePAth();

                File.Move(oldPath, tempPath);
                File.Move(tempPath, newPath);
            }
            else
            {
                if (File.Exists(newPath))
                    return fileinfo;

                fileinfo.MoveTo(newPath);
            }

            return new FileInfo(newPath);
        }

        public static FileInfo Rename(this FileInfo fileinfo, Func<string, string> transformation, bool ignoreExtension = true)
        {
            var oldPath = fileinfo.FullName;
            var newPath = FileName.Rename(oldPath, transformation, ignoreExtension);

            if (oldPath == newPath)
                return fileinfo;

            if (oldPath.ToUpper() == newPath.ToUpper()) //needed as file system is case insensitive
            {
                var parent = fileinfo.Directory;
                var tempPath = parent.GetAvailablePAth();

                File.Move(oldPath, tempPath);
                File.Move(tempPath, newPath);
            }
            else
            {
                if (File.Exists(newPath))
                    return fileinfo;

                fileinfo.MoveTo(newPath);
            }

            return new FileInfo(newPath);
        }

        public static FileInfo MoveTo(this FileInfo fileinfo, DirectoryInfo directory, bool overWriteExisting = false)
        {

            var fileName = Path.GetFileName(fileinfo.FullName);
            var dirPath = directory.FullName;

            if (Path.GetDirectoryName(fileName) == dirPath && !overWriteExisting) //already there
                return fileinfo;

            var newPath = Path.Combine(dirPath, fileName);

            if (File.Exists(newPath))
                return fileinfo;

            fileinfo.MoveTo(newPath);
            return new FileInfo(newPath);
        }

        public static FileInfo CopyTo(this FileInfo fileinfo, DirectoryInfo directory, bool overWriteExisting = false)
        {

            var fileName = Path.GetFileName(fileinfo.FullName);
            var dirPath = directory.FullName;

            if (Path.GetDirectoryName(fileName) == dirPath && !overWriteExisting) //already there
                return fileinfo;

            var newPath = Path.Combine(dirPath, fileName);

            if (File.Exists(newPath))
                return fileinfo;

            fileinfo.CopyTo(newPath);
            return new FileInfo(newPath);
        }


        public static string RelativePath(this FileInfo fileinfo, DirectoryInfo directory)
        {
            var root = directory.FullName;
            var path = fileinfo.FullName;

            if (!path.StartsWith(root)) return path;

            var length = (root.EndsWith(@"\")) ? root.Length : root.Length + 1;
            return path.Substring(length);
        }

        #region Hashing
        public static string Md5Hash(this FileInfo fileInfo)
        {
            return fileInfo.OpenRead().CryptoHash<MD5CryptoServiceProvider>();
        }

        public static string Sha1Hash(this FileInfo fileInfo)
        {
            return fileInfo.OpenRead().CryptoHash<SHA1CryptoServiceProvider>();
        }

        public static string RipeMd160Hash(this FileInfo fileInfo)
        {
            return fileInfo.OpenRead().CryptoHash<RIPEMD160Managed>();
        }
        public static string Sha256Hash(this FileInfo fileInfo)
        {
            return fileInfo.OpenRead().CryptoHash<SHA256CryptoServiceProvider>();
        }
        public static string Sha384Hash(this FileInfo fileInfo)
        {
            return fileInfo.OpenRead().CryptoHash<SHA384CryptoServiceProvider>();
        }
        public static string Sha512Hash(this FileInfo fileInfo)
        {
            return fileInfo.OpenRead().CryptoHash<SHA512CryptoServiceProvider>();
        }


        public static string Hash(this FileInfo fileInfo, EHashCode hashCode)
        {
            switch (hashCode)
            {
                case EHashCode.Md5:
                    return fileInfo.OpenRead().CryptoHash<MD5CryptoServiceProvider>();
     
                case EHashCode.Sha1:
                    return fileInfo.Sha1Hash();
                case EHashCode.RipeMd160:
                    return fileInfo.RipeMd160Hash();
                case EHashCode.Sha256:
                    return fileInfo.Sha256Hash();
                case EHashCode.Sha384:
                    return fileInfo.Sha384Hash();
                case EHashCode.Sha512:
                    return fileInfo.Sha512Hash();
                default:
                    throw new ArgumentOutOfRangeException(nameof(hashCode), hashCode, null);
            }
        }
        #endregion

        private static bool IsSameAs(this FileInfo fileInfo, FileInfo otherFileInfo, EHashCode hashCode, bool performByteByByte)
        {
            if (fileInfo.Length != otherFileInfo.Length)
                return false;

            if (fileInfo.Hash(hashCode) != otherFileInfo.Hash(hashCode))
                return true;

            //from https://support.microsoft.com/en-us/kb/320348 
            int file1byte;
            int file2byte;

            var fs1 = new FileStream(fileInfo.FullName, FileMode.Open);
            var fs2 = new FileStream(otherFileInfo.FullName, FileMode.Open);
            do
            {
                // Read one byte from each file.
                file1byte = fs1.ReadByte();
                file2byte = fs2.ReadByte();
            }
            while ((file1byte == file2byte) && (file1byte != -1));
            return ((file1byte - file2byte) == 0);
        }



        private static bool IsDifferentThan(this FileInfo fileInfo, FileInfo otherFileInfo, EHashCode hashCode  , bool performByteByByte)
        {
            return !fileInfo.IsSameAs(otherFileInfo, hashCode, performByteByByte);
        }

        private static class FileName
        {
            public static string Rename(string path, string newName, bool ignoreExtension = true)
            {
                var directory = Path.GetDirectoryName(path);

                if (ignoreExtension)
                {
                    var extension = Path.GetExtension(path);
                    return Path.Combine(directory, newName + extension);
                }
                return Path.Combine(directory, newName);
            }

            public static string Rename(string path, Func<string, string> transformation, bool ignoreExtension = true)
            {
                var name = (ignoreExtension)
                    ? Path.GetFileNameWithoutExtension(path)
                    : Path.GetFileName(path);

                var newName = transformation(name);
                return Rename(path, newName, ignoreExtension);
            }
        }


    }
}
