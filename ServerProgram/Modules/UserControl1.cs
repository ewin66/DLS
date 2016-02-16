using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.ProductsDemo.Win.Controls;
using DevExpress.ProductsDemo.Win.Common;

using DevExpress.MailDemo.Win;

namespace DevExpress.ProductsDemo.Win.Modules
{
    public partial class UserControl1 : BaseModule
    {
        public UserControl1()
        {
            InitializeComponent();

            this.deLoginHistoryDateFrom.DateTime = DateTime.Today;
            this.deLoginHistoryDateTo.DateTime = DateTime.Today;


        }


        protected internal override void ButtonClick(string tag)
        {
            switch (tag)
            {
                case TagResources.LoginHistorySearch:
                    this.gcLoingHistoryGrid.SafeInvoke(d => d.DataSource = null);
                    OleDbUserControl crud = new OleDbUserControl();

                    string query = "select * from LoginHistory";
                    DataSet ds = new DataSet();
                    ds = crud.SelectOleDbTable(crud.Connection, query);
                    this.gcLoingHistoryGrid.DataSource = ds.Tables[0];

                    break;
                case TagResources.ContactNew:

                    break;
            }
        }
    }
}
