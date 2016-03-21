using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using System.Configuration;
using DevExpress.ProductsDemo.Win.Controls;
using DevExpress.ProductsDemo.Win.Common;
using DevExpress.ProductsDemo.Win.DB;
using DevExpress.MailDemo.Win;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.Utils.Menu;
using DevExpress.MailClient.Win;

namespace DevExpress.ProductsDemo.Win.Forms
{
    public partial class fmLoad_AMR_MST04 : DevExpress.XtraEditors.XtraForm
    {
        AMR_MST04 mst04, bindingMst04;
        public fmLoad_AMR_MST04()
        {
            InitializeComponent();
        }
        public fmLoad_AMR_MST04(AMR_MST04 mst04, IDXMenuManager menuManager)
        {
            InitializeComponent();
            this.mst04 = mst04;
            this.bindingMst04 = mst04.Clone();
            InitMenuManager(menuManager);
            //gridView1.ShowFindPanel();

            InitData();

        }
        void InitMenuManager(IDXMenuManager menuManager)
        {
            foreach (Control ctrl in lcMain.Controls)
            {
                BaseEdit edit = ctrl as BaseEdit;
                if (edit != null)
                {
                    edit.MenuManager = menuManager;
                }
            }
        }
        private void InitData()
        {

            MySqlManage crud = new MySqlManage(ConfigurationManager.ConnectionStrings["MySQL"].ConnectionString);

            string query = "select MST04SNO, MST04CMP, MST04DON, MST04HNO from amr_mst04";
            DataSet ds = new DataSet();
            ds = crud.SelectMariaDBTable(crud.Connection, query);

            gridControl1.DataSource = ds.Tables[0];

            //gridView1.MakeRowVisible(gridView1.FocusedRowHandle);

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //bindingMst04 = gridView1.GetRow(gridView1.FocusedRowHandle) as AMR_MST04;

            bindingMst04.MST04SNO = Convert.ToUInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "MST04SNO").ToString());
            bindingMst04.MST04DON = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "MST04DON").ToString();
            bindingMst04.MST04HNO = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "MST04HNO").ToString();

            mst04.Assign(bindingMst04);

        }


    }



}