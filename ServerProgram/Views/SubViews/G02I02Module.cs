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
using DevExpress.ProductsDemo.Win.Item;
using DevExpress.MailDemo.Win;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraCharts;
using DevExpress.MailClient.Win;
using ServerProgram.DB;
using ServerProgram;

namespace DevExpress.ProductsDemo.Win.Modules
{
    public partial class G02I02Module : BaseModule, IG02I02Module
    {


        public G02I02Module()
        {
            InitializeComponent();
            InitializeSubViews();

            MainPresenter = new G02I02ModulePresenter(this);

        }

        #region 변수
        private string mModuleName = "검침 데이터 분석";
        G02I02ModuleTab1 tab1;
        G02I02ModuleTab2 tab2;
        #endregion


        /// <summary>
        /// 서브폼 추가
        /// </summary>
        public void InitializeSubViews()
        {
            tab1 = new G02I02ModuleTab1();
            tab1.Dock = DockStyle.Fill;
            this.xtraTabPage1.Controls.Add(tab1);

            tab2 = new G02I02ModuleTab2();
            tab2.Dock = DockStyle.Fill;
            this.xtraTabPage2.Controls.Add(tab2);
        }

        /// <summary>
        /// 리본 버튼 이벤트
        /// </summary>
        /// <param name="tag"></param>
        protected internal override void ButtonClick(string tag)
        {
            switch (tag)
            {
                case TagResources.G02I01:
                    break;
            }
        }


        #region IG02I02Module

        /// <summary>
        /// 현재 사용 데이터모델을 지정/반환합니다.
        /// </summary>
        public IBaseModel CurrentData { get; set; }

        /// <summary>
        /// 컨트롤러를 지정/반환합니다.
        /// </summary>
        public IG02I02ModulePresenter MainPresenter { get; set; }


        public void ShowMessage(string msg)
        {
            XtraMessageBox.Show(msg, mModuleName, MessageBoxButtons.OK, MessageBoxIcon.None);
        }

        public void SearchComplete(IBaseModel item)
        {
            if (item == null)
                return;

            AMR_MST04Model model = new AMR_MST04Model();
            model = (AMR_MST04Model)item;

            CurrentData = item;

            if (model.Name.Equals("Tab1"))
                tab1.DataBinding(this.CurrentData);
            else
                tab2.DataBinding(this.CurrentData);
        }

        #endregion
    }
}
