using System;
using System.IO;
using System.Management;
using System.Runtime;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration.Internal;
using System.Management.Instrumentation;

namespace SystemMonitor.Tool
{
    public class Information
    {
        public Cpu CpuInfo { get; }
        public Memory MemoryInfo { get; }
        public Disk DiskInfo { get; }
        public NetWork NetInfo { get; }

        public void InflatInformation()
        {
            CpuInfo.CoreCount = Environment.ProcessorCount;
            CpuInfo.Frequency = Util.GetCPUSpeed();
            CpuInfo.Type = Util.GetCpuType();
        }
        public Information()
        {

        }

    }
}
