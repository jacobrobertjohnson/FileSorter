using System.IO;

namespace FileSorter
{
    internal class FileSystem
    {
        public static void EnsureDirectoryExists(string dirPath)
        {
            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);
        }
    }
}
