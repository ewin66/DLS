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
using DevExpress.ProductsDemo.Win;
using DevExpress.MailDemo.Win;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.MailClient.Win;
using ServerProgram;

namespace DevExpress.ProductsDemo.Win.Modules
{
    public partial class G02I01ModuleTab1 : UserControl, IBaseSubView
    {



        public G02I01ModuleTab1()
        {
            InitializeComponent();

            this.gridControl1.DataBindings.Add(new System.Windows.Forms.Binding("DataSource", this.bindingSource1, "DataTable", true));

            mMST04 = new AMR_MST04();

            //MainPresenter = new G02I01ModulePresenter(this);
        }

        AMR_MST04 mMST04;
       
        //public event EventHandler DongLoadEvent;
        public event EventHandler Tab1SearchEvent;

        private void textEdit1_Click_1(object sender, EventArgs e)
        {
            fmLoad_AMR_MST04 frm = new fmLoad_AMR_MST04();
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
            textEdit1.Text = frm.bindingMst04.MST04DON + " / " + frm.bindingMst04.MST04HNO;

            mMST04.MST04SNO = frm.bindingMst04.MST04SNO;
            mMST04.MST04DON = frm.bindingMst04.MST04DON;
            mMST04.MST04HNO = frm.bindingMst04.MST04HNO;
            //frm.CurrentData;
        }

        //private void textEdit1_Click(object sender, EventArgs e)
        //{
        //    //LoadMST04();
        //    if (DongLoadEvent != null)
        //        DongLoadEvent(this, EventArgs.Empty);

        //}
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (Tab1SearchEvent != null)
                Tab1SearchEvent(this, EventArgs.Empty);
        }
        


        //DialogResult LoadMST04()
        //{
            //if (mst04 == null) return DialogResult.Ignore;
            //AMR_MST04 mst04 = new AMR_MST04();
            //DialogResult ret = DialogResult.Cancel;
            //Cursor.Current = Cursors.WaitCursor;
            //using (fmLoad_AMR_MST04 frm = new fmLoad_AMR_MST04(mst04, OwnerForm.Ribbon))
            //{
            //    frm.StartPosition = FormStartPosition.CenterParent;
            //    ret = frm.ShowDialog(OwnerForm);
            //}
            ////UpdateCurrentContact();
            //textEdit1.Text = mst04.MST04DON + " / " + mst04.MST04HNO;

            //mMST04.MST04SNO = mst04.MST04SNO;
            //mMST04.MST04DON = mst04.MST04DON;
            //mMST04.MST04HNO = mst04.MST04HNO;

            //Cursor.Current = Cursors.Default;
        //    return ret;
        //}

        //private void sbSearch_Click(object sender, EventArgs e)
        //{
            //this.gcGrid.SafeInvoke(d => d.DataSource = null);
            //MySqlManage crud = new MySqlManage(ConfigurationManager.ConnectionStrings["MySQL"].ConnectionString);

            //string query = string.Format("select TOT00DAT, TOT00PW1,  TOT00WT1, TOT00GS1, TOT00HT1, TOT00CL1 from amr_tot00 where TOT00SNO = '{0}' order by TOT00DAT asc",
            //    mMST04.MST04SNO);
            //DataSet ds = new DataSet();
            //ds = crud.SelectMariaDBTable(crud.Connection, query);
            //this.gcGrid.DataSource = ds.Tables[0];


            //if (RefreshEvent != null)
            //    RefreshEvent(this, EventArgs.Empty);
        //}



        #region IBaseSubView
        public void DataBinding(IBaseModel datalist)
        {
            if (datalist == null) return;
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate() { this.DataBinding(datalist); });
            }
            else
            {
                bindingSource1.DataSource = datalist;
            }
        }
        #endregion



 



    }
}
