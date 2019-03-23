using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kontur.LogPacker
{
    public static class StringExtensions
    {
        public static IEnumerable<(string substring, int index)> GetWordsWithIndices(
            this string source, 
            char[] delimiters = null, 
            int startIndex = 0, 
            bool shouldIncludeDelimiter = false)
        {
            if (startIndex >= source.Length)
                throw new ArgumentException("Start index must be less than source length");

            if (delimiters == null)
                delimiters = new[] { ' ' };

            var currentWord = new StringBuilder();
            var currentIndex = 0;

            for (var i = startIndex; i < source.Length; i++)
            {
                if (delimiters.Contains(source[i]))
                {
                    if (currentWord.Length > 0)
                    {
                        if (shouldIncludeDelimiter)
                            currentWord.Append(source[i]);

                        yield return (currentWord.ToString(), currentIndex);
                        currentWord = new StringBuilder();
                        currentIndex = i + 1;
                    }

                    continue;
                }

                currentWord.Append(source[i]);
            }

            if (currentWord.Length > 0)
                yield return (currentWord.ToString(), currentIndex);
        }
    }
}
