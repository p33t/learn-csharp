using System;
using System.Diagnostics;
using System.IO;

namespace fundamental_c_sharp
{
    public class FileHandling
    {
        private const string SourceDirName = "fundamental-c-sharp";

        public static void Demo()
        {
            Console.WriteLine("FileHandling ===========================");
            var currentDirectory = Directory.GetCurrentDirectory();
            var sourceDir = currentDirectory.Substring(0,
                currentDirectory.IndexOf(SourceDirName, StringComparison.Ordinal) + SourceDirName.Length);

            Debug.Assert(File.Exists(Path.Combine(sourceDir, "FileHandling.cs")));
        }
    }
}