namespace FileSorter
{
    public class AppArguments
    {
        public AppArguments(string[] args)
        {
            if (args.Length >= 3)
            {
                AreValid = true;

                SourceDir = enforceTrailingBackslash(args[0]);
                DestinationDir = enforceTrailingBackslash(args[1]);
                SearchPattern = enforceTrailingBackslash(args[2]);
            }
        }

        public bool AreValid { get; set; } = false;
        public string SourceDir { get; set; }
        public string DestinationDir { get; set; }
        public string SearchPattern { get; set; }

        private string enforceTrailingBackslash(string str)
        {
            if (!str.EndsWith(@"\"))
                str += @"\";

            return str;
        }
    }
}
