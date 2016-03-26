using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using ServerProgram.DB;

namespace ServerProgram
{
    public class fmLoad_AMR_MST04Presenter : IfmLoad_AMR_MST04Presenter
    {
        public IfmLoad_AMR_MST04 CurrentForm { get; set; }

        /// <summary>
        /// 생성자
        /// </summary>
        public fmLoad_AMR_MST04Presenter(IfmLoad_AMR_MST04 view)
        {
            CurrentForm = view;
            CurrentForm.LoadEvent += new EventHandler(CurrentForm_Onload);

        }


        void CurrentForm_Onload(object sender, EventArgs e)
        {

            MySqlManage crud = new MySqlManage(ConfigurationManager.ConnectionStrings["MySQL"].ConnectionString);

            string query = "select MST04SNO, MST04CMP, MST04DON, MST04HNO from amr_mst04";
            DataSet ds = new DataSet();
            ds = crud.SelectMariaDBTable(crud.Connection, query);

            AMR_MST04Model model = new AMR_MST04Model();

            model.DataTable = ds.Tables[0];

            CurrentForm.LoadComplete(model);
        }
    }
}
