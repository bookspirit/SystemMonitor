using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemMonitor.Tool
{
    public class Disk
    {
        public long TotalSize { get; set; }
        public long UsedSize { get; set; }
        public string DiskType { get; set; }
    }
}
