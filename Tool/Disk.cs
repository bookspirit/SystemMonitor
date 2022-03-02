
namespace SystemMonitor.Tool
{
    /// <summary>
    /// 磁盘模型类
    /// </summary>
    public class Disk
    {
        /// <summary>
        /// 磁盘总存储量
        /// </summary>
        public ulong TotalSize { get; set; }

        /// <summary>
        /// 磁盘已用数量
        /// </summary>
        public ulong UsedSize { get; set; }

        /// <summary>
        /// 各分区总量
        /// </summary>
        public ulong[] PartionTotal { get; set; }

        /// <summary>
        /// 各分区已用量
        /// </summary>
        public ulong[] PartionUsed { get; set; }

        /// <summary>
        /// 各分区文件系统
        /// </summary>
        public string[] PartionType { get; set; }

        /// <summary>
        /// 磁盘文件系统
        /// </summary>
        public string DiskType { get; set; }

        /// <summary>
        /// 磁盘IOPS
        /// </summary>
        public uint DiskRSpeed { get; set; }

        /// <summary>
        /// 磁盘吞吐量
        /// </summary>
        public uint DiskWSpeed { get; set; }

        /// <summary>
        /// 磁盘IOPS
        /// </summary>
        public uint DiskIOPS { get; set; }

        /// <summary>
        /// 磁盘分区数
        /// </summary>
        public int PartionCount
        {
            get
            {
                if (PartionTotal == null)
                    return 0;
                return PartionTotal.Length;
            }
        }
    }
}
