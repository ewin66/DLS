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
            DataSet ds = new DataSet();
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand();

            cmd.CommandText = "CALL sp_load(@sno, @datefrom, @dateto);";
            cmd.Parameters.AddWithValue("@sno", model.MST04SNO);
            cmd.Parameters.AddWithValue("@datefrom", model.From.ToString("yyyy-MM-dd") + " 00");
            cmd.Parameters.AddWithValue("@dateto", model.To.ToString("yyyy-MM-dd") + " 00");

            ds = crud.CallSPMariaDBTable(crud.Connection, cmd);

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
