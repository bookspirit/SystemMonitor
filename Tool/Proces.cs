
namespace SystemMonitor.Tool
{
    /// <summary>
    /// 进程模型类
    /// </summary>
    public class Proces
    {
        /// <summary>
        /// 进程名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 进程分配内存大小
        /// </summary>
        public long Memory { get; set; }

        /// <summary>
        /// 进程描述
        /// </summary>
        public string Description { get; set; }
    }
}
