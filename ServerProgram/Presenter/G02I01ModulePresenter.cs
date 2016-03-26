using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using ServerProgram.DB;
using DevExpress.MailClient.Win;

namespace ServerProgram
{
    public class G02I01ModulePresenter : IG02I01ModulePresenter
    {
        public IG02I01Module CurrentForm { get; set; }

        /// <summary>
        /// 생성자
        /// </summary>
        public G02I01ModulePresenter(IG02I01Module view)
        {
            CurrentForm = view;
            //CurrentForm.Tab1SearchEvent += new EventHandler(CurrentForm_OnTab1Search);
            CommandCenter.StateChanged.Executed += new EventHandler<ExecutedEventArgs>(StateChanged_Executed);
        }

        void StateChanged_Executed(object sender, ExecutedEventArgs e)
        {
            AMR_MST04 data = new AMR_MST04();
                
            data = (AMR_MST04)e.Parameter;

            MySqlManage crud = new MySqlManage(ConfigurationManager.ConnectionStrings["MySQL"].ConnectionString);

            string query = string.Format("select TOT00DAT, TOT00PW1,  TOT00WT1, TOT00GS1, TOT00HT1, TOT00CL1 from amr_tot00 where TOT00SNO = '{0}' order by TOT00DAT asc",
                data.MST04SNO);
            DataSet ds = new DataSet();
            ds = crud.SelectMariaDBTable(crud.Connection, query);

            AMR_MST04Model model = new AMR_MST04Model();
            model.DataTable = ds.Tables[0];
            CurrentForm.Tab1SearchComplete(model);
            CurrentForm.ShowMessage("불러오기를 완료하였습니다.");
        }

        void CurrentForm_OnTab1Search(object sender, EventArgs e)
        {
            //AMR_MST04Model model = new AMR_MST04Model();
            //CurrentForm.Tab1SearchComplete(model);

        }
    }
}
