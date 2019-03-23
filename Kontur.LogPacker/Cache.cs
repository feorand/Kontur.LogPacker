using System.Collections.Generic;

namespace Kontur.LogPacker
{
    public class Cache
    {
        private readonly Dictionary<string, ulong> cache = new Dictionary<string, ulong>();
        private ulong cacheIndex;

        public ulong AddIfAbsent(string newItem)
        {
            if (!cache.ContainsKey(newItem))
                cache[newItem] = cacheIndex++;

            return cache[newItem];
        }

        public IEnumerable<string> GetContents(string startToken, string endToken)
        {
            if (cache.Count <= 0)
                yield break;

            yield return startToken;

            foreach (var pair in cache)
                yield return $"{pair.Key}:::{pair.Value};";

            yield return endToken;
        }
    }
}
