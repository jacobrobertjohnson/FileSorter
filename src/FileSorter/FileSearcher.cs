using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileSorter
{
    public class FileSearcher
    {
        private string _sourcePath,
            _destPath;
        private string[] _acceptedExtensions;
        private Dictionary<string, FileToSort> _filesToSort;
        private Logger _logger;

        public FileSearcher(AppArguments appArgs, Logger logger)
        {
            _logger = logger;
            _sourcePath = appArgs.SourceDir;
            _destPath = appArgs.DestinationDir;
            _acceptedExtensions = appArgs.SearchPattern.ToLower().Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            _filesToSort = new Dictionary<string, FileToSort>();
        }

        public void SearchForFiles() => searchForFiles(_sourcePath, _destPath);

        private void searchForFiles(string dirPath, string destPath)
        {
            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(dirPath);
                IEnumerable<FileInfo> foundFiles = dirInfo.EnumerateFiles("*.*").Where(d => _acceptedExtensions.Contains(d.Extension.ToLower()));
                IEnumerable<DirectoryInfo> directories = dirInfo.EnumerateDirectories();

                foreach (FileInfo file in foundFiles)
                {
                    addFileHashToResults(file, destPath);
                }

                foreach (DirectoryInfo directory in directories)
                {
                    searchForFiles(directory.FullName, destPath);
                }
            }
            catch (Exception e)
            {
                _logger.Write(ConsoleColor.Red,
                    "[DIRECTORY  SEARCH ERROR]",
                    $"- Directory: {dirPath}",
                    e.Message);
            }
        }

        private void addFileHashToResults(FileInfo fileInfo, string destPath)
        {
            try
            {
                if (File.Exists(fileInfo.FullName))
                {
                    string fileHash = FileHash.GetMd5(fileInfo.FullName);

                    _filesToSort[fileHash] = new FileToSort(fileInfo, destPath);
                }
            }
            catch (Exception e)
            {
                _logger.Write(
                    ConsoleColor.Red,
                    "[FILE SEARCH ERROR]",
                    $"- File: {fileInfo.FullName}",
                    e.Message);
            }
        }

        public Dictionary<string, FileToSort> GetFiles() => _filesToSort;
    }
}
