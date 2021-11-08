using System;
using System.Collections.Generic;

namespace FileSorter
{
    class Program
    {
        static void Main(string[] args)
        {
            AppArguments appArgs = new AppArguments(args);
            Logger logger = new Logger();
            FileSearcher searcher;
            Dictionary<string, FileToSort> filesToSort;

            if (appArgs.AreValid)
            {
                searcher = new FileSearcher(appArgs, logger);

                logger.Write($"\nStarted searching for files at {DateTime.Now}");

                searcher.SearchForFiles();

                logger.Write(ConsoleColor.Green, $"Finished searching for files at {DateTime.Now}");

                filesToSort = searcher.GetFiles();

                logger.Write(ConsoleColor.Green, $"{filesToSort.Count} files will be copied.");

                new FileCopier(filesToSort, logger).CopyFilesToDestination();

                logger.Write(ConsoleColor.Green, $"Finished copying files at {DateTime.Now}");

                Console.Write("Press Enter to continue...");
                Console.ReadLine();
            }
            else
                logger.Write(ConsoleColor.Red,"Usage:\nFileSorter.exe [sourceDirectory] [targetDirectory] [searchPattern]");
        }
    }
}
