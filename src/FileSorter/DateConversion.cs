using System;
using System.Text;
using System.Text.RegularExpressions;

namespace FileSorter
{
    internal class DateConversion
    {
        private static Regex _dateCleaner = new Regex(":");

        public static DateTime? ToDate(byte[] bytesIn)
        {
            string formattedDate = getDateBytes(bytesIn);
            DateTime parsedDate;

            if (DateTime.TryParse(formattedDate, out parsedDate))
                return parsedDate;

            return null;
        }

        private static string getDateBytes(byte[] bytesIn)
        {
            string rawDateProp = Encoding.UTF8.GetString(bytesIn);

            return _dateCleaner.Replace(rawDateProp, "-", 2);
        }
    }
}
