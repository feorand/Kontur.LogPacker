namespace Kontur.LogPacker
{
    internal class CachedLogInfo
    {
        public ulong Date { get; set; }
        public ulong FractionOfSecond { get; set; }
        public ulong Status { get; set; }
        public ulong Id { get; set; }

        public override string ToString()
            => $"{Date} {FractionOfSecond} {Id} {Status}";
    }
}
