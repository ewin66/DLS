using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using ServerProgram.DB;
using DevExpress.MailClient.Win;
using DevExpress.ProductsDemo.Win.Item;

using ServerProgram.Serial;
namespace ServerProgram
{
    public class fmMainPresenter : IfmMainPresenter
    {
        public IfmMain CurrentForm { get; set; }

        /// <summary>
        /// 생성자
        /// </summary>
        public fmMainPresenter(IfmMain view)
        {
            CurrentForm = view;
            CommandCenter.ReadingChanged.Executed += new EventHandler<ExecutedEventArgs>(ReadingChanged);
        }

        private DK1Interface comm;
        DataTable mCommTable = new DataTable();
        DataTable mDataTable = new DataTable();

        void ReadingChanged(object sender, ExecutedEventArgs e)
        {
            if (e.Parameter.ToString().Equals("ReadingStart"))
                ReadingStart();
            else
                ReadingStop();

            CurrentForm.ShowMessage(e.Parameter.ToString());
        }

        // 시리얼 통신 시작
        void ReadingStart()
        {
            if (comm == null || !comm.actived)
            {
                comm = new DK1Interface();
                comm.eDataReceive += new EventHandler<DK1EventArgs>(comm_eDataReceive);
            }
        }

        // 시리얼 통신 종료
        void ReadingStop()
        {
            if (comm != null || comm.actived)
            {
                comm.eDataReceive -= new EventHandler<DK1EventArgs>(comm_eDataReceive);
                comm.Dispose();
            }
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

                //CurrentForm.
            }
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
            
            MySqlManage crud = new MySqlManage(ConfigurationManager.ConnectionStrings["MySQL"].ConnectionString);

            string query = "select MST04SNO, MST04DON, MST04HNO from amr_mst04";
            DataSet ds = new DataSet();
            ds = crud.SelectMariaDBTable(crud.Connection, query);

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                mDataTable.Rows.Add(row[0], DateTime.Now.ToString("yyyy-MM-dd hh"), row[1], row[2], "null", "null", "null", "null", "null", "null", "null");
                mCommTable.Rows.Add(row[0], DateTime.Now.ToString("yyyy-MM-dd hh"), row[1], row[2], "null", "null", "null", "null", "null", "null", "null");
            }

        }
    }
}
