using System.Linq;
using NUnit.Framework;

namespace Kontur.LogPacker.Tests
{
    internal class StringExtensionTests
    {
        [Test]
        public void ShouldParseOrdinaryString()
        {
            const string source = "a b,c";
            var result = source.GetWordsWithIndices(new[] { ' ', ',' }).ToList();
            Assert.AreEqual(("a", 0), result[0]);
            Assert.AreEqual(("b", 2), result[1]);
            Assert.AreEqual(("c", 4), result[2]);
        }

        [Test]
        public void ShouldParseStringWithTwoConsecutiveSpaces()
        {
            const string source = "a  b";
            var result = source.GetWordsWithIndices(new[] { ' ', ',' }).ToList();
            Assert.AreEqual(("a", 0), result[0]);
            Assert.AreEqual(("b", 3), result[1]);
        }
    }
}
