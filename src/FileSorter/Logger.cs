using System;
using System.IO;
using System.Reflection;

namespace FileSorter
{
    public class Logger
    {
        const ConsoleColor DEFAULT_COLOR = ConsoleColor.White;

        private string _logPath;

        public Logger()
        {
            _logPath = makeLogFilePath();
        }

        public void Write(params string[] lines) => Write(DEFAULT_COLOR, true, lines);

        public void Write(bool endWithNewLine, params string[] lines) => Write(DEFAULT_COLOR, endWithNewLine, lines);

        public void Write(ConsoleColor color, params string[] lines) => Write(color, true, lines);

        public void Write(ConsoleColor color, bool endWithNewLine, params string[] lines)
        {
            Console.ForegroundColor = color;

            foreach (string line in lines)
                writeLine(line);

            if (endWithNewLine)
                writeLine();

            Console.ForegroundColor = DEFAULT_COLOR;
        }

        private string makeLogFilePath()
        {
            string logDirPath = makeLogDirPath();

            FileSystem.EnsureDirectoryExists(logDirPath);

            return $"{logDirPath}FileSorter.{DateTime.Now:yyyy-MM-dd hh.mm.ss}.txt";
        }

        private string makeLogDirPath()
        {
            string exeFilePath = Assembly.GetExecutingAssembly().Location;

            return Path.GetDirectoryName(exeFilePath) + @"\Logs\";
        }

        private void writeLine(string line = "")
        {
            Console.WriteLine(line);
            writeToLog(line);
        }

        private void writeToLog(string line)
        {
            using (StreamWriter writer = new StreamWriter(new FileStream(_logPath, FileMode.Append)))
            {
                writer.WriteLine(line);
            }
        }
    }
}
