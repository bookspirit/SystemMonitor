using System.ComponentModel;
namespace SystemMonitor.Tool
{
    /// <summary>
    /// 内存模型类
    /// </summary>
    public class Memory:INotifyPropertyChanged
    {
        /// <summary>
        /// 内存插槽数
        /// </summary>
        public int SlotCount { get; set; }

        /// <summary>
        /// 各插槽内存大小
        /// </summary>
        public ulong[] MemorySize { get; set; }

        /// <summary>
        /// 内存总量
        /// </summary>
        public ulong TotalMemory { get; set; }

        /// <summary>
        /// 内存已用量
        /// </summary>
        private ulong _usedMemory;
        public ulong UsedMemory
        {
            set
            {
                _usedMemory = value;
                if (PropertyChanged != null)//有改变  
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("UsedMemory"));//对Name进行监听  
                }
            }
            get
            {
                return _usedMemory;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
