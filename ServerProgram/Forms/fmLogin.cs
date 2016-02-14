using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraNavBar;
using DevExpress.XtraEditors.DXErrorProvider;
using DevExpress.MailClient.Win;
using DevExpress.ProductsDemo.Win;
using DevExpress.ProductsDemo.Win.Controls;

namespace DevExpress.ProductsDemo.Win.Forms {
    public partial class fmLogin : XtraForm {
        OleDbUserControl db;
        string user = string.Empty;
        
        public fmLogin() {
            InitializeComponent();

            db = new OleDbUserControl();

        }

        protected override void OnClosing(CancelEventArgs e) {
            base.OnClosing(e);
        }

        private void sbOK_Click(object sender, EventArgs e)
        {
            user = this.textEdit1.Text;
            if (db.loginCheck(user, this.textEdit2.Text))
            {
                db.InsertLoginHistory(user, "로그인");
                
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                XtraMessageBox.Show("입력하신 아이디 또는 비밀번호가 잘못되었습니다.");
            }
            
        }

        private void sbCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        public string User { 
            get { return user; } 
        }
    }
}
