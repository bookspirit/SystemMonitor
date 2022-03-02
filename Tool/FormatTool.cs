using System;

namespace SystemMonitor.Tool
{
    /// <summary>
    /// 格式化工具类
    /// </summary>
    public class FormatTool
    {
        /// <summary>
        /// 字节到千字节的转换
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static double ByteToKByte(double input)
        {  
            return Math.Round(input / 1024, 2);
        }

        /// <summary>
        /// 字节到兆字节的转换
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static double ByteToMByte(double input)
        {
            return Math.Round(input / 1024 / 1024, 2);
        }

        /// <summary>
        /// 字节到吉字节的转换
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static double ByteToGByte(double input)
        {
            return Math.Round(input / 1024 / 1024 / 1024, 2);
        }

        /// <summary>
        /// 保留两位小数
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static double Round2(double input)
        {
            return Math.Round(input, 2);
        }

        /// <summary>
        /// 换算至千单位制
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static double ToK(double input)
        {
            return Math.Round(input / 1000, 2);
        }
    }
}
