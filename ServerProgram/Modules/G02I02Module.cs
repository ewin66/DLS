using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using DevExpress.ProductsDemo.Win.Forms;
using DevExpress.ProductsDemo.Win.Controls;
using DevExpress.ProductsDemo.Win.Common;
using DevExpress.ProductsDemo.Win.DB;
using DevExpress.MailDemo.Win;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.MailClient.Win;

namespace DevExpress.ProductsDemo.Win.Modules
{
    public partial class G02I02Module : BaseModule
    {

        AMR_MST04 mst04;

        public G02I02Module()
        {
            InitializeComponent();

            this.deLoginHistoryDateFrom.DateTime = DateTime.Today;

  
        }

  
        protected internal override void ButtonClick(string tag)
        {
            switch (tag)
            {
                case TagResources.LoginHistorySearch:
                    this.gcLoingHistoryGrid.SafeInvoke(d => d.DataSource = null);
                    MySqlManage crud = new MySqlManage(ConfigurationManager.ConnectionStrings["MySQL"].ConnectionString);

                    string query = "select IQR03DAT, IQR03AID,  IQR03MNO from amr_iqr03";
                    DataSet ds = new DataSet();
                    ds = crud.SelectMariaDBTable(crud.Connection, query);
                    this.gcLoingHistoryGrid.DataSource = ds.Tables[0];

                    break;
                case TagResources.ContactNew:

                    break;
            }
        }


        private void textEdit1_Click(object sender, EventArgs e)
        {
            LoadMST04(mst04);

        }



        DialogResult LoadMST04(AMR_MST04 mst04)
        {
            //if (mst04 == null) return DialogResult.Ignore;
            if (mst04 == null) mst04 = new AMR_MST04();
            DialogResult ret = DialogResult.Cancel;
            Cursor.Current = Cursors.WaitCursor;
            using (fmLoad_AMR_MST04 frm = new fmLoad_AMR_MST04(mst04, OwnerForm.Ribbon))
            {
                frm.StartPosition = FormStartPosition.CenterParent;
                ret = frm.ShowDialog(OwnerForm);
            }
            //UpdateCurrentContact();
            textEdit1.Text = mst04.MST04DON + " / " + mst04.MST04HNO;
            Cursor.Current = Cursors.Default;
            return ret;
        }


    }
}
