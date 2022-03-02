using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SystemMonitor.Tool;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //foreach(string tem in Util.GetNetCardType())
            //Console.WriteLine(tem);
            //foreach(string tm in Util.GetMACAddress())
            //Console.WriteLine(tm);
            //Console.WriteLine(Util.GetIPV4Address());
            //Console.WriteLine(Util.GetIPV6Address());
            //for(int i = 0; i < Util.GetPartionTotal().Length; i++)
            //{
            //    Console.WriteLine(Util.GetPartionTotal()[i]/1024/1024/1024);
            //}
            //for (int i = 0; i < Util.GetDriveFormat().Length; i++)
            //{
            //    Console.WriteLine(Util.GetDriveFormat()[i]);
            //}
            //Console.WriteLine(Util.GetDiskTotal() / 1024 / 1024 / 1024);
            //Console.WriteLine(Util.GetDiskTotal() / 1024 / 1024 / 1024);
            Console.WriteLine((float)Util.GetMemoryFree()/1024/1024);
            Console.ReadKey();
        }
    }
}
