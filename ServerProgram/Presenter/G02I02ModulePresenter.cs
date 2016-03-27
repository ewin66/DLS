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
    public class G02I02ModulePresenter : IG02I02ModulePresenter
    {
        public IG02I02Module CurrentForm { get; set; }

        /// <summary>
        /// 생성자
        /// </summary>
        public G02I02ModulePresenter(IG02I02Module view)
        {
            CurrentForm = view;
            //CurrentForm.Tab1SearchEvent += new EventHandler(CurrentForm_OnTab1Search);
            CommandCenter.GraphSearchChanged.Executed += new EventHandler<ExecutedEventArgs>(GraphSearchChanged_Executed);
        }

        void GraphSearchChanged_Executed(object sender, ExecutedEventArgs e)
        {
            string messgae = "불러오기를 완료하였습니다.";
            AMR_MST04Model model = new AMR_MST04Model();
            
            model = (AMR_MST04Model)e.Parameter;
            
            MySqlManage crud = new MySqlManage(ConfigurationManager.ConnectionStrings["MySQL"].ConnectionString);

            string query = string.Format("select TOT00DAT, {0} from amr_tot00 " +
                "where TOT00SNO = '{1}' and TOT00DAT between '{2}' and '{3}' " + 
                "order by TOT00DAT asc ",
                model.Sensor,
                model.MST04SNO,
                model.From.ToString("yyyy-MM-dd"),
                model.To.ToString("yyyy-MM-dd"));
            DataSet ds = new DataSet();
            ds = crud.SelectMariaDBTable(crud.Connection, query);
            //ds = crud.CallSPMariaDBTable(crud.Connection, query);
            
            if (ds.Tables.Count > 0)
            {
                model.DataTable = ds.Tables[0];
                
                CurrentForm.SearchComplete(model);
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
