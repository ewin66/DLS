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

        AMR_MST04 mMST04;
        string []mKey;
        Dictionary<string, ChartVariable2> chartData = new Dictionary<string, ChartVariable2>();

        public G02I02Module()
        {
            InitializeComponent();
            InitializeSubViews();

            MainPresenter = new G02I02ModulePresenter(this);


            mMST04 = new AMR_MST04();
            mKey = new string[] { "전기", "수도", "온수", "가스", "난방" };

            foreach (string key in mKey)
            {
                ChartVariable2 chartValue = new ChartVariable2();
                chartData.Add(key, chartValue);
            }
        }
        G02I02ModuleTab1 tab1;
        G02I02ModuleTab2 tab2;

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

        protected internal override void ButtonClick(string tag)
        {
            switch (tag)
            {
                case TagResources.G02I01:
                    break;
            }
        }


        private void textEdit1_Click(object sender, EventArgs e)
        {
            //LoadMST04();

        }



        //DialogResult LoadMST04()
        //{
        //    //if (mst04 == null) return DialogResult.Ignore;
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

        private void sbLoingHistorySearch_Click(object sender, EventArgs e)
        {
            //this.gcGrid.SafeInvoke(d => d.DataSource = null);


            foreach (string key in mKey)
            {
                //this.chartControl1.SafeInvoke(d => d.Series[key].Points.BeginUpdate());
                //this.chartControl1.SafeInvoke(d => d.Series[key].Points.Clear());
            }
            MySqlManage crud = new MySqlManage(ConfigurationManager.ConnectionStrings["MySQL"].ConnectionString);

            string query = string.Format("select TOT00DAT, TOT00PW1,  TOT00WT1, TOT00GS1, TOT00HT1, TOT00CL1 from amr_tot00 where TOT00SNO = '{0}' order by TOT00DAT asc",
                mMST04.MST04SNO);
            DataSet ds = new DataSet();
            ds = crud.SelectMariaDBTable(crud.Connection, query);

            foreach( DataRow row in ds.Tables[0].Rows)
            {
                string time = row["TOT00DAT"].ToString();
                // 그래프 Y값 추출
                DateTime datetime = DateTime.ParseExact(time, "yyyy-MM-dd HH:mm", null);

                // 센서 값 넣기
                double[] value = new double[] { Convert.ToDouble(row.ItemArray[1]), Convert.ToDouble(row.ItemArray[2]), 
                                                Convert.ToDouble(row.ItemArray[3]), Convert.ToDouble(row.ItemArray[4]), Convert.ToDouble(row.ItemArray[5]) };


                int sensorOffset = 0;

                // 센서별 데이터 넣기
                foreach (string key in mKey)
                {
                    chartData[key].data = new SeriesPoint(datetime, value[sensorOffset++]);
                    //this.chartControl1.SafeInvoke(d => d.Series[key].Points.Add(chartData[key].data));
                }
            }

            foreach (string key in mKey)
            {
                //this.chartControl1.SafeInvoke(d => d.Series[key].Points.EndUpdate());

            }
            //this.gcGrid.DataSource = ds.Tables[0];

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
            MessageBox.Show(msg);
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
