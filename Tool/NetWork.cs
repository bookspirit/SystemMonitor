using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemMonitor.Tool
{
    public class NetWork
    {
        public List<string> NetCardType { get; set; }
        public List<string> MacAddress { get; set; }
        public string IPV4Address { get; set; }
        public string IPV6Address { get; set; }
    }
}
