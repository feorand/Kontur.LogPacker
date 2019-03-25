using System.Collections.Generic;
using System.Linq;

namespace Kontur.LogPacker
{
    public class LogUnpacker
    {
        private const string LineStartToken = "////LINE";
        private const string CacheStartToken = "////CACHE";
        private const string CacheEndToken = "////END";

        private readonly Dictionary<ulong, string> reverseDateCache = new Dictionary<ulong, string>();
        private readonly Dictionary<ulong, string> reverseStatusCache = new Dictionary<ulong, string>();
        private readonly Dictionary<ulong, string> reverseSentenceCache = new Dictionary<ulong, string>();

        public IEnumerable<string> UnpackLines(IEnumerable<string> compressedLines)
        {
            // ReSharper disable once PossibleMultipleEnumeration
            using (var linesEnumerator = compressedLines.GetEnumerator())
            {
                MoveToCache(linesEnumerator);
                FillFromEnumerator(linesEnumerator, reverseDateCache, CacheEndToken);
                FillFromEnumerator(linesEnumerator, reverseStatusCache, CacheEndToken);
                FillFromEnumerator(linesEnumerator, reverseSentenceCache, CacheEndToken);
            }

            // ReSharper disable once PossibleMultipleEnumeration
            return compressedLines.TakeWhile(s => !s.StartsWith(CacheStartToken)).Select(UnpackLine);
        }

        private string UnpackLine(string line)
        {
            if (!line.StartsWith(LineStartToken))
                return line;

            var parts = line.Split(' ', '|').Skip(1).ToList();
            var date = reverseDateCache[ulong.Parse(parts[0])];
            var fractionOfSecond = parts[1].PadLeft(3,'0');
            var id = parts[2] + new string(' ', (6 - parts[2].Length) % 6);
            var statusFromCache = reverseStatusCache[ulong.Parse(parts[3])];
            var status = statusFromCache + new string(' ', (5 - statusFromCache.Length) % 5);

            var descriptionParts = parts.Skip(4).Select(p => reverseSentenceCache[ulong.Parse(p)]);

            return $"{date},{fractionOfSecond} {id} {status} {string.Join("", descriptionParts)}";
        }

        private static void MoveToCache(IEnumerator<string> source)
        {
            while (source.MoveNext())
                if (source.Current.StartsWith(CacheStartToken))
                    return;
        }

        private static void FillFromEnumerator(IEnumerator<string> source, Dictionary<ulong, string> cache, string endToken)
        {
            while (source.MoveNext())
            {
                if (source.Current.StartsWith(endToken))
                    return;

                var value = source.Current;
                source.MoveNext();
                var key = ulong.Parse(source.Current);
                cache[key] = value;
            }
        }
    }
}
