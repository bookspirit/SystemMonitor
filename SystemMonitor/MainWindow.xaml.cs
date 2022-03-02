using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using MahApps.Metro.Controls;

namespace SystemMonitor
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        //Information info;
        public MainWindow()
        {
            InitializeComponent();
            //info = InformationGetter.getInfomation();
            //UpdateData();

        }

        private void UpdateData()
        {
            //if (info != null)
            //{
            //    CPUCountTB.Text += info.ProcessorCount.ToString();
            //    CPUFreTB.Text += info.ProcessorFrequence;
            //    CPUTypeTB.Text += info.ProcessorType;
            //    MemoryTotalTB.Text += info.MemoryTotal;
            //    MemoryUsedTB.Text += info.MemoryUsed;
            //    DiskTotalTB.Text += info.DiskTotal;
            //    DiskUsageTB.Text += info.DiskUsed;
            //    NetCardTypeTB.Text += info.NetworkCardType;
            //    MacTB.Text += info.MACAddress;
            //    IPTB.Text += info.IPAddress;


            //}
        }
    }
}
