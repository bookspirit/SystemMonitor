using System.Windows.Controls;
using SystemMonitor.Tool;

namespace SystemMonitor
{
    /// <summary>
    /// CpuSimple.xaml 的交互逻辑
    /// </summary>
    public partial class CpuSimple : UserControl
    {
        Cpu info = App.information.CpuInfo;
        public CpuSimple()
        {
            InitializeComponent();
            CPUChart.InitialChart1();
            CPUInfoGrid.DataContext = info;
        }
        /// <summary>
        /// 设置chart的值
        /// </summary>
        public void UpdateCpu()
        { 
            double cpuPercentage = FormatTool.Round2(info.UseRatio);
            CPUChart.SetChartValue(100.0 - cpuPercentage, cpuPercentage);
        }
    }
}
