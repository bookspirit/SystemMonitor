using System.Threading;

namespace SystemMonitor
{
    public class WatcherThread
    {
        Thread thread;

        bool Sign = false;//停止标记,false表示请求停止线程

        public WatcherThread()
        {
            thread = new Thread(Run);
        }

        public void Start()
        {
            if (thread != null && thread.ThreadState == ThreadState.Unstarted)
            {
                Sign = true;
                thread.Start();
            }
        }

        public void Stop()
        {
                Sign = false;
        }
        /// <summary>
        /// 2秒每次，刷新数据
        /// </summary>
        void Run()
        {            
            while (Sign)
            {
                //do task here
                Thread.Sleep(2000);
                App.information.UpdatePerformance();
                Record();
            }
        }

        void Record()
        {
            
        }
    }
}
