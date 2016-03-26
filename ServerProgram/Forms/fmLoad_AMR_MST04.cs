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
using ServerProgram;

namespace DevExpress.ProductsDemo.Win.Forms
{
    public partial class fmLoad_AMR_MST04 : DevExpress.XtraEditors.XtraForm, IfmLoad_AMR_MST04
    {

        public fmLoad_AMR_MST04()
        {
            InitializeComponent();

            this.gridControl1.DataBindings.Add(new System.Windows.Forms.Binding("DataSource", this.bindingSource1, "DataTable", true));
        }


        public AMR_MST04Model bindingMst04 = new AMR_MST04Model();


        /// <summary>
        /// 폼 로드
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fmLoad_AMR_MST04_Load(object sender, EventArgs e)
        {
            MainPresenter = new fmLoad_AMR_MST04Presenter(this);
            if (LoadEvent != null)
                LoadEvent(this, EventArgs.Empty);
        }


        #region IfmLoad_AMR_MST04


        /// <summary>
        /// 현재 사용 데이터모델을 지정/반환합니다.
        /// </summary>
        public IBaseModel CurrentData { get; set; }

        /// <summary>
        /// 컨트롤러를 지정/반환합니다.
        /// </summary>
        public IfmLoad_AMR_MST04Presenter MainPresenter { get; set; }


        public event EventHandler LoadEvent;


        /// <summary>
        /// 그리드뷰 바인딩
        /// </summary>
        /// <param name="loadItem"></param>
        public void LoadComplete(IBaseModel loadItem)
        {
            CurrentData = loadItem;
            bindingSource1.DataSource = CurrentData;
        }


        #endregion


        /// <summary>
        /// 세대 선택
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            bindingMst04.MST04SNO = Convert.ToUInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "MST04SNO").ToString());
            bindingMst04.MST04DON = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "MST04DON").ToString();
            bindingMst04.MST04HNO = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "MST04HNO").ToString();

        }


    }



}