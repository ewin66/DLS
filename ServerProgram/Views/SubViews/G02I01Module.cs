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
    public partial class G02I01Module : BaseModule, IBaseSubView, IG02I01Module
    {



        public G02I01Module()
        {
            InitializeComponent();
            InitializeSubViews();

            mMST04 = new AMR_MST04();

            MainPresenter = new G02I01ModulePresenter(this);
        }

        AMR_MST04 mMST04;
        
        G02I01ModuleTab1 tab1;

        event EventHandler IG02I01Module.Tab1SearchEvent
        {
            add { this.tab1.Tab1SearchEvent += value; }
            remove { this.tab1.Tab1SearchEvent -= value; }
        }

        /// <summary>
        /// 서브폼 추가
        /// </summary>
        public void InitializeSubViews()
        {
            tab1 = new G02I01ModuleTab1();
            tab1.Dock = DockStyle.Fill;
            this.xtraTabPage1.Controls.Add(tab1);

        }

        protected internal override void ButtonClick(string tag)
        {
            switch (tag)
            {
                case TagResources.LoginHistorySearch:


                    break;
                case TagResources.ContactNew:

                    break;
            }
        }


        private void textEdit1_Click(object sender, EventArgs e)
        {
            //LoadMST04();

        }



        //DialogResult LoadMST04()
        //{
            ////if (mst04 == null) return DialogResult.Ignore;
            //AMR_MST04 mst04 = new AMR_MST04();
            //DialogResult ret = DialogResult.Cancel;
            //Cursor.Current = Cursors.WaitCursor;
            //using (fmLoad_AMR_MST04 frm = new fmLoad_AMR_MST04(mst04, OwnerForm.Ribbon))
            //{
            //    frm.StartPosition = FormStartPosition.CenterParent;
            //    ret = frm.ShowDialog(OwnerForm);
            //}
            ////UpdateCurrentContact();
            ////textEdit1.Text = mst04.MST04DON + " / " + mst04.MST04HNO;

            //mMST04.MST04SNO = mst04.MST04SNO;
            //mMST04.MST04DON = mst04.MST04DON;
            //mMST04.MST04HNO = mst04.MST04HNO;

            //Cursor.Current = Cursors.Default;
            //return ret;
        //}

        private void sbSearch_Click(object sender, EventArgs e)
        {
            //this.gcGrid.SafeInvoke(d => d.DataSource = null);
            //MySqlManage crud = new MySqlManage(ConfigurationManager.ConnectionStrings["MySQL"].ConnectionString);

            //string query = string.Format("select TOT00DAT, TOT00PW1,  TOT00WT1, TOT00GS1, TOT00HT1, TOT00CL1 from amr_tot00 where TOT00SNO = '{0}' order by TOT00DAT asc",
            //    mMST04.MST04SNO);
            //DataSet ds = new DataSet();
            //ds = crud.SelectMariaDBTable(crud.Connection, query);
            //this.gcGrid.DataSource = ds.Tables[0];


            //if (RefreshEvent != null)
            //    RefreshEvent(this, EventArgs.Empty);
        }


        #region IBaseSubView

        
        public void DataBinding(IBaseModel datalist)
        {
            //bindingSource1.DataSource = datalist;
        }

        #endregion IBaseSubView


        #region IG02I01Module

        /// <summary>
        /// 현재 사용 데이터모델을 지정/반환합니다.
        /// </summary>
        public IBaseModel CurrentData { get; set; }

        /// <summary>
        /// 컨트롤러를 지정/반환합니다.
        /// </summary>
        public IG02I01ModulePresenter MainPresenter { get; set; }


        public void ShowMessage(string msg)
        {

        }

        public void Tab1SearchComplete(IBaseModel item)
        {
            if (item == null)
                return;

            CurrentData = item;
            tab1.DataBinding(this.CurrentData);
        }

        public void DongLoadComplete(IBaseModel loadItem)
        { }


        public void LoadComplete(IBaseModel loadItem)
        {
            if (loadItem == null)
                return;

            CurrentData = loadItem;
            DataBinding(this.CurrentData);
        }

        #endregion

        private void sbLoingHistorySearch_Click(object sender, EventArgs e)
        {

        }
    }
}
