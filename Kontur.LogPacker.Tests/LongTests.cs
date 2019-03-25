using System;
using System.IO;
using NUnit.Framework;

namespace Kontur.LogPacker.Tests
{
    internal class LongTests
    {
        [Test]
        public void ShouldParseExample()
        {
            EntryPoint.Main(new [] {"example.log", "example.log.compressed"});
            EntryPoint.Main(new [] {"-d", "example.log.compressed", "example.log.decompressed"});

            var original = File.ReadAllText("example.log");
            var compressed = File.ReadAllText("example.log.compressed");
            var decompressed = File.ReadAllText("example.log.decompressed");

            //var correctPart = new StringBuilder();

            //for (var i = 0; i < original.Length; i++)
            //{
            //    Assert.AreEqual(original[i], decompressed[i], $"Correct part: {correctPart}\nError index: {i}\nExpected:{original[i]}\nWas: {decompressed[i]}");
            //    correctPart.Append(original[i]);
            //}

            CollectionAssert.AreEqual(File.ReadAllBytes("example.log"), File.ReadAllBytes("example.log.decompressed"));
        }

        [Test]
        public void ShouldUnpackRandomData()
        {
            var bytes = new byte[1024 * 1024];
            new Random().NextBytes(bytes);
            File.WriteAllBytes("random", bytes);
            EntryPoint.Main(new[] { "random", "random.compressed" });
            EntryPoint.Main(new[] { "-d", "random.compressed", "random.decompressed" });
            CollectionAssert.AreEqual(File.ReadAllBytes("random"), File.ReadAllBytes("random.compressed"));
            CollectionAssert.AreEqual(File.ReadAllBytes("random"), File.ReadAllBytes("random.decompressed"));
        }
    }
}
