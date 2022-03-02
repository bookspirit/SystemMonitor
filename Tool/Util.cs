using System;
using System.Collections.Generic;
using System.Management;
using System.IO;
using System.Net;
using System.Diagnostics;
using System.Net.NetworkInformation;
using Microsoft.Win32;

namespace SystemMonitor.Tool
{
    public class Util
    {

        /// <summary>
        /// 获取CPU主频速度
        /// </summary>
        /// <returns></returns>
        public static int GetCPUSpeed()
        {

            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT *FROM Win32_Processor");
            foreach (var myObject in searcher.Get())
            {
                return Convert.ToInt32(myObject.Properties["CurrentClockSpeed"].Value);
            }
            return 0;
        }

        /// <summary>
        /// 获取CPU型号信息
        /// </summary>
        /// <returns></returns>
        public static string GetCpuType()
        {
            ManagementClass mc = new ManagementClass("Win32_Processor");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (var myObject in moc)
            {
                return myObject.Properties["ProcessorType"].Value.ToString();
            }
            return "Unknown";
        }

        /// <summary>
        /// 获取CPU使用率
        /// </summary>
        /// <returns></returns>
        public static float GetCpuPercentage()
        {
            PerformanceCounter p = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            return (p.NextValue() / Environment.ProcessorCount).ToString();

        }

        /// <summary>
        /// 获取内存总量
        /// </summary>
        /// <returns></returns>
        public static long GetMemoryTotal()
        {
            ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (var mo in moc)
            {
                if (mo["TotalPhysicalMemory"] != null)
                {
                    return Convert.ToInt64(mo["TotalPhysicalMemory"]);
                }
            }
            return 0;
        }

        /// <summary>
        /// 获取已用内存量，返回单位为KB
        /// </summary>
        /// <returns></returns>
        public static long GetMemoryFree()
        {
            ManagementClass mc = new ManagementClass("Win32_OperatingSystem");
            ManagementObjectCollection moc = mc.GetInstances();
            long memory = 0;
            foreach (var mo in moc)
            {
                if (mo["FreePhysicalMemory"] != null)
                {
                    memory += Convert.ToInt64(mo["FreePhysicalMemory"]);
                }
            }
            return memory;
        }

        /// <summary>
        /// 获取分区总容量
        /// </summary>
        /// <returns></returns>
        public static long[] GetPartionTotal()
        {
            var drive = DriveInfo.GetDrives();
            long[] totals = new long[drive.Length];
            for (int i = 0; i < drive.Length; i++)
            {
                if (drive[i].IsReady)
                {
                    totals[i] = drive[i].TotalSize;
                }
            }
            return totals;
        }

        /// <summary>
        /// 获取分区剩余容量
        /// </summary>
        /// <returns></returns>
        public static long[] GetPartionFree()
        {
            var drive = DriveInfo.GetDrives();
            long[] frees = new long[drive.Length];
            for (int i = 0; i < drive.Length; i++)
            {
                if (drive[i].IsReady)
                {
                    frees[i] = drive[i].AvailableFreeSpace;
                }
            }
            return frees;
        }

        /// <summary>
        /// 获取磁盘总容量
        /// </summary>
        /// <returns></returns>
        public static long GetDiskTotal()
        {
            long[] all = GetPartionTotal();
            long total = 0;
            foreach(long it in all)
            {
                total += it;
            }
            return total;
        }

        /// <summary>
        /// 获取磁盘剩余容量
        /// </summary>
        /// <returns></returns>
        public static long GetDiskFree()
        {
            long[] all = GetPartionFree();
            long free = 0;
            foreach (long it in all)
            {
                free += it;
            }
            return free;
        }

        /// <summary>
        /// 获取分区文件系统格式
        /// </summary>
        /// <returns></returns>
        public static string[] GetDriveFormat()
        {
            var drive = DriveInfo.GetDrives();
            string[] formats = new string[drive.Length];
            for (int i = 0;i < drive.Length;i++)
            {
                if (drive[i].IsReady)
                {
                    formats[i] = drive[i].DriveFormat;
                }
            }
            return formats;
        }

        /// <summary>
        /// 获取网卡型号
        /// </summary>
        /// <returns></returns>
        public static List<string> GetNetCardType()
        {
            string path = "SYSTEM\\CurrentControlSet\\Control\\Network\\{4D36E972-E325-11CE-BFC1-08002BE10318}\\";
            var interfaces = NetworkInterface.GetAllNetworkInterfaces();
            List<string> cards = new List<string>();
            foreach (var adapter in interfaces)
            {
                string key = path + adapter.Id + "\\Connection";
                RegistryKey rk = Registry.LocalMachine.OpenSubKey(key, false);
                if(rk != null)
                {
                    string pnpInstanceID = rk.GetValue("PnpInstanceID", "").ToString();
                    if(pnpInstanceID.Length>3&& pnpInstanceID.Substring(0, 3) == "PCI")
                    {
                        cards.Add(adapter.Description);
                    }
                }
            }
            return cards;
        }

        /// <summary>
        /// 获取网卡MAC地址
        /// </summary>
        /// <returns></returns>
        public static List<string> GetMACAddress()
        {
            string path = "SYSTEM\\CurrentControlSet\\Control\\Network\\{4D36E972-E325-11CE-BFC1-08002BE10318}\\";
            var interfaces = NetworkInterface.GetAllNetworkInterfaces();
            List<string> macs = new List<string>();
            foreach (var adapter in interfaces)
            {
                string key = path + adapter.Id + "\\Connection";
                RegistryKey rk = Registry.LocalMachine.OpenSubKey(key, false);
                if (rk != null)
                {
                    string pnpInstanceID = rk.GetValue("PnpInstanceID", "").ToString();
                    if (pnpInstanceID.Length > 3 && pnpInstanceID.Substring(0, 3) == "PCI")
                    {
                        string mac = adapter.GetPhysicalAddress().ToString();

                        macs.Add(mac);
                    }
                }
            }
            return macs;
        }

        /// <summary>
        /// 获取本机IPV4地址
        /// </summary>
        /// <returns></returns>
        public static string GetIPV4Address()
        {
            IPHostEntry myEntry = Dns.GetHostEntry(Dns.GetHostName());
            foreach(IPAddress add in myEntry.AddressList)
            {
                if (add.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    return add.ToString();
                }
            }
            return "";
        }

        /// <summary>
        /// 获取本机IPV6地址
        /// </summary>
        /// <returns></returns>
        public static string GetIPV6Address()
        {
            IPHostEntry myEntry = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress add in myEntry.AddressList)
            {
                if (add.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                {
                    return add.ToString();
                }
            }
            return "";
        }

        public static string GetActiveNetcard()
        {
            NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
            string intername;
            foreach(NetworkInterface inter in interfaces)
            {
                if(inter.OperationalStatus == OperationalStatus.Up)
                {
                    return inter.Name;
                }
            }
            return "";
        }

        public static float GetNetworkUtilization(string networkCard)
        {

            const int numberOfIterations = 10;

            PerformanceCounter bandwidthCounter = new PerformanceCounter("Network Interface", "Current Bandwidth", networkCard);
            float bandwidth = bandwidthCounter.NextValue();//valor fixo 10Mb/100Mn/

            PerformanceCounter dataSentCounter = new PerformanceCounter("Network Interface", "Bytes Sent/sec", networkCard);

            PerformanceCounter dataReceivedCounter = new PerformanceCounter("Network Interface", "Bytes Received/sec", networkCard);

            float sendSum = 0;
            float receiveSum = 0;

            for (int index = 0; index < numberOfIterations; index++)
            {
                sendSum += dataSentCounter.NextValue();
                receiveSum += dataReceivedCounter.NextValue();
            }
            float dataSent = sendSum;
            float dataReceived = receiveSum;


            float utilization = (8 * (dataSent + dataReceived)) / (bandwidth * numberOfIterations) * 100;
            return utilization;
        }

        public static float GetNetworkFlow()
        {
            return GetNetworkUtilization(GetActiveNetcard());
        }
    }
}
