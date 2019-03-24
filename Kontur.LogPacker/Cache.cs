using System.Collections.Generic;
using System.Numerics;

namespace Kontur.LogPacker
{
    public class Cache
    {
        private readonly Dictionary<string, BigInteger> cache = new Dictionary<string, BigInteger>();
        private BigInteger cacheIndex;

        public BigInteger AddIfAbsent(string newItem)
        {
            if (!cache.ContainsKey(newItem))
                cache[newItem] = cacheIndex++;

            return cache[newItem];
        }

        public IEnumerable<string> GetContents(string endToken)
        {
            foreach (var (key, value) in cache)
            {
                yield return key;
                yield return value.ToString();
            }

            yield return endToken;
        }
    }
}
