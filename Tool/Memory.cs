using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemMonitor.Tool
{
    public class Memory
    {
        public int SlotCount { get; set; }
        public long[] MemorySize { get; set; }
        public long TotalMemory { get; set; }
        public long UsedMemory { get; set; }
    }
}
