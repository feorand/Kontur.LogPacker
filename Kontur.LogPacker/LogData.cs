using System;

namespace Kontur.LogPacker
{
    class LogData
    {
        public DateTime Time { get; set; }
        public uint Id { get; set; }
        public string Type { get; set; }
        public string ProcessId { get; set; }
    }
}
