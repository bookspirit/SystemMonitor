using System.Windows;

namespace SystemMonitor
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        WatcherThread watcher;
        public static DetailPage detail;
        public MainWindow()
        {
            App.information.InflatInformation();
            watcher = new WatcherThread();
            watcher.Start();
            InitializeComponent();
        }


        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            watcher.Stop();
        }

    }
}
