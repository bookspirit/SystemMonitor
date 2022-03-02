using System;
using System.Windows.Controls;

using SystemMonitor.Tool;

namespace SystemMonitor
{
    /// <summary>
    /// DiskSimple.xaml 的交互逻辑
    /// </summary>
    public partial class DiskSimple : UserControl
    {
        Disk info = App.information.DiskInfo;
        public DiskSimple()
        {
            InitializeComponent();
            DiskChart.InitialChart1();
            DiskInfoGrid.DataContext = info;
        }
        /// <summary>
        /// 设置chart的值
        /// </summary>
        public void UpdateDisk()
        {
            DiskChart.SetChartValue(FormatTool.ByteToGByte(info.TotalSize-info.UsedSize), FormatTool.ByteToGByte(info.UsedSize));   
        }
    }
}
