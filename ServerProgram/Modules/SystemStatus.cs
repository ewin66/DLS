﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using DevExpress.MailDemo.Win;
using System.Threading;
using System.Threading.Tasks;
using DevExpress.ProductsDemo.Win.Common;
using DevExpress.ProductsDemo.Win.Item;
using DevExpress.ProductsDemo.Win.DB;
using DevExpress.ProductsDemo.Win.Serial;
using System.Runtime.InteropServices;

namespace DevExpress.ProductsDemo.Win.Modules
{
    public partial class SystemStatus : BaseModule
    {
 
        public SystemStatus()
        {
            InitializeComponent();


            mSensorInfoTable = initCommDataTable();
            InitGridData();

            // 실시간 데이터 저장 테이블
            mCommTable = new DataTable();
            mCommTable = initCommDataTable();


            
            

            //timer1.Interval = 1000;
            //timer1.Tick += new EventHandler(intervalTimer_Tick);

           

            //this.serialConnection.Open("COM1", 115200, 8, System.IO.Ports.Parity.None, System.IO.Ports.StopBits.One);
            //this.serialConnection
            //SerialPort sp = new SerialPort("COM1", 115200, System.IO.Ports.Parity.None, 8, System.IO.Ports.StopBits.One);
            //sp.Open();
            //sp.Write("hi1234");
        }

        private DK1Interface comm;
        private string mCom;
        private string mInterval;
        

        private bool mTimeSync = false;


        //private System.Windows.Forms.Timer timer1;

        DataTable mSensorInfoTable = new DataTable();
        DataTable mCommTable = new DataTable();
        DataTable mErrorTable = new DataTable();
        DataTable mDataTable = new DataTable();


        internal override void ShowModule(bool firstShow)
        {
            base.ShowModule(firstShow);
            //gidControlAptManage.Focus();
            //UpdateActionButtons();
            if (firstShow)
            {
                ButtonClick(TagResources.StartStopRealtimeStatus);
                //    gidControlAptManage.ForceInitialize();
                //    GridHelper.SetFindControlImages(gidControlAptManage);
                //    if (DataHelper.AMR_MST04s.Count == 0) UpdateCurrentContact();
            }

        }

        protected internal override void ButtonClick(string tag)
        {
            switch (tag)
            {
                case TagResources.StartStopRealtimeStatus:

                    break;
                case TagResources.StartStopComStart:
                    Run();
                    break;
                case TagResources.StartStopComStop:
                    Stop();
                    break;
                default:

                    break;
            }
        }


        DataTable initCommDataTable()
        {
            DataTable table = new DataTable();

            try
            {

                
                DataColumn column;
                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "NO";

                // Add the column to the DataTable.Columns collection.
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "검침시간";

                // Add the column to the DataTable.Columns collection.
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "동";

                // Add the column to the DataTable.Columns collection.
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "호";

                // Add the column to the DataTable.Columns collection.
                table.Columns.Add(column);


                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "전기";

                // Add the column to the DataTable.Columns collection.
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "수도";

                // Add the column to the DataTable.Columns collection.
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "온수";

                // Add the column to the DataTable.Columns collection.
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "가스";

                // Add the column to the DataTable.Columns collection.
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "열량";

                // Add the column to the DataTable.Columns collection.
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "냉방";

                // Add the column to the DataTable.Columns collection.
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "통신상태";

                // Add the column to the DataTable.Columns collection.
                table.Columns.Add(column);

                // Make the ID column the primary key column.
                DataColumn[] PrimaryKeyColumns = new DataColumn[2];
                PrimaryKeyColumns[0] = table.Columns["동"];
                PrimaryKeyColumns[1] = table.Columns["호"];
                table.PrimaryKey = PrimaryKeyColumns;


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return table;

        }
        
        void InitGridData()
        {
            this.gridControl1.SafeInvoke(d => d.DataSource = null);
            MySqlManage crud = new MySqlManage();

            string query = "select MST04SNO, MST04DON, MST04HNO from amr_mst04";
            DataSet ds = new DataSet();
            ds = crud.SelectMariaDBTable(crud.Connection, query);

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                mSensorInfoTable.Rows.Add(row[0], "null", row[1], row[2], "null", "null", "null", "null", "null", "null", "null");
            }
            gridControl1.DataSource = mSensorInfoTable;
        }


        void comm_eDataReceive(object sender, DK1EventArgs e)
        {
            if (e.GetType().Equals(typeof(DK1DataArgs)))
            {


                // add log
                DK1DataArgs test = (DK1DataArgs)e;
                memoEdit1.SafeInvoke(d => d.Text += (DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss [RECV] ") + test.Data + "\r\n"));
                memoEdit1.SafeInvoke(d => d.SelectionStart = memoEdit1.Text.Length);
                memoEdit1.SafeInvoke(d => d.ScrollToCaret());

                // update grid view
                //updateDevice( (DK1DataArgs)e );
                //DataRow foundRow = mSensorInfoTable.Rows.Find(new string[2] { "1221", test.address });
                //if (foundRow != null)
                //{
                   
                //    try
                //    {
                //        foundRow[5] = "999999";
                //    }
                //    catch(Exception ex)
                //    {
                //        Console.WriteLine(ex.Message);
                //    }

                //}
            }
        }

        /// <summary>
        /// 통신 시작
        /// </summary>
        /// <returns></returns>
        public bool Run()
        {

            //if( comm == null )
            if ( comm == null || !comm.actived )
            {
                comm = new DK1Interface(mDataTable, mCommTable, mErrorTable, 1000);
                comm.eDataReceive += new EventHandler<DK1EventArgs>(comm_eDataReceive);
            }
                


            return true;

        }

        /// <summary>
        /// 통신 종료
        /// </summary>
        public void Stop()
        {
            
            comm.Dispose();
            //GC.SuppressFinalize(comm);

        }


    }
}
