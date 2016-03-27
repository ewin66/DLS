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
using DevExpress.ProductsDemo.Win.Item;
using DevExpress.MailDemo.Win;
using DevExpress.XtraEditors;
using DevExpress.XtraCharts;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.MailClient.Win;
using ServerProgram;

namespace DevExpress.ProductsDemo.Win.Modules
{
    public partial class G02I02ModuleTab2 : UserControl, IBaseSubView
    {

        public G02I02ModuleTab2()
        {
            InitializeComponent();

            //this.gridControl1.DataBindings.Add(new System.Windows.Forms.Binding("DataSource", this.bindingSource1, "DataTable", true ));

            //SideBySideBarSeriesLabel label = this.chartControl1.Series[0].Label as SideBySideBarSeriesLabel;

            //label.Position = BarSeriesLabelPosition.TopInside;


            this.dateEdit1.DateTime = DateTime.Now;
            this.dateEdit2.DateTime = DateTime.Now.AddDays(+1);

            // "전기", "수도", "온수", "가스", "난방"
            mSensorList = new string[] { "TOT00PW1", "TOT00WT1", "TOT00GS1", "TOT00HT1", "TOT00CL1"  };
            
            mMST04 = new AMR_MST04Model();

        }

        string[] mSensorList;
        AMR_MST04Model mMST04;
        //Dictionary<string, ChartVariable2> chartData = new Dictionary<string, ChartVariable2>();

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
            if (mMST04 != null && this.radioGroup1.SelectedIndex >= 0 )
            {
                mMST04.From = this.dateEdit1.DateTime;
                mMST04.To = this.dateEdit2.DateTime;
                mMST04.Name = "Tab2";
                mMST04.Sensor = mSensorList[this.radioGroup1.SelectedIndex];
                CommandCenter.GraphSearchChanged.Execute(mMST04);
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
                //gridControl1.DataSource = null;
                //gridControl1.DataSource = model.DataTable;

                this.chartControl1.Series[0].Points.BeginUpdate();
                this.chartControl1.Series[0].Points.Clear();

                foreach (DataRow row in model.DataTable.Rows)
                {
                    string time = row["TOT00DAT"].ToString();
                    // 그래프 Y값 추출
                    DateTime datetime = DateTime.ParseExact(time, "yyyy-MM-dd HH", null);

                    // 센서 값 넣기
                    //double value = Convert.ToDouble(row.ItemArray[1]);
                    double value = Convert.ToDouble(row["TOT00VALUE"]);
                    //double[] value = new double[] { Convert.ToDouble(row.ItemArray[1]), Convert.ToDouble(row.ItemArray[2]), 
                    //                            Convert.ToDouble(row.ItemArray[3]), Convert.ToDouble(row.ItemArray[4]), Convert.ToDouble(row.ItemArray[5]) };


                    // 센서별 데이터 넣기
                    ChartVariable2 chartValue = new ChartVariable2();
                    chartValue.data = new SeriesPoint(datetime, value);
                    this.chartControl1.Series[0].Points.Add(chartValue.data);
                }

                this.chartControl1.Series[0].Points.EndUpdate();

                this.chartControl1.Titles.RemoveAt(0);
                ChartTitle chartTitle1 = new ChartTitle();
                chartTitle1.Text = string.Format("{0}동 {1}호 - {2}", model.MST04DON, model.MST04HNO, this.radioGroup1.Text);
                chartTitle1.Alignment = System.Drawing.StringAlignment.Center;
                chartTitle1.Dock = DevExpress.XtraCharts.ChartTitleDockStyle.Top;
                this.chartControl1.Titles.Add(chartTitle1);

            //    ChartTitle chartTitle1 = new ChartTitle();
            //    ChartTitle chartTitle2 = new ChartTitle();
            //    chartTitle1.Text = "Great Lakes Gross State Product";
            //    chartTitle2.Alignment = System.Drawing.StringAlignment.Far;
            //    chartTitle2.Dock = DevExpress.XtraCharts.ChartTitleDockStyle.Bottom;
            //    chartTitle2.Font = new System.Drawing.Font("Tahoma", 8F);
            //    chartTitle2.Text = "From www.bea.gov";
            //    chartTitle2.TextColor = System.Drawing.Color.Gray;
            //    this.chartControl1.Titles.AddRange(new DevExpress.XtraCharts.ChartTitle[] {
            //chartTitle1,
            //chartTitle2});
            }
        }
        #endregion



    }
}
