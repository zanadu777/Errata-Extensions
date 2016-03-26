using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace Errata.IO
{
    public static class DirectoryInfoExtensions
    {
        public static DirectoryInfo Child(this DirectoryInfo dir, string relativepath)
        {
            var path = Path.Combine(dir.FullName, relativepath);
            return new DirectoryInfo(path);
        }

        public static void DeleteFiles(this DirectoryInfo dir)
        {
            var files = Directory.GetFiles(dir.FullName);
            foreach (var f in files)
            {
                File.Delete(f);
            }
        }

        public static bool IsEmpty(this DirectoryInfo dir)
        {
            var files = Directory.GetFiles(dir.FullName, "*.*", SearchOption.AllDirectories);
            if (files.Length == 0)
                return true;

            if (files.Length != 1) return false;

            var fileName = Path.GetFileName(files[0]);

            return fileName == "Thumbs.db";
        }

        public static void DeleteEmpty(this DirectoryInfo dir)
        {
            if (!dir.Exists) return;
            if (dir.IsEmpty())
            {
                dir.DeleteFiles();//To get rid of Thumbs.db
                dir.Delete();
            }

        }

        public static void DeleteContents(this DirectoryInfo dir)
        {
            dir.DeleteFiles();
            foreach (var directory in dir.GetDirectories())
                directory.Delete(true);
        }

        public static DirectoryInfo Rename(this DirectoryInfo directory, string name)
        {
            if (directory.Parent == null)
                throw new Exception("Cannot rename drives with this method");


            var directoryPath = Path.Combine(directory.Parent.FullName, name);
            directory.MoveTo(directoryPath);
            return new DirectoryInfo(directoryPath);
        }

        public static bool Contains(this DirectoryInfo directory, DirectoryInfo potentialSubdirectory)
        {
            return directory.FullName.Contains(potentialSubdirectory.FullName);
        }

        public static bool IsContainedBy(this DirectoryInfo directory, DirectoryInfo potentialSuperdirectory)
        {
            return potentialSuperdirectory.FullName.Contains(directory.FullName);
        }


        public static string GetAvailablePAth(this DirectoryInfo directory)
        {
            var potentialPath = PotentialPath(directory);
            while (File.Exists(potentialPath))
            {
                potentialPath = PotentialPath(directory);
            }
            return potentialPath;
        }

        private static string PotentialPath(this DirectoryInfo directory)
        {
            var tempPath = Path.GetTempFileName();
            File.Delete(tempPath);
            var name = Path.GetFileName(tempPath);
            var potentialPath = Path.Combine(directory.FullName, name);
            return potentialPath;
        }



        private static void CopyFilesTo(this DirectoryInfo directory, DirectoryInfo destination)
        {
            foreach (var file in directory.GetFiles())
            {
                file.CopyTo(destination);
            }
        }


        private static void CopyFilesTo(this DirectoryInfo directory, DirectoryInfo destination, ECopyMoveOptions option)
        {
            IEnumerable<FileInfo> filesToBecopied;
            IEnumerable<string> destinationNames;
            switch (option)
            {

                case ECopyMoveOptions.OverwriteExisting:
                    filesToBecopied = directory.GetFiles();
                    break;
                case ECopyMoveOptions.OnlyOverwriteExisting:
                    destinationNames = from file in destination.GetFiles()
                                       select file.Name;

                    filesToBecopied = from file in directory.GetFiles()
                                      where destinationNames.Contains(file.Name)
                                      select file;

                    break;
                case ECopyMoveOptions.PreserveExisting:
                    destinationNames = from file in destination.GetFiles()
                                       select file.Name;

                    filesToBecopied = from file in directory.GetFiles()
                                      where !destinationNames.Contains(file.Name)
                                      select file;

                    break;
                //case ECopyMoveOptions.ReplaceNewer:
                //    break;
                //case ECopyMoveOptions.ReplaceOlder:
                //    break;
                //case ECopyMoveOptions.ReplaceLarger:
                //    break;
                //case ECopyMoveOptions.ReplaceSmaller:
                //    break;
                default:
                    throw new ArgumentOutOfRangeException("option");
            }

            foreach (var file in filesToBecopied)
            {
                file.CopyTo(destination);
            }

        }

        private static void CopyFilesFrom(this DirectoryInfo directory, DirectoryInfo source)
        {
            foreach (var file in source.GetFiles())
            {
                file.CopyTo(directory);
            }
        }


        private static void MoveFilesTo(this DirectoryInfo directory, DirectoryInfo destination)
        {
            foreach (var file in directory.GetFiles())
            {
                file.MoveTo(destination);
            }
        }

        private static void MoveFilesFrom(this DirectoryInfo directory, DirectoryInfo source)
        {
            foreach (var file in source.GetFiles())
            {
                file.CopyTo(directory);
            }
        }

        

        private static void CopyFilesTo(this DirectoryInfo directory, DirectoryInfo destination, IEnumerable<string> filenames)
        {

            var files = from file in directory.GetFiles()
                        where filenames.Contains(file.Name)
                        select file;

            foreach (var file in files)
                file.CopyTo(destination);
        }

        private static void CopyFilesFrom(this DirectoryInfo directory, Uri uri)
        {
            //uri.
            using (var client = new WebClient())
            {
                try
                {
                    var path = Path.Combine(directory.FullName, uri.Segments.Last());
                    client.DownloadFile(uri, path);
                }
                catch (Exception)
                {
                    
                    throw;
                }
            }
        }

        private static void CopyFilesFrom(this DirectoryInfo directory, string path)
        {

        }

        private static void CopyFilesFrom(this DirectoryInfo directory, DirectoryInfo source, IEnumerable<string> filenames)
        {

            var files = from file in directory.GetFiles()
                        where filenames.Contains(file.Name)
                        select file;

            foreach (var file in files)
                file.CopyTo(directory);
        }


        private static void MoveFilesTo(this DirectoryInfo directory, DirectoryInfo destination, IEnumerable<string> filenames)
        {
            var files = from file in directory.GetFiles()
                        where filenames.Contains(file.Name)
                        select file;

            foreach (var file in files)
                file.CopyTo(destination);
        }

        private static void MoveFilesFrom(this DirectoryInfo directory, DirectoryInfo source, IEnumerable<string> filenames)
        {

            var files = from file in directory.GetFiles()
                        where filenames.Contains(file.Name)
                        select file;

            foreach (var file in files)
                file.MoveTo(directory);
        }



        private static Byte[] ExplorerSortedBytes(DirectoryInfo directory)
        {
            var files = (directory.GetFiles().Select(f => f)
                 .Where(f => (f.Attributes & FileAttributes.Hidden) == 0)).ToArray();

            files.SortLikeExplorer();
            var bytes = files.Bytes();
            return bytes;
        }


        public static string Md5Hash(this DirectoryInfo directory)
        {
            return ExplorerSortedBytes(directory).Md5Hash();
        }

        public static string Sha1Hash(this DirectoryInfo directory)
        {
            return ExplorerSortedBytes(directory).Sha1Hash();
        }

        public static string RipeMd160Hash(this DirectoryInfo directory)
        {
            return ExplorerSortedBytes(directory).RipeMd160Hash();
        }
        public static string Sha256Hash(this DirectoryInfo directory)
        {
            return ExplorerSortedBytes(directory).Sha256Hash();
        }
        public static string Sha384Hash(this DirectoryInfo directory)
        {
            return ExplorerSortedBytes(directory).Sha384Hash();
        }
        public static string Sha512Hash(this DirectoryInfo directory)
        {
            return ExplorerSortedBytes(directory).Sha512Hash();
        }
    }
}
