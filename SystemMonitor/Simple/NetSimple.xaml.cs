using System.Windows.Controls;

using SystemMonitor.Tool;

namespace SystemMonitor
{
    /// <summary>
    /// NetSimple.xaml 的交互逻辑
    /// </summary>
    public partial class NetSimple : UserControl
    {
        NetWork info = App.information.NetInfo;
        public NetSimple()
        {
            InitializeComponent();
            NetChart.InitialChart2();
            NetInfoGrid.DataContext = info;
        }
        /// <summary>
        /// 添加chart的值
        /// </summary>
        public void UpdateNet()
        {  
            NetChart.SetChartValue(FormatTool.ByteToKByte(info.DataInSpeed), FormatTool.ByteToKByte(info.DataOutSpeed));
        }
    }
}
