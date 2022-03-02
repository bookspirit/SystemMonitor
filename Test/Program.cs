using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Threading;
using System.Timers;

using SystemMonitor.Tool;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Util.GetNetCardType());
            Console.WriteLine(Util.GetMACAddress());
            Console.WriteLine(Util.GetIPV4Address());
            Console.WriteLine(Util.GetIPV6Address());
            for (int i = 0; i < Util.GetPartionTotal().Length; i++)
            {
                Console.WriteLine(Util.GetPartionTotal()[i] / 1024 / 1024 / 1024);
            }
            for (int i = 0; i < Util.GetDriveFormat().Length; i++)
            {
                Console.WriteLine(Util.GetDriveFormat()[i]);
            }
            Console.WriteLine(Util.GetDiskTotal() / 1024 / 1024 / 1024);
            Console.WriteLine(Util.GetDiskTotal() / 1024 / 1024 / 1024);
            Console.WriteLine((float)(Util.GetMemoryTotal() - Util.GetMemoryUsed()) / 1024 / 1024);
            Console.WriteLine(Util.GetNetworkBandWidth());
            Console.WriteLine(String.Format("In: {0}, Out: {1}", Util.GetNetworkIn(), Util.GetNetworkOut()));
            Console.WriteLine(Util.GetActiveNetcard());
            Console.WriteLine(Util.GetCpuType());
            Console.WriteLine(Util.GetMemoryTotal());
            for(int i = 0; i < 10; i++)
            {
                Console.WriteLine("Input:"+Util.GetNetworkIn());
                Console.WriteLine("Output:" + Util.GetNetworkOut());
                Thread.Sleep(1000);
            }
            Console.WriteLine(Util.GetCpuPerformance());
            Process[] ps = Process.GetProcesses();
            foreach (Process p in ps)
            {
                Console.WriteLine(p.ProcessName + "\t" + p.MachineName + "\t" + p.SessionId);
            }
            Console.WriteLine(Util.GetNetCardType());

            PerformanceCounterCategory pcc = new PerformanceCounterCategory("Network Interface");
            foreach (string pc in pcc.GetInstanceNames())
            {
                Console.WriteLine(pc);
            }
            Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface inter in interfaces)
            {
                Console.WriteLine("Description:"+inter.Description
                    +"\nId:"+inter.Id
                    + "\nIsReceiveOnly:" + inter.IsReceiveOnly
                    + "\nName:" + inter.Name
                    + "\nNetworkInterfaceType:" + inter.NetworkInterfaceType
                    + "\nOperationalStatus:" + inter.OperationalStatus
                    + "\nSpeed:" + inter.Speed
                    + "\nSupportsMulticast:" + inter.SupportsMulticast
                    );
                Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            }

            Console.WriteLine("All Performance Counter Categories");
            foreach (PerformanceCounterCategory performanceCounterCategory in PerformanceCounterCategory.GetCategories())
            {
                Console.WriteLine(performanceCounterCategory.CategoryName);
                Console.WriteLine("----------------------------------------------------------");
                foreach (string name in performanceCounterCategory.GetInstanceNames())
                {
                    Console.WriteLine("\t\t" + name);
                }
                Console.WriteLine();
            }
            Console.Write("Press any key to exit");
            Console.ReadKey();
        }

    }
}
