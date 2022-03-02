using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

using SystemMonitor.Tool;


namespace SystemMonitor
{
    /// <summary>
    /// Doughnut.xaml 的交互逻辑
    /// </summary>
    public partial class Doughnut : UserControl
    {
        public Doughnut()
        {
            InitializeComponent();
            SeriesCollection = new SeriesCollection();
                       
            DataContext = this;
        }



        public SeriesCollection SeriesCollection { get; set; }


      /// <summary>
      /// 为chart设置两个值
      /// </summary>
      /// <param name="value1"></param>
      /// <param name="value2"></param>
        public void SetChartValue(double value1,double value2)
        {
            foreach (var series in SeriesCollection)
            {
                if (series.Title == "未使用"|| series.Title == "接收")
                {
                    foreach (var observable in series.Values.Cast<ObservableValue>())
                    {
                        observable.Value = value1;
                    }
                }
                else
                {
                    foreach (var observable in series.Values.Cast<ObservableValue>())
                    {
                        observable.Value = value2;
                    }
                }
            }
        }
       private void AddSeriesOnClick(object sender, RoutedEventArgs e)
       {
           var r = new Random();
           var c = SeriesCollection.Count > 0 ? SeriesCollection[0].Values.Count : 5;
           
           var vals = new ChartValues<ObservableValue>();

           for (var i = 0; i<c; i++)
           {
               vals.Add(new ObservableValue(r.Next(0, 10)));
           }

           SeriesCollection.Add(new PieSeries
           {
               Values = vals
           });
       }
        //这个是用于网络的chart的初始化
        public void InitialChart2()
        {
            foreach (var series in SeriesCollection)
            {
                if (series.Values.Count > 0)
                {
                    series.Values.RemoveAt(0);
                }
            }
            SeriesCollection.Add(new PieSeries
            {
                Title = "发送",
                Values = new ChartValues<ObservableValue> { new ObservableValue(1) },
                DataLabels = true
            });
            SeriesCollection.Add(new PieSeries
            {
                Title = "接收",
                Values = new ChartValues<ObservableValue> { new ObservableValue(1) },
                DataLabels = true
            });
                         
        }
        //cpu 内存和磁盘的chart的初始化
        public void InitialChart1()
        {
            foreach (var series in SeriesCollection)
            {
                if (series.Values.Count > 0)
                {
                    series.Values.RemoveAt(0);
                }
            }

            SeriesCollection.Add(new PieSeries
            {
                Title = "未使用",
                Values = new ChartValues<ObservableValue> { new ObservableValue(1) },
                DataLabels = true
            });
            SeriesCollection.Add(new PieSeries
            {
                Title = "已使用",
                Values = new ChartValues<ObservableValue> { new ObservableValue(1) },
                DataLabels = true
            });

        }
    }

 }

