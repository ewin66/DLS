using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
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

using System.Runtime.InteropServices;

using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;

using System.Collections;
using ServerProgram.DB;
using ServerProgram.Serial;
using ServerProgram;

namespace DevExpress.ProductsDemo.Win.Modules
{

    public partial class G03I01Module : BaseModule, IG03I01Module, IBaseSubView
    {

        public G03I01Module()
        {
            InitializeComponent();
            MainPresenter = new G03I01ModulePresenter(this);

            //// 실시간 데이터 저장 테이블
            //mCommTable = initCommDataTable();
            //mDataTable = initCommDataTable();
            //InitGridData();


            //timer1.Interval = 1000;
            //timer1.Tick += new EventHandler(uiUpdateTimer_Tick);

            //mWorkArmTot00 = new WORK_AMR_TOT00();
            //mWorkArmTot00.eReady += mWorkArmTot00_eReady;
            //mInsertThread = new Thread(new ThreadStart(mWorkArmTot00.DoWork));
            //mInsertThread.IsBackground = true;


        }
        private DK1Interface comm;
        private WORK_AMR_TOT00 mWorkArmTot00;
        private Thread mInsertThread;
        private MySqlManage crud;

        public const int ColumnCount = 10, RowCount = 40;
        private System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();

        //DataTable mSensorInfoTable = new DataTable();
        DataTable mCommTable = new DataTable();
        DataTable mErrorTable = new DataTable();
        DataTable mDataTable = new DataTable();


        // 시간별 검침 데이터 Insert
        void mWorkArmTot00_eReady(object sender, EventArgs e)
        {
            int offset = 4;
            mCommTable = comm.CommTable;

            //DataRow foundErrRow = mErrorTable.Rows.Find(string.Format("0x{0:D2}", slaveId));

            foreach (DataRow row in mCommTable.Rows)
            {
    //`TOT00SNO` INT(4) NOT NULL COMMENT '세대 번호',
    //`TOT00DAT` CHAR(20) NOT NULL COMMENT '검침 일시',
    //`TOT00PW1` VARCHAR(8) NULL DEFAULT NULL COMMENT '전력 검침 값',
    //`TOT00WT1` VARCHAR(8) NULL DEFAULT NULL COMMENT '수도 검침 값',
    //`TOT00GS1` VARCHAR(8) NULL DEFAULT NULL COMMENT '가스 검침 값',
    //`TOT00CL1` VARCHAR(8) NULL DEFAULT NULL COMMENT '열량 검침 값',
    //`TOT00HT1` VARCHAR(8) NULL DEFAULT NULL COMMENT '온수 검침 값',
    //`TOT00CO1` VARCHAR(8) NULL DEFAULT NULL COMMENT '냉방 검침 값',
                //mCommTable.Rows.Add(row[0], "null", row[1], row[2], "null", "null", "null", "null", "null", "null", "null");
                offset = 4;

                // 빠른 테스트를 위해 이전시간값을 가져온다.
                string dat = row.ItemArray[1].ToString();
                DateTime dtDate = DateTime.ParseExact(dat, "yyyy-MM-dd HH", null);
                
                string sql = string.Format("insert into amr_tot00 values('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}')", 
                    row.ItemArray[0],
                    dtDate.AddHours(+1).ToString("yyyy-MM-dd HH"),
                    row.ItemArray[offset++], row.ItemArray[offset++], row.ItemArray[offset++], row.ItemArray[offset++], row.ItemArray[offset++], row.ItemArray[offset++]);


                crud.InsertMariaDB(crud.Connection, sql);

                // 빠른 테스트를 위해 임의로 한시간씩 증가한다.
                string updatehour = dtDate.AddHours(+1).ToString("yyyy-MM-dd HH");
                row["검침시간"] = updatehour;
            }
        }


        public IList CreateData()
        {
            
            RecordCollection coll = new RecordCollection();
            for (int n = 0; n < mCommTable.Rows.Count; n++)
            {
                Record row = new Record();
                coll.Add(row);
            }
            return coll;
        }



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
                //case TagResources.ReadingStart:
                //    CommandCenter.ReadingChanged.Execute("RUN");

                //    break;
                //case TagResources.ReadingStop:
                //    CommandCenter.ReadingChanged.Execute("STOP");

                //    break;
                default:
                    
                    
                    
                    break;
            }
        }

        public void InsertThread()
        {

        }


        /// <summary>
        /// UI 업데이트 타이머
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uiUpdateTimer_Tick(object sender, EventArgs e)
        {
  
            mCommTable = comm.CommTable;

            int rowCount = 0;
            foreach (DataRow row in mCommTable.Rows)
            {
                int colCount = 0;
                foreach (object item in row.ItemArray)
                {
                    SetValue(gridControl1.DataSource, rowCount, colCount++, item);
                }
                rowCount++;
                    
            }
 
        }

        //<label1>
        void SetValue(object data, int row, int column, object val)
        {
            RecordCollection rc = data as RecordCollection;
            rc.SetValue(row, column, val);
        }


        /// <summary>
        /// 테이블 생성
        /// </summary>
        /// <returns></returns>
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
        
        /// <summary>
        /// 테이블 기본값 입력
        /// </summary>
        void InitGridData()
        {
            this.gridControl1.SafeInvoke(d => d.DataSource = null);
            crud = new MySqlManage(ConfigurationManager.ConnectionStrings["MySQL"].ConnectionString);

            string query = "select MST04SNO, MST04DON, MST04HNO from amr_mst04";
            DataSet ds = new DataSet();
            ds = crud.SelectMariaDBTable(crud.Connection, query);

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                mDataTable.Rows.Add(row[0], DateTime.Now.ToString("yyyy-MM-dd hh"), row[1], row[2], "null", "null", "null", "null", "null", "null", "null");
                mCommTable.Rows.Add(row[0], DateTime.Now.ToString("yyyy-MM-dd hh"), row[1], row[2], "null", "null", "null", "null", "null", "null", "null");
            }
            
            //gridControl1.DataSource = mSensorInfoTable;

            gridControl1.DataSource = CreateData();
        }


        /// <summary>
        /// 통신 이벤트 수신
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void comm_eDataReceive(object sender, DK1EventArgs e)
        {
            if (e.GetType().Equals(typeof(DK1DataArgs)))
            {

                // add log
                DK1DataArgs test = (DK1DataArgs)e;
                if (memoEdit1.Text.Length == 1024)
                    memoEdit1.SafeInvoke(d => d.Text = (DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss [RECV] ") + test.Data + "\r\n"));
                else
                memoEdit1.SafeInvoke(d => d.Text += (DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss [RECV] ") + test.Data + "\r\n"));

                memoEdit1.SafeInvoke(d => d.SelectionStart = memoEdit1.Text.Length);
                memoEdit1.SafeInvoke(d => d.ScrollToCaret());

                //byte[] data = DK1Util.HexStringToByteArray(test.Data);
                //SetValue(gridControl1.DataSource, 3, 5, Convert.ToInt64(data[15]));

            }
        }


        /// <summary>
        /// 통신 시작
        /// </summary>
        /// <returns></returns>
        public bool Run()
        {

            //if( comm == null )

            // 시리얼 통신
            if ( comm == null || !comm.actived )
            {
                comm = new DK1Interface(mDataTable, mCommTable, mErrorTable, 1000);
                comm.eDataReceive += new EventHandler<DK1EventArgs>(comm_eDataReceive);
            }

            // UI 업데이트 타이머 시작되지 않았다면 시작
            if (!timer1.Enabled)
                timer1.Enabled = true; 

            return true;

        }

        /// <summary>
        /// 통신 종료
        /// </summary>
        public void Stop()
        {
            // UI 업데이트 종료
            timer1.Enabled = false;

            comm.Dispose();
            //GC.SuppressFinalize(comm);


        }




        #region IG03I01Module

        /// <summary>
        /// 현재 사용 데이터모델을 지정/반환합니다.
        /// </summary>
        public IBaseModel CurrentData { get; set; }

        /// <summary>
        /// 컨트롤러를 지정/반환합니다.
        /// </summary>
        public IG03I01ModulePresenter MainPresenter { get; set; }


        public void ShowMessage(string msg)
        {
            MessageBox.Show(msg);
        }

        public void ReadingComplete(IBaseModel item)
        {
            if (item == null)
                return;

            AMR_MST04Model model = new AMR_MST04Model();
            model = (AMR_MST04Model)item;

            CurrentData = item;

            this.DataBinding(CurrentData);
            //if (model.Name.Equals("Tab1"))
            //    tab1.DataBinding(this.CurrentData);
            //else
            //    tab2.DataBinding(this.CurrentData);
        }

        #endregion
                

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

            }

        }
        #endregion  

    }




}
