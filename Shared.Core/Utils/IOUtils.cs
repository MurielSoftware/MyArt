using Shared.Core.Constants;
using System;
using System.IO;
using System.Linq;

namespace Shared.Core.Utils
{
    public class IOUtils
    {
        public static string SERVER_PATH = AppDomain.CurrentDomain.BaseDirectory;
        public static string UPLOAD_FOLDER = "Upload";

        /// <summary>
        /// Creates the directory in some path if it is needed.
        /// </summary>
        /// <param name="directoryPath">The path</param>
        public static void CreateDirectories(string directoryPath)
        {
            string[] path_segments = directoryPath.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
            string path = string.Empty;

            foreach (string path_segment in path_segments)
            {
                path += path_segment;
                CreateDirectory(path);
                path += "/";
            }
        }

        /// <summary>
        /// Deletes the directory if it is empty.
        /// </summary>
        /// <param name="directoryPath">The path</param>
        public static void DeleteDirectoryIfNeeded(string directoryPath)
        {
            DirectoryInfo directory = new DirectoryInfo(directoryPath);
            if (!directory.Exists)
            {
                return;
            }
            if (directory.GetFiles().Length > 0 || directory.GetDirectories().Length > 0)
            {
                return;
            }
            if (directoryPath.Substring(directoryPath.Length - UPLOAD_FOLDER.Length).Equals(UPLOAD_FOLDER))
            {
                return;
            }
            directory.Delete();

            if (SharedConstants.SLASH.Equals(directoryPath.Last().ToString()))
            {
                directoryPath = directoryPath.Substring(0, directoryPath.LastIndexOf(SharedConstants.SLASH));
            }
            directoryPath = directoryPath.Substring(0, directoryPath.LastIndexOf(SharedConstants.SLASH));
            DeleteDirectoryIfNeeded(directoryPath);
        }

        /// <summary>
        /// Deletes the file if it exists.
        /// </summary>
        /// <param name="filePath">The path to the file</param>
        public static void Delete(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        /// <summary>
        /// Creates the directory if it does not exist.
        /// </summary>
        /// <param name="path">The path to create a directory</param>
        public static void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public static string GetUploadRoot()
        {
#if DEBUG
            return SERVER_PATH.Replace('\\', '/').Substring(0, SERVER_PATH.Length - 1);
#else
            string serverPathWithoutLastSegment = SERVER_PATH.Replace('\\', '/').Substring(0, SERVER_PATH.Length - 1);
            return serverPathWithoutLastSegment.Substring(0, serverPathWithoutLastSegment.LastIndexOf('/'));
#endif
        }
    }
}
