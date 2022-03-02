using System.Windows.Controls;

using SystemMonitor.Tool;

namespace SystemMonitor
{
    /// <summary>
    /// MemorySimple.xaml 的交互逻辑
    /// </summary>
    public partial class MemorySimple : UserControl
    {
        Memory info = App.information.MemoryInfo;
        public MemorySimple()
        {
            InitializeComponent();
            MemoryChart.InitialChart1();
            MemoryInfoGrid.DataContext = info;
        }
        /// <summary>
        /// 添加chart的值
        /// </summary>
        public void UpdateMemory()
        {       
            ulong memoryTotal = info.TotalMemory;
            ulong memoryUsed = info.UsedMemory;
            MemoryChart.SetChartValue(FormatTool.ByteToGByte(memoryTotal - memoryUsed), FormatTool.ByteToGByte(memoryUsed));
        }
    }
}
