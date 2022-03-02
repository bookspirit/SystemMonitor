using System;
using System.Management;
using System.IO;
using System.Net;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SystemMonitor.Tool
{
    public static class Util
    {
        static ManagementObjectSearcher searcher;
        static PerformanceCounter ProcessorCounter;
        static PerformanceCounter MemoryCounter;
        static PerformanceCounter DiskIOCounter;
        static PerformanceCounter DiskDIOCounter;
        static PerformanceCounter DiskDWCounter;
        static PerformanceCounter DiskDRCounter;
        static PerformanceCounter dataSentCounter;
        static PerformanceCounter dataReceiveCounter;
        static PerformanceCounter bandwidthCounter;

        static Util()
        {
            searcher = new ManagementObjectSearcher();
            ProcessorCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            MemoryCounter = new PerformanceCounter("Memory", "Available Bytes");
            DiskIOCounter = new PerformanceCounter("PhysicalDisk", "Disk Transfers/sec", "_Total");
            DiskDIOCounter = new PerformanceCounter("PhysicalDisk", "Disk Bytes/sec", "_Total");
            DiskDRCounter = new PerformanceCounter("PhysicalDisk", "Disk Read Bytes/sec", "_Total");
            DiskDWCounter = new PerformanceCounter("PhysicalDisk", "Disk Write Bytes/sec", "_Total");
            dataSentCounter = new PerformanceCounter("Network Interface", "Bytes Sent/sec",GetActiveNetcard());
            dataReceiveCounter = new PerformanceCounter("Network Interface", "Bytes Received/sec",GetActiveNetcard());
            bandwidthCounter = new PerformanceCounter("Network Interface", "Current Bandwidth", GetActiveNetcard());
        }

        /// <summary>
        ///  WMI通用查询方法
        /// </summary>
        /// <param name="property"></param>
        /// <param name="classKey"></param>
        /// <returns></returns>
        private static Object WMIQuery(string property, string classKey)
        {
            searcher.Query = new SelectQuery("SELECT " + property + " FROM " + classKey);
            ManagementObjectCollection.ManagementObjectEnumerator en = searcher.Get().GetEnumerator();
            if (en.MoveNext() && en.Current.Properties.Count > 0)
            {
                object rel = en.Current.Properties[property].Value;
                en.Dispose();
                return rel;
            }
            return null;
        }

        /// <summary>
        /// 获取CPU核心数量
        /// </summary>
        /// <returns></returns>
        public static int GetCpuCoreCount()
        {
            return Environment.ProcessorCount;
        }

        /// <summary>
        /// 获取CPU主频速度
        /// </summary>
        /// <returns></returns>
        public static uint GetCPUSpeed()
        {
            Object res = WMIQuery("MaxClockSpeed", "Win32_Processor");
            return res == null ? 0 : Convert.ToUInt32(res);
        }

        /// <summary>
        /// 获取CPU型号信息
        /// </summary>
        /// <returns></returns>
        public static string GetCpuType()
        {
            Object res = WMIQuery("Name", "Win32_Processor");
            return res == null ? "Unknown" : res.ToString();
        }

        /// <summary>
        /// 获取CPU使用率
        /// </summary>
        /// <returns></returns>
        public static float GetCpuPerformance()
        {
            return ProcessorCounter.NextValue();

        }

        /// <summary>
        /// 获取内存总量
        /// </summary>
        /// <returns></returns>
        public static ulong GetMemoryTotal()
        {
            Object res = WMIQuery("TotalPhysicalMemory", "Win32_ComputerSystem");
            return res == null ? 0 : Convert.ToUInt64(res);
        }

        /// <summary>
        /// 获取已用内存量，返回单位为KB
        /// </summary>
        /// <returns></returns>
        public static ulong GetMemoryUsed()
        {
            return Convert.ToUInt64(GetMemoryTotal() - MemoryCounter.NextValue());
        }

        /// <summary>
        /// 获取分区总容量
        /// </summary>
        /// <returns></returns>
        public static ulong[] GetPartionTotal()
        {
            var drive = DriveInfo.GetDrives();
            ulong[] totals = new ulong[drive.Length];
            int size = 0;
            for (int i = 0; i < drive.Length; i++)
            {
                if (drive[i].DriveType == DriveType.Fixed)
                {
                    totals[size] = Convert.ToUInt64(drive[i].TotalSize);
                    size ++ ;
                }
            }
            Array.Resize(ref totals, size);
            return totals;
        }

        /// <summary>
        /// 获取分区已用容量
        /// </summary>
        /// <returns></returns>
        public static ulong[] GetPartionUsed()
        {
            var drive = DriveInfo.GetDrives();
            ulong[] used = new ulong[drive.Length];
            for (int i = 0; i < drive.Length; i++)
            {
                if (drive[i].DriveType == DriveType.Fixed)
                {
                    used[i] = Convert.ToUInt64(drive[i].TotalSize - drive[i].AvailableFreeSpace);
                }
            }
            return used;
        }

        /// <summary>
        /// 获取磁盘总容量
        /// </summary>
        /// <returns></returns>
        public static ulong GetDiskTotal()
        {
            ulong[] all = GetPartionTotal();
            ulong total = 0;
            foreach (ulong it in all)
            {
                total += it;
            }
            return total;
        }

        /// <summary>
        /// 获取磁盘已用容量
        /// </summary>
        /// <returns></returns>
        public static ulong GetDiskUsed()
        {
            ulong[] all = GetPartionUsed();
            ulong used = 0;
            foreach (ulong it in all)
            {
                used += it;
            }
            return used;
        }

        /// <summary>
        /// 获取分区文件系统格式
        /// </summary>
        /// <returns></returns>
        public static string[] GetDriveFormat()
        {
            var drive = DriveInfo.GetDrives();
            string[] formats = new string[drive.Length];
            for (int i = 0; i < drive.Length; i++)
            {
                if (drive[i].DriveType==DriveType.Fixed)
                {
                    formats[i] = drive[i].DriveFormat;
                }
            }
            return formats;
        }

        /// <summary>
        /// 假装磁盘就一个分区，获取第一个可用分区的分区格式
        /// </summary>
        /// <returns></returns>
        public static string GetDiskFormat()
        {
            string[] formats = GetDriveFormat();
            if (formats.Length > 0)
            {
                return formats[2];
            }
            return "";
        }

        /// <summary>
        /// 获取磁盘写入速率
        /// </summary>
        /// <returns></returns>
        public static uint GetDiskWSpeed()
        {
            return Convert.ToUInt32(DiskDWCounter.NextValue());
        }

        /// <summary>
        /// 获取磁盘读取速率
        /// </summary>
        /// <returns></returns>
        public static uint GetDiskRSpeed()
        {
            return Convert.ToUInt32(DiskDRCounter.NextValue());
        }

        /// <summary>
        /// 获取磁盆读写速率
        /// </summary>
        /// <returns></returns>
        public static uint GetDiskDIOSpeed()
        {
            return Convert.ToUInt32(DiskDIOCounter.NextValue());
        }

        /// <summary>
        /// 获取磁盘IOPS
        /// </summary>
        /// <returns></returns>
        public static uint GetDiskIOSpeed()
        {
            return Convert.ToUInt32(DiskIOCounter.NextValue());
        }

        /// <summary>
        /// 获取网卡型号
        /// </summary>
        /// <returns></returns>
        public static string GetNetCardType()
        {
            //string path = "SYSTEM\\CurrentControlSet\\Control\\Network\\{4D36E972-E325-11CE-BFC1-08002BE10318}\\";
            //var interfaces = NetworkInterface.GetAllNetworkInterfaces();
            //List<string> cards = new List<string>();
            //foreach (var adapter in interfaces)
            //{
            //    string key = path + adapter.Id + "\\Connection";
            //    RegistryKey rk = Registry.LocalMachine.OpenSubKey(key, false);
            //    if (rk != null)
            //    {
            //        string pnpInstanceID = rk.GetValue("PnpInstanceID", "").ToString();
            //        if (pnpInstanceID.Length > 3 && pnpInstanceID.Substring(0, 3) == "PCI")
            //        {
            //            cards.Add(adapter.Description);
            //        }
            //    }
            //}
            //return cards;
            
            foreach (NetworkInterface inter in NetworkInterface.GetAllNetworkInterfaces())
            {
                if ((inter.NetworkInterfaceType == NetworkInterfaceType.GigabitEthernet ||
                    inter.NetworkInterfaceType == NetworkInterfaceType.Ethernet ||
                    inter.NetworkInterfaceType == NetworkInterfaceType.Wireless80211) &&
                    inter.OperationalStatus == OperationalStatus.Up)
                {
                    return inter.Description;
                }
            }
            return "";

        }

        /// <summary>
        /// 获取网卡MAC地址
        /// </summary>
        /// <returns></returns>
        public static string GetMACAddress()
        {
            //string path = "SYSTEM\\CurrentControlSet\\Control\\Network\\{4D36E972-E325-11CE-BFC1-08002BE10318}\\";
            //var interfaces = NetworkInterface.GetAllNetworkInterfaces();
            //List<string> macs = new List<string>();
            //foreach (var adapter in interfaces)
            //{
            //    string key = path + adapter.Id + "\\Connection";
            //    RegistryKey rk = Registry.LocalMachine.OpenSubKey(key, false);
            //    if (rk != null)
            //    {
            //        string pnpInstanceID = rk.GetValue("PnpInstanceID", "").ToString();
            //        if (pnpInstanceID.Length > 3 && pnpInstanceID.Substring(0, 3) == "PCI")
            //        {
            //            string mac = adapter.GetPhysicalAddress().ToString();

            //            macs.Add(mac);
            //        }
            //    }
            //}
            //return macs;

            NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface inter in interfaces)
            {
                if ((inter.NetworkInterfaceType == NetworkInterfaceType.GigabitEthernet ||
                    inter.NetworkInterfaceType == NetworkInterfaceType.Ethernet ||
                    inter.NetworkInterfaceType == NetworkInterfaceType.Wireless80211) &&
                    inter.OperationalStatus == OperationalStatus.Up)
                {
                    return inter.GetPhysicalAddress().ToString();
                }
            }
            return "";

        }

        /// <summary>
        /// 获取本机IPV4地址
        /// </summary>
        /// <returns></returns>
        public static string GetIPV4Address()
        {
            IPHostEntry myEntry = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress add in myEntry.AddressList)
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

        /// <summary>
        /// 获取当前正在使用的网卡
        /// </summary>
        /// <returns></returns>
        public static string GetActiveNetcard()
        {
            NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface inter in interfaces)
            {
                if ((inter.NetworkInterfaceType == NetworkInterfaceType.GigabitEthernet||
                    inter.NetworkInterfaceType == NetworkInterfaceType.Ethernet||
                    inter.NetworkInterfaceType == NetworkInterfaceType.Wireless80211)&&
                    inter.OperationalStatus == OperationalStatus.Up)
                {
                    return inter.Description.Replace("#","_");
                }
            }
            return null;
        }

        /// <summary>
        /// 获取当前带宽
        /// </summary>
        /// <param name="networkCard"></param>
        /// <returns></returns>
        public static float GetNetworkBandWidth()
        {
           return bandwidthCounter.NextValue();
        }

        /// <summary>
        /// 获取当前网络接受速率
        /// </summary>
        /// <returns></returns>
        public static uint GetNetworkIn()
        {
            if(dataReceiveCounter.InstanceName != null)
            {
                return Convert.ToUInt32(dataReceiveCounter.NextValue());
            }
            return 0;
        }

        /// <summary>
        /// 获取当前网络发送速率
        /// </summary>
        /// <returns></returns>
        public static uint GetNetworkOut()
        {
            if (dataSentCounter.InstanceName != null)
            {
                return Convert.ToUInt32(dataSentCounter.NextValue());
            }
            return 0;
        }

        /// <summary>
        /// 获取进程信息列表
        /// </summary>
        /// <returns></returns>
        public static List<Proces> GetProcessList()
        {
            List<Proces> res = new List<Proces>();

            Process[] pcs = Process.GetProcesses();
            foreach(var pc in pcs)
            {
                Proces p = new Proces { Name = pc.ProcessName, Memory = pc.PagedMemorySize64 };
                try
                {
                    p.Description = pc.MainModule.FileVersionInfo.FileDescription;
                }
                catch (Exception)
                {
                    p.Description = pc.ProcessName;
                }
                finally
                {
                    res.Add(p);
                }

            }
            return res;}


        }
}
