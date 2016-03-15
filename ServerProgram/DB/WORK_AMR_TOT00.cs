using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using DevExpress.ProductsDemo.Win.Item;

namespace DevExpress.ProductsDemo.Win.DB
{
    public class WORK_AMR_TOT00 : Worker
    {

        public override void DoWork()
        {
            while (!_shouldStop)
            {

                TimeSpan a = DateTime.Now.TimeOfDay;
                Console.WriteLine("현재 : " + a.ToString());
                TimeSpan b = new TimeSpan(a.Hours, (a.Minutes / 1 + 1) * 1, 0);
                Console.WriteLine("다음 : " + b.ToString());
                TimeSpan due = b - a;
                Console.WriteLine("대기 : " + due.TotalSeconds.ToString() + " 초");
                System.Threading.Thread.Sleep(due);
                
                //실행할 코드
                //Console.WriteLine("worker thread: working...");
                eHandler(new EventArgs { });

                if (b > DateTime.Now.TimeOfDay)  //시간 오차 해결, 중복 실행 방지
                {
                    System.Threading.Thread.Sleep(100);
                }

            }
            Console.WriteLine("worker thread: terminating gracefully.");
        }

        public override void RequestStop()
        {
            _shouldStop = true;

        }

        private volatile bool _shouldStop;

        public event EventHandler<EventArgs> eReady;
        protected void eHandler(EventArgs e)
        {
            EventHandler<EventArgs> handler = eReady;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
