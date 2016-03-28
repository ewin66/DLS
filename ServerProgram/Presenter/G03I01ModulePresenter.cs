using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using ServerProgram.DB;
using DevExpress.MailClient.Win;

using DevExpress.ProductsDemo.Win.Item;
using ServerProgram.Serial;

namespace ServerProgram
{
    public class G03I01ModulePresenter : IG03I01ModulePresenter
    {
        public IG03I01Module CurrentForm { get; set; }

        /// <summary>
        /// 생성자
        /// </summary>
        public G03I01ModulePresenter(IG03I01Module view)
        {
            CurrentForm = view;
            //CurrentForm.Tab1SearchEvent += new EventHandler(CurrentForm_OnTab1Search);
            //CommandCenter.ReadingChanged.Executed += new EventHandler<ExecutedEventArgs>(ReadingChanged_Executed);
        }

        private DK1Interface comm;


        void ReadingChanged_Executed(object sender, ExecutedEventArgs e)
        {
            if (e.Parameter.ToString().Equals("RUN"))
                ReadingStart();
            else
                ReadingStop();

            CurrentForm.ShowMessage(e.Parameter.ToString());
        }

        void CurrentForm_OnTab1Search(object sender, EventArgs e)
        {
            //AMR_MST04Model model = new AMR_MST04Model();
            //CurrentForm.Tab1SearchComplete(model);

        }

        // 시리얼 통신 시작
        void ReadingStart()
        {
            if (comm == null || !comm.actived)
            {
                comm = new DK1Interface();
                comm.eDataReceive += new EventHandler<DK1EventArgs>(comm_eDataReceive);
            }
        }

        // 시리얼 통신 종료
        void ReadingStop()
        {
            if (comm != null || comm.actived)
            {
                comm.eDataReceive -= new EventHandler<DK1EventArgs>(comm_eDataReceive);
                comm.Dispose();
            }
        }

        /// <summary>
        /// 통신 이벤트 수신
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void comm_eDataReceive(object sender, DK1EventArgs e)
        {
            if (e.GetType().Equals(typeof(DK1DataArgs)))
            {
                // add log
                DK1DataArgs test = (DK1DataArgs)e;

                //CurrentForm.
            }
        }


    }
}
