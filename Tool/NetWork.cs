using System.Collections.Generic;

namespace SystemMonitor.Tool
{
    /// <summary>
    /// 网络模型类
    /// </summary>
    public class NetWork
    {
        /// <summary>
        /// 网卡类型
        /// </summary>
        public string NetCardType { get; set; }

        /// <summary>
        /// 网卡MAC地址
        /// </summary>
        public string MacAddress { get; set; }

        /// <summary>
        /// IPV4地址
        /// </summary>
        public string IPV4Address { get; set; }

        /// <summary>
        /// IPV6地址
        /// </summary>
        public string IPV6Address { get; set; }

        /// <summary>
        /// 接收速率
        /// </summary>
        public ulong DataInSpeed { get; set; }

        /// <summary>
        /// 发送速率
        /// </summary>
        public ulong DataOutSpeed { get; set; }
    }
}
