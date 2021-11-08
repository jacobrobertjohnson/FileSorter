using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace FileSorter
{
    public class FileToSort
    {
        public string Filename { get; private set; }
        public string FullPath { get; private set; }
        public string TargetDirectory { get; private set; }
        public DateTime DateCreated { get; private set; }

        public FileToSort(FileInfo fileInfo, string rootDestinationDir)
        {
            Filename = fileInfo.Name;
            FullPath = fileInfo.FullName;
            DateCreated = getCreationTime(fileInfo);
            TargetDirectory = makeTargetDirectory(rootDestinationDir, DateCreated);
        }

        private DateTime getCreationTime(FileInfo fileInfo)
        {
            return Exif.GetDateTaken(fileInfo.FullName) ?? fileInfo.LastWriteTime;
        }

        private string makeTargetDirectory(string rootDestinationDir, DateTime createdDate)
            => $"{rootDestinationDir}{createdDate.Year}\\{createdDate.Month:00}\\";
    }
}
