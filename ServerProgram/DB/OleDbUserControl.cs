using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Text;
using System.Windows.Forms;

namespace DevExpress.ProductsDemo.Win.DB
{
    class OleDbUserControl
    {
        OleDbConnection connection;
        public OleDbConnection Connection { get { return connection; } }

        public OleDbUserControl()
        {

            string DBFileName = string.Empty;
            string connectionString = string.Empty;
            
            DBFileName = DevExpress.Utils.FilesHelper.FindingFileName(Application.StartupPath, "demo.mdb");
            if (DBFileName != string.Empty)
            {
                connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + DBFileName;
            }

            connection = new OleDbConnection(connectionString);
            connection.Open();

        }

        public DataSet SelectOleDbTable(OleDbConnection conn, string query)
        {
            
            OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
            DataSet ds = new DataSet();
            oleDbDataAdapter.SelectCommand = new OleDbCommand(query, conn);
            oleDbDataAdapter.Fill(ds);
            oleDbDataAdapter.Dispose();

            return ds;
        }

        public bool loginCheck(string id, string pw)
        {
            string userid, userpw;
            string tblGrid = "UserInfo";
            OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
            DataSet ds = new DataSet();
            oleDbDataAdapter.SelectCommand = new OleDbCommand("SELECT UserID, Grade, UserName, Phone, Password FROM " + tblGrid, connection);
            oleDbDataAdapter.Fill(ds, tblGrid);
            oleDbDataAdapter.Dispose();

            foreach (DataRow row in ds.Tables[tblGrid].Rows)
            {
                userid = row["UserID"].ToString();
                userpw = row["Password"].ToString();

                if (userid == id && userpw == pw)
                    return true;
            }

            return false;
        }

        public void InsertLoginHistory(string userid, string action)
        {
            OleDbDataAdapter oledbAdapter = new OleDbDataAdapter();

            string sql = string.Format("insert into LoginHistory values('{0}', '{1}', '{2}')", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), userid, action);

            oledbAdapter.InsertCommand = new OleDbCommand(sql, connection);
            oledbAdapter.InsertCommand.ExecuteNonQuery();
        }
    }
}
