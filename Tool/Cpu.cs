using System.ComponentModel;
namespace SystemMonitor.Tool
{
    /// <summary>
    /// CPU模型类
    /// </summary>
    public class Cpu:INotifyPropertyChanged
    {

        /// <summary>
        /// CPU核心数
        /// </summary>
        public int CoreCount { get; set; }

        /// <summary>
        /// CPU型号
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// CPU主频
        /// </summary>
        public uint Frequency { get; set; }

        /// <summary>
        /// CPU使用率
        /// </summary>
        private float _useRatio;
        public float UseRatio  
        {  
            set  
            {  
                _useRatio = value;  
                if (PropertyChanged != null)//有改变  
                {  
                    PropertyChanged(this, new PropertyChangedEventArgs("UseRatio"));//对Name进行监听  
                }  
            }  
            get  
            {  
                return _useRatio;  
            }  
        } 

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
