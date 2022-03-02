using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using SystemMonitor.Tool;

namespace SystemMonitor
{
    /// <summary>
    /// DetailWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DetailPage : Page
    {
        Grid current;
        Label crt;
        DispatcherTimer dt;
        ObservableCollection<Proces> processCollection;
        
        public DetailPage()
        {
            InitializeComponent();
            
            dt = new DispatcherTimer()
            {
                Interval = new TimeSpan(20000000)
            };
            dt.Tick += Dt_Tick;
            dt.Start();
            processCollection = new ObservableCollection<Proces>();
            ProcessList.ItemsSource = processCollection;
            
        }

        public void InitTab(string tabn)
        {
            if (tabn == "CpuSimple")
            {
                ChangeTab(button1);
            }
            else if (tabn == "MemorySimple")
            {
                ChangeTab(button2);
            }
            else if (tabn == "DiskSimple")
            {
                ChangeTab(button3);
            }
            else if (tabn == "NetSimple")
            {
                ChangeTab(button4);
            }
        }
        /// <summary>
        /// 每隔两秒就刷新显示的值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dt_Tick(object sender, EventArgs e)
        {
                UpdateCpu();
                UpdateMemory();
                UpdateDisk();
                UpdateNet();
                processCollection.Clear();
            foreach (var item in App.information.ProcessInfo)
            {
                processCollection.Add(item);
            }
        }

        private void UpdateNet()
        {
            NetOutMC.Label.Text = "发送速率";
            NetOutMC.AddNewValue(FormatTool.ByteToKByte( App.information.NetInfo.DataOutSpeed));
            NetOutMC.UnitTB.Text = "KB/s";
            NetInMC.Label.Text = "接收速率";
            NetInMC.AddNewValue(FormatTool.ByteToKByte( App.information.NetInfo.DataInSpeed));
            NetInMC.UnitTB.Text = "KB/s";
        }

        private void UpdateDisk()
        {
            DiskWMC.Label.Text = "硬盘IOPS";
            DiskWMC.AddNewValue(App.information.DiskInfo.DiskRSpeed);
            DiskWMC.UnitTB.Text = "t/s";
            DiskRMC.Label.Text = "硬盘吞吐量";
            DiskRMC.AddNewValue(FormatTool.ByteToKByte(App.information.DiskInfo.DiskWSpeed));
            DiskRMC.UnitTB.Text = "KB/s";
        }

        private void UpdateMemory()
        {
            MemoryMC.Label.Text = "内存使用量";
            MemoryMC.AddNewValue(FormatTool.ByteToGByte(App.information.MemoryInfo.UsedMemory));
            MemoryMC.UnitTB.Text = "GB";
        }

        private void UpdateCpu()
        {
            CPUMC.AddNewValue(App.information.CpuInfo.UseRatio);
        }
        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            Brush bs = ((Viewbox)sender).OpacityMask;
            bs.Opacity = 0.4;
            ((Viewbox)sender).OpacityMask = bs;
        }

        private void Image_MouseLeave(object sender, MouseEventArgs e)
        {
            ((Viewbox)sender).OpacityMask.Opacity = 1;
        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            Brush bs = ((Label)sender).OpacityMask;
            bs.Opacity = 0.4;
            ((Label)sender).OpacityMask = bs;
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            if(crt != (Label)sender)
            {
                ((Label)sender).OpacityMask.Opacity = 1;
            }
        }

        private void Button_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ChangeTab(sender);
        }

        private void ChangeTab(Object sender)
        {
            if (sender == null)
            {
                return;
            }

            if (sender == button1)
            {
                ChangeView(tab1);
            }
            else if (sender == button2)
            {
                ChangeView(tab2);
            }
            else if (sender == button3)
            {
                ChangeView(tab3);
            }
            else if (sender == button4)
            {
                ChangeView(tab4);
            }
            else if (sender == button5)
            {
                ChangeView(tab5);
            }
            if (crt != null)
            {
                crt.OpacityMask.Opacity = 1;
            }
            crt = (Label)sender;
            crt.OpacityMask.Opacity = 0.4;
        }

        private void ChangeView(Grid tab)
        {
            if (current != null)
            {
                current.Visibility = Visibility.Hidden;
            }
            current = tab;
            current.Visibility = Visibility.Visible;
        }

        private void Nav_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Uri("pack://application:,,,/SimplePage.xaml"));
        }

    }
}
