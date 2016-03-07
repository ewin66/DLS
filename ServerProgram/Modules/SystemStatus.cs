using System;
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



            mCom = "COM1";
            mInterval = "1000";

            //timer1.Interval = 1000;
            //timer1.Tick += new EventHandler(intervalTimer_Tick);

            mComport.DataReceived += mComport_DataReceived;

  

            //this.serialConnection.Open("COM1", 115200, 8, System.IO.Ports.Parity.None, System.IO.Ports.StopBits.One);
            //this.serialConnection
            //SerialPort sp = new SerialPort("COM1", 115200, System.IO.Ports.Parity.None, 8, System.IO.Ports.StopBits.One);
            //sp.Open();
            //sp.Write("hi1234");
        }

        private DK1Interface comm;
        private string mCom;
        private string mInterval;
        private SerialPort mComport = new SerialPort();

        private bool mTimeSync = false;
        Thread workerThread;

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

        /// <summary>
        /// 시리얼 데이터 수신
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void mComport_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int bytes = mComport.BytesToRead;

            byte[] buffer = new byte[bytes];

            mComport.Read(buffer, 0, bytes);

            memoEdit1.SafeInvoke(d => d.Text += (DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss [RECV] ") + DK1Util.ByteArrayToHexString(buffer) + "\r\n"));
            memoEdit1.SafeInvoke(d => d.ScrollToCaret());

            //throw new NotImplementedException();
        }

        /// <summary>
        /// UI 업데이트 타이머
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void intervalTimer_Tick(object sender, EventArgs e)
        {
            //int index = 0;

            //mCommTable = mComm.CommTable;
            //mErrorTable = mComm.ErrorTable;
            //DataTable errorList = mComm.ErrorListTable;
            //foreach (DataRow row in mCommTable.Rows)
            //{
            //    index = mCommTable.Rows.IndexOf(row);
            //    BMSStatusDataGridViewInvoke(dataGridViewBMSStatus, index, row, mErrorTable.Rows[index]);
            //}
            //mComport.Write("hi123433");

        }

        #region thread

        private volatile bool _shouldStop;

        /// <summary>
        ///  스레드 시작
        /// </summary>
        public void RequestStart()
        {
            _shouldStop = false;
        }
        /// <summary>
        ///  스레드 종료
        /// </summary>
        public void RequestStop()
        {
            _shouldStop = true;
        }
              /// <summary>
        /// 모드버스 통신 스레드
        /// </summary>
        /// <param name="cycle"></param>
        public void DoWork(object cycle)
        {
            int timer = Convert.ToInt32(cycle);

            byte[] item = new byte[9];

            item[0] = 0x02;
            item[1] = 0x00;
            item[2] = 0xFF;
            item[3] = 0xFF;
            item[4] = 0xFF;
            item[5] = 0xFF;
            item[6] = 0x11;
            item[7] = 0x0F;
            item[8] = 0x1C;

            while (!_shouldStop )
            {

                foreach (DataRow row in mCommTable.Rows)
                {
                    mComport.Write(item, 0, item.Length);

                    memoEdit1.SafeInvoke(d => d.Text += (DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss [SEND] ") + DK1Util.ByteArrayToHexString(item) + "\r\n"));
                    memoEdit1.SafeInvoke(d => d.ScrollToCaret());

                    System.Threading.Thread.Sleep(timer);

                }

            }
        }


        /// <summary>
        /// 통신 시작
        /// </summary>
        /// <returns></returns>
        public bool Run()
        {

            if( comm == null )
                comm = new DK1Interface(mDataTable, mCommTable, mErrorTable, 1000);


            return true;




            // COM 포트가 이미 열려 있다면 
            if (mComport.IsOpen)
            {
                // 모드버스 통신 스레드가 시작되지 않았다면 시작
                if (!workerThread.IsAlive)
                {
                    workerThread.Start(this.mInterval);
                    workerThread.IsBackground = true;
                }

                //// UI 업데이트 타이머 시작되지 않았다면 시작
                //if (!timer1.Enabled)
                //    timer1.Enabled = true;

                return false;
            }



            try
            {

                // COM 포트 설정
                mComport.BaudRate = 9600;
                mComport.DataBits = 8;
                mComport.Parity = Parity.None;
                mComport.StopBits = StopBits.One;
                mComport.PortName = mCom;


                // COM 포트 열기
                mComport.Open();


                //// UI 업데이트 타이머 시작
                ////timer1.Start();
                //// UI 업데이트 타이머 시작되지 않았다면 시작
                //if (!timer1.Enabled)
                //    timer1.Enabled = true;


                // 모드버스 인터페이스 스레드
                workerThread = new Thread(new ParameterizedThreadStart(DoWork));

                RequestStart();
                if (!workerThread.IsAlive)
                {
                    workerThread.Start(this.mInterval);
                    workerThread.IsBackground = true;
                }


            }
            catch (Exception e)
            {
                //MetroMessageBox.Show(mParent, e.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        /// <summary>
        /// 통신 종료
        /// </summary>
        public void Stop()
        {
            comm.Dispose();
            GC.SuppressFinalize(comm);

        }
        #endregion

    }
}
