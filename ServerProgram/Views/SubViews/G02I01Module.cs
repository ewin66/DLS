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
    public partial class G02I01Module : BaseModule, IG02I01Module
    {


        public G02I01Module()
        {
            InitializeComponent();
            InitializeSubViews();

            MainPresenter = new G02I01ModulePresenter(this);
        }

        
        G02I01ModuleTab1 tab1;
        G02I01ModuleTab2 tab2;

        /// <summary>
        /// 서브폼 추가
        /// </summary>
        public void InitializeSubViews()
        {
            tab1 = new G02I01ModuleTab1();
            tab1.Dock = DockStyle.Fill;
            this.xtraTabPage1.Controls.Add(tab1);

            tab2 = new G02I01ModuleTab2();
            tab2.Dock = DockStyle.Fill;
            this.xtraTabPage2.Controls.Add(tab2);

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
            MessageBox.Show(msg);
        }

        public void SearchComplete(IBaseModel item)
        {
            if (item == null)
                return;

            AMR_MST04Model model = new AMR_MST04Model();
            model = (AMR_MST04Model)item;

            CurrentData = item;

            if( model.Name.Equals("Tab1") )
                tab1.DataBinding(this.CurrentData);
            else
                tab2.DataBinding(this.CurrentData);
        }

        #endregion

    }
}
