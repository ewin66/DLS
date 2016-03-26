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
            string messgae = "불러오기를 완료하였습니다.";
            AMR_MST04Model model = new AMR_MST04Model();
            
            model = (AMR_MST04Model)e.Parameter;

            MySqlManage crud = new MySqlManage(ConfigurationManager.ConnectionStrings["MySQL"].ConnectionString);

            string query = string.Format("select TOT00DAT, TOT00PW1,  TOT00WT1, TOT00GS1, TOT00HT1, TOT00CL1 from amr_tot00 " +
                "where TOT00SNO = '{0}' and TOT00DAT between '{1}' and '{2}' " + 
                "order by TOT00DAT asc ",
                model.MST04SNO,
                model.from.ToString("yyyy-MM-dd"),
                model.to.ToString("yyyy-MM-dd"));
            DataSet ds = new DataSet();
            ds = crud.SelectMariaDBTable(crud.Connection, query);
            if (ds.Tables.Count > 0)
            {
                model.DataTable = ds.Tables[0];
                
                CurrentForm.Tab1SearchComplete(model);
            }
            CurrentForm.ShowMessage(messgae);
        }

        void CurrentForm_OnTab1Search(object sender, EventArgs e)
        {
            //AMR_MST04Model model = new AMR_MST04Model();
            //CurrentForm.Tab1SearchComplete(model);

        }
    }
}
