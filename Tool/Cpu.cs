using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemMonitor.Tool
{
    public class Cpu
    {
        public int CoreCount { get; set; }
        public string Type { get; set; }
        public int Frequency { get; set; }
        public float UseRatio { get; set; }

    }
}
