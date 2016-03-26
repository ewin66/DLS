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

namespace DevExpress.ProductsDemo.Win.Modules
{
    public partial class G02I02Module : BaseModule
    {

        AMR_MST04 mMST04;
        string []mKey;
        Dictionary<string, ChartVariable2> chartData = new Dictionary<string, ChartVariable2>();

        public G02I02Module()
        {
            InitializeComponent();

            this.deLoginHistoryDateFrom.DateTime = DateTime.Today;

            mMST04 = new AMR_MST04();
            mKey = new string[] { "전기", "수도", "온수", "가스", "난방" };

            foreach (string key in mKey)
            {
                ChartVariable2 chartValue = new ChartVariable2();
                chartData.Add(key, chartValue);
            }
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
                this.chartControl1.SafeInvoke(d => d.Series[key].Points.BeginUpdate());
                this.chartControl1.SafeInvoke(d => d.Series[key].Points.Clear());
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
                    this.chartControl1.SafeInvoke(d => d.Series[key].Points.Add(chartData[key].data));
                }
            }

            foreach (string key in mKey)
            {
                this.chartControl1.SafeInvoke(d => d.Series[key].Points.EndUpdate());

            }
            //this.gcGrid.DataSource = ds.Tables[0];

        }


    }
}
