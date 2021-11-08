using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileSorter
{
    public class FileCopier
    {
        private List<FileToSort> _filesToSort;
        private Logger _logger;

        public FileCopier(Dictionary<string, FileToSort> filesToSort, Logger logger)
        {
            _filesToSort = filesToSort.Values.ToList();
            _logger = logger;
        }

        public void CopyFilesToDestination()
        {
            foreach (FileToSort fileToSort in _filesToSort)
            {
                try
                {
                    FileSystem.EnsureDirectoryExists(fileToSort.TargetDirectory);
                    copyFileToDestination(fileToSort);
                }
                catch (Exception e)
                {
                    _logger.Write(ConsoleColor.Red,
                        "[FILE COPY ERROR]",
                        $"- Source: {fileToSort.FullPath}",
                        $"- Target: {fileToSort.TargetDirectory}",
                        e.Message);
                }
            }
        }

        private void copyFileToDestination(FileToSort fileToSort)
        {
            string originalDestinationPath = fileToSort.TargetDirectory + fileToSort.Filename,
                destinationPath = originalDestinationPath,
                extension = Path.GetExtension(destinationPath);
            int count = 2;

            while (File.Exists(destinationPath))
            {
                destinationPath = originalDestinationPath.Replace(extension, $"_{count}{extension}");
                count++;
            }

            File.Copy(fileToSort.FullPath, destinationPath);
        }
    }
}
