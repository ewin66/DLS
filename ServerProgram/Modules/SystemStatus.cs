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
using DevExpress.ProductsDemo.Win.Serial;
using System.Runtime.InteropServices;

using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;


using System.Collections;

using ServerProgram.DB;

namespace DevExpress.ProductsDemo.Win.Modules
{

    public partial class SystemStatus : BaseModule
    {
 
        public SystemStatus()
        {
            InitializeComponent();


            // 실시간 데이터 저장 테이블
            mCommTable = initCommDataTable();
            mDataTable = initCommDataTable();
            InitGridData();


            timer1.Interval = 1000;
            timer1.Tick += new EventHandler(uiUpdateTimer_Tick);

            mWorkArmTot00 = new WORK_AMR_TOT00();
            mWorkArmTot00.eReady += mWorkArmTot00_eReady;
            mInsertThread = new Thread(new ThreadStart(mWorkArmTot00.DoWork));
            mInsertThread.IsBackground = true;


            //this.serialConnection.Open("COM1", 115200, 8, System.IO.Ports.Parity.None, System.IO.Ports.StopBits.One);
            //this.serialConnection
            //SerialPort sp = new SerialPort("COM1", 115200, System.IO.Ports.Parity.None, 8, System.IO.Ports.StopBits.One);
            //sp.Open();
            //sp.Write("hi1234");
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
                case TagResources.StartStopComStart:
                    Run();

                    if (!mInsertThread.IsAlive)
                    {
                        mInsertThread.Start();
                        mInsertThread.IsBackground = true;
                    }
                    break;
                case TagResources.StartStopComStop:
                    Stop();
                    
                    break;
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

    }


    #region record
    public class RecordCollection : CollectionBase, IBindingList, ITypedList
    {
        public Record this[int i] { get { return (Record)List[i]; } }
        public void Add(Record record)
        {
            int res = List.Add(record);
            record.owner = this;
            record.Index = res;
        }
        public void SetValue(int row, int col, object val)
        {
            this[row].SetValue(col, val);
        }
        internal void OnListChanged(Record rec)
        {
            if (listChangedHandler != null) listChangedHandler(this, new ListChangedEventArgs(ListChangedType.ItemChanged, rec.Index, rec.Index));
        }

        PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] accessors)
        {
            PropertyDescriptorCollection coll = TypeDescriptor.GetProperties(typeof(Record));
            ArrayList list = new ArrayList(coll);
            list.Sort(new PDComparer());
            PropertyDescriptorCollection res = new PropertyDescriptorCollection(null);
            for (int n = 0; n < SystemStatus.ColumnCount; n++)
            {
                res.Add(list[n] as PropertyDescriptor);
            }
            return res;
        }
        class PDComparer : IComparer
        {
            int IComparer.Compare(object a, object b)
            {
                return Comparer.Default.Compare(GetName(a), GetName(b));
            }
            int GetName(object a)
            {
                PropertyDescriptor pd = (PropertyDescriptor)a;
                if (pd.Name.StartsWith("Column")) return Convert.ToInt32(pd.Name.Substring(6));
                return -1;

            }
        }
        string ITypedList.GetListName(PropertyDescriptor[] accessors) { return ""; }
        public object AddNew() { return null; }
        public bool AllowEdit { get { return true; } }
        public bool AllowNew { get { return false; } }
        public bool AllowRemove { get { return false; } }

        private ListChangedEventHandler listChangedHandler;
        public event ListChangedEventHandler ListChanged
        {
            add { listChangedHandler += value; }
            remove { listChangedHandler -= value; }
        }
        public void AddIndex(PropertyDescriptor pd) { throw new NotSupportedException(); }
        public void ApplySort(PropertyDescriptor pd, ListSortDirection dir) { throw new NotSupportedException(); }
        public int Find(PropertyDescriptor property, object key) { throw new NotSupportedException(); }
        public bool IsSorted { get { return false; } }
        public void RemoveIndex(PropertyDescriptor pd) { throw new NotSupportedException(); }
        public void RemoveSort() { throw new NotSupportedException(); }
        public ListSortDirection SortDirection { get { throw new NotSupportedException(); } }
        public PropertyDescriptor SortProperty { get { throw new NotSupportedException(); } }
        public bool SupportsChangeNotification { get { return true; } }
        public bool SupportsSearching { get { return false; } }
        public bool SupportsSorting { get { return false; } }

    }

    public class Record
    {
        internal int Index = -1;
        internal RecordCollection owner;
        string[] values = new string[20];
        public string Column0 { get { return values[0]; } set { SetValue(0, value); } }
        public string Column1 { get { return values[1]; } set { SetValue(1, value); } }
        public string Column2 { get { return values[2]; } set { SetValue(2, value); } }
        public string Column3 { get { return values[3]; } set { SetValue(3, value); } }
        public string Column4 { get { return values[4]; } set { SetValue(4, value); } }
        public string Column5 { get { return values[5]; } set { SetValue(5, value); } }
        public string Column6 { get { return values[6]; } set { SetValue(6, value); } }
        public string Column7 { get { return values[7]; } set { SetValue(7, value); } }
        public string Column8 { get { return values[8]; } set { SetValue(8, value); } }
        public string Column9 { get { return values[9]; } set { SetValue(9, value); } }
        public string Column10 { get { return values[10]; } set { SetValue(10, value); } }
        public string Column11 { get { return values[11]; } set { SetValue(11, value); } }
        public string Column12 { get { return values[12]; } set { SetValue(12, value); } }
        public string Column13 { get { return values[13]; } set { SetValue(13, value); } }
        public string Column14 { get { return values[14]; } set { SetValue(14, value); } }
        public string Column15 { get { return values[15]; } set { SetValue(15, value); } }
        public string Column16 { get { return values[16]; } set { SetValue(16, value); } }
        public string Column17 { get { return values[17]; } set { SetValue(17, value); } }
        public string Column18 { get { return values[18]; } set { SetValue(18, value); } }
        public string Column19 { get { return values[19]; } set { SetValue(19, value); } }
        public string GetValue(int index) { return values[index]; }
        //<label1>
        public void SetValue(int index, object val)
        {
            values[index] = (string)val;
            if (this.owner != null) this.owner.OnListChanged(this);
        }
        //</label1>
    }
    #endregion


}
