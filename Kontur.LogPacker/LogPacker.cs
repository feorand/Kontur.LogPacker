using System;
using System.Collections.Generic;
using System.Linq;

namespace Kontur.LogPacker
{
    public class LogPacker
    {
        private const string LineStartToken = "////LINE";
        private const string CacheStartToken = "////CACHE";
        private const string CacheEndToken = "////END";

        private readonly Cache dateCache = new Cache();
        private readonly Cache statusCache = new Cache();
        private readonly Cache sentencesCache = new Cache();

        public IEnumerable<string> PackLines(IEnumerable<string> sourceLines)
        {
            foreach (var line in sourceLines)
                yield return PackLine(line);

            yield return CacheStartToken;
            foreach (var line in GetCacheContents())
                yield return line;
        }

        private IEnumerable<string> GetCacheContents()
        {
            foreach (var line in dateCache.GetContents(CacheEndToken))
                yield return line;

            foreach (var line in statusCache.GetContents(CacheEndToken))
                yield return line;

            foreach (var line in sentencesCache.GetContents(CacheEndToken))
                yield return line;
        }

        private string PackLine(string source)
        {
            var words = source.GetWordsWithIndices(new[] {' ', ','}).Take(6).ToList();

            var logInfo = PackInfoPart(words.Select(w => w.substring).ToList());

            if (logInfo == null)
                return source;

            var descriptionIndex = words[5].index;

            var descriptionSentences = source
                .GetWordsWithIndices(new[] {'.', '!', ':', ']'}, descriptionIndex, true, false)
                .Select(w => w.Item1)
                .Select(sentence => sentencesCache.AddIfAbsent(sentence));

            var result = $"{LineStartToken} {logInfo} {string.Join("|", descriptionSentences)}";
            return result;
        }

        private CachedLogInfo PackInfoPart(IReadOnlyList<string> words)
        {
            var possibleDate = string.Join(" ", words.Take(2));

            if (!DateTime.TryParse(possibleDate, out _))
                return null;

            var date = dateCache.AddIfAbsent(possibleDate);

            if (!ulong.TryParse(words[2], out var fractionOfSecond))
                return null;

            if (!ulong.TryParse(words[3], out var id))
                return null;

            var status = statusCache.AddIfAbsent(words[4]);

            return new CachedLogInfo {Date = date, FractionOfSecond = fractionOfSecond, Id = id, Status = status};
        }
    }
}