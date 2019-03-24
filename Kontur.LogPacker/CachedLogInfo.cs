using System.Numerics;

namespace Kontur.LogPacker
{
    internal class CachedLogInfo
    {
        public BigInteger Date { get; set; }
        public BigInteger FractionOfSecond { get; set; }
        public BigInteger Id { get; set; }
        public BigInteger Status { get; set; }

        public override string ToString()
            => $"{Date} {FractionOfSecond} {Id} {Status}";
    }
}
