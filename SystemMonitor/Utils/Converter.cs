using System;
using System.Globalization;
using System.Windows.Data;
using SystemMonitor.Tool;

namespace SystemMonitor
{
    /// <summary>
    /// 数据绑定的转换器
    /// </summary>
    public class K2GConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                var tmp = System.Convert.ToDouble(value);
                return FormatTool.ByteToGByte(tmp);
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class ToKConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value !=null)
            {
                var tmp =System.Convert.ToDouble(value);
                return FormatTool.ToK(tmp);
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class Round2Converter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                var tmp = System.Convert.ToDouble(value);
                return FormatTool.Round2(tmp);
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
