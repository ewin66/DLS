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
    public partial class G02I01ModuleTab2 : UserControl, IBaseSubView
    {

        public G02I01ModuleTab2()
        {
            InitializeComponent();

            //this.gridControl1.DataBindings.Add(new System.Windows.Forms.Binding("DataSource", this.bindingSource1, "DataTable", true ));

            this.dateEdit1.DateTime = DateTime.Now;

            mMST04 = new AMR_MST04Model();

        }

        AMR_MST04Model mMST04;

        /// <summary>
        /// 조회할 동 가져오기
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textEdit1_Click_1(object sender, EventArgs e)
        {

            fmLoad_AMR_MST04 frm = new fmLoad_AMR_MST04();
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();

            textEdit1.Text = frm.bindingMst04.MST04DON + " / " + frm.bindingMst04.MST04HNO;

            mMST04.MST04SNO = frm.bindingMst04.MST04SNO;
            mMST04.MST04DON = frm.bindingMst04.MST04DON;
            mMST04.MST04HNO = frm.bindingMst04.MST04HNO;

        }

        /// <summary>
        /// 조회
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (mMST04 != null)
            {
                mMST04.From = this.dateEdit1.DateTime;
                mMST04.To = this.dateEdit1.DateTime.AddDays(+1);
                mMST04.Name = "Tab2";
                CommandCenter.StateChanged.Execute(mMST04);
            }
                
        }
        


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
                AMR_MST04Model model = new AMR_MST04Model();

                model = (AMR_MST04Model)datalist;
                gridControl1.DataSource = null;
                gridControl1.DataSource = model.DataTable;
                //bindingSource1.DataSource = datalist;

            }
        }
        #endregion



    }
}
