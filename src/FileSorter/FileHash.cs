using System;
using System.IO;
using System.Security.Cryptography;

namespace FileSorter
{
    internal class FileHash
    {
        internal static string GetMd5(string filePath)
        {
            using (var md5 = MD5.Create())
            using (var stream = File.OpenRead(filePath))
            {
                byte[] hash = md5.ComputeHash(stream);

                return convertHashToString(hash);
            }
        }

        private static string convertHashToString(byte[] hash)
        {
            return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
        }
    }
}
