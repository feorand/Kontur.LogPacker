using System.IO;
using System.IO.Compression;

namespace Kontur.LogPacker
{
    public static class EntryPoint
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

            File.WriteAllLines("temp", packedLines);

            //using (var writer = new StreamWriter("temp"))
            //    foreach (var line in packedLines)
            //        writer.WriteLine(line);

            using (var tempFileStream = File.OpenRead("temp"))
            using (var compressedFileStream = new GZipStream(File.Create(compressedFilePath), CompressionLevel.Optimal))
                tempFileStream.CopyTo(compressedFileStream);

            File.Delete("temp");
        }

        private static void UnpackFile(string sourceFilePath, string extractedFilePath)
        {
            //using (var writer = new StreamWriter(extractedFilePath))
            //    foreach (var line in extractedLines)
            //        writer.WriteLine(line);

            using (var tempFileStream = File.Create("temp"))
            using (var sourceFileStram = new GZipStream(File.OpenRead(sourceFilePath), CompressionMode.Decompress))
                sourceFileStram.CopyTo(tempFileStream);

            var compressedLines = File.ReadLines("temp");
            var extractedLines = new LogUnpacker().UnpackLines(compressedLines);

            File.WriteAllLines(extractedFilePath, extractedLines);
        }
    }
}
