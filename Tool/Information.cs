
using System.Collections.Generic;

namespace SystemMonitor.Tool
{
    /// <summary>
    /// 信息类，全部信息的统一存取对象
    /// </summary>
    public class Information
    {
        /// <summary>
        /// CPU模型实例
        /// </summary>
        public Cpu CpuInfo { get; }

        /// <summary>
        /// 内存模型实例
        /// </summary>
        public Memory MemoryInfo { get; }

        /// <summary>
        /// 磁盘模型实例
        /// </summary>
        public Disk DiskInfo { get; }

        /// <summary>
        /// 网络模型实例
        /// </summary>
        public NetWork NetInfo { get; }

        /// <summary>
        /// 进程模型实例
        /// </summary>

        public List<Proces> ProcessInfo;

        /// <summary>
        /// 向模型中填充数据
        /// </summary>
        public void InflatInformation()
        {
            CpuInfo.CoreCount = Util.GetCpuCoreCount();
            CpuInfo.Frequency = Util.GetCPUSpeed();
            CpuInfo.Type = Util.GetCpuType();
            MemoryInfo.TotalMemory = Util.GetMemoryTotal();
            DiskInfo.TotalSize = Util.GetDiskTotal();
            DiskInfo.UsedSize = Util.GetDiskUsed();
            DiskInfo.PartionTotal = Util.GetPartionTotal();
            DiskInfo.PartionUsed = Util.GetPartionUsed();
            DiskInfo.DiskType = Util.GetDiskFormat();
            NetInfo.NetCardType = Util.GetNetCardType();
            NetInfo.MacAddress = Util.GetMACAddress();
            NetInfo.IPV4Address = Util.GetIPV4Address();
            NetInfo.IPV6Address = Util.GetIPV6Address();
        }

        /// <summary>
        /// 更新动态信息数据
        /// </summary>
        public void UpdatePerformance()
        {

            CpuInfo.UseRatio = Util.GetCpuPerformance();
            MemoryInfo.UsedMemory = Util.GetMemoryUsed();
            DiskInfo.DiskRSpeed = Util.GetDiskIOSpeed();
            DiskInfo.DiskWSpeed = Util.GetDiskDIOSpeed();
            NetInfo.DataInSpeed = Util.GetNetworkIn();
            NetInfo.DataOutSpeed = Util.GetNetworkOut();
            ProcessInfo = Util.GetProcessList();
        }

        /// <summary>
        /// 信息类构造器
        /// </summary>
        public Information()
        {
            CpuInfo = new Cpu();
            MemoryInfo = new Memory();
            DiskInfo = new Disk();
            NetInfo = new NetWork();
            ProcessInfo = new List<Proces>();
        }

    }
}
