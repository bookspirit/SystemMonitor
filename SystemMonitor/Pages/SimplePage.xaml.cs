using System;
using System.Media;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows.Threading;

namespace SystemMonitor
{
    /// <summary>
    /// SimplePage.xaml 的交互逻辑
    /// </summary>
    public partial class SimplePage : Page
    {
        DispatcherTimer dt;
        public SimplePage()
        {
            InitializeComponent();
            dt = new DispatcherTimer()
            {
                Interval = new TimeSpan(20000000)
            };
            dt.Tick += Dt_Tick;
            dt.Start();
        }
        private void Dt_Tick(object sender, EventArgs e)
        {
            CpuSimple.UpdateCpu();
            MemorySimple.UpdateMemory();
            DiskSimple.UpdateDisk();
            NetSimple.UpdateNet();
            CheckUsed();
        }
        /// <summary>
        /// 检查cpu和内存的使用量，超过90%就提醒用户
        /// </summary>
        private void CheckUsed()
        {
            if (Double.Parse(CpuSimple.CPUPercentTB.Text) > 90.0 ||( Double.Parse(MemorySimple.MemoryUsedTB.Text)/ Double.Parse(MemorySimple.MemoryTotalTB.Text)) > 0.9)
            {
                // setDataSource
                SoundPlayer player = new SoundPlayer("alert.wav");
                player.Play();
                //MessageBox.Show("高负荷运行！");
            }
        }

        private void CpuSimple_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(MainWindow.detail == null)
            {
                MainWindow.detail = new DetailPage();
            }
            MainWindow.detail.InitTab((e.Source as Control).Name);
            NavigationService.Navigate(MainWindow.detail);
        }

        private void CpuSimple_MouseEnter(object sender, MouseEventArgs e)
        {
            var a = e.OriginalSource;
            if (a is CpuSimple)
            {
                (a as CpuSimple).Border1.Background.Opacity = 0.8;
            }
            else if(a is MemorySimple)
            {
                (a as MemorySimple).Border1.Background.Opacity = 0.8;
            }
            else if (a is DiskSimple)
            {
                (a as DiskSimple).Border1.Background.Opacity = 0.8;
            }
            else if (a is NetSimple)
            {
                (a as NetSimple).Border1.Background.Opacity = 0.8;
            }
        }

        private void CpuSimple_MouseLeave(object sender, MouseEventArgs e)
        {
            var a = e.OriginalSource;
            if (a is CpuSimple)
            {
                (a as CpuSimple).Border1.Background.Opacity = 1.0;
            }
            else if (a is MemorySimple)
            {
                (a as MemorySimple).Border1.Background.Opacity = 1.0;
            }
            else if (a is DiskSimple)
            {
                (a as DiskSimple).Border1.Background.Opacity = 1.0;
            }
            else if (a is NetSimple)
            {
                (a as NetSimple).Border1.Background.Opacity = 1.0;
            }
        }
    }
}
