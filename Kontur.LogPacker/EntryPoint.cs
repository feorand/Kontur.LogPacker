using System;
using System.IO;

namespace Kontur.LogPacker
{
    internal static class EntryPoint
    {
        public static void Main(string[] args)
        {
            if (args[0] == "-d")
                UnpackFile(args[1], args[2]);
            else
                PackFile(args[0], args[1]);
        }

        private static void PackFile(string sourceFilePath, string compressedFilePath)
        {
            var sourceLines = File.ReadLines(sourceFilePath);
            var packedLines = new LogPacker().PackLines(sourceLines);

            using (var writer = new StreamWriter(compressedFilePath))
                foreach (var line in packedLines)
                    writer.WriteLine(line);
        }

        private static void UnpackFile(string sourceFilePath, string extractedFilePath)
        {
            var compressedLines = File.ReadLines(sourceFilePath);
            var extractedLines = new LogUnpacker().UnpackLines(compressedLines);

            using (var writer = new StreamWriter(extractedFilePath))
                foreach (var line in extractedLines)
                    writer.WriteLine(line);
        }
    }
}
