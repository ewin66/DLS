using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace DevExpress.ProductsDemo.Win.Common
{
    class MySqlManage
    {
        MySqlConnection connection;
        public MySqlConnection Connection { get { return connection; } }
        public MySqlManage()
        {
            string MyConString = "SERVER=localhost; DATABASE=AMR; UID=root; PASSWORD=root;";

            connection = new MySqlConnection(MyConString);


            connection.Open();
  
        }

        public DataSet SelectMariaDBTable(MySqlConnection conn, string query)
        {
            MySqlDataReader Reader;
            MySqlCommand command = conn.CreateCommand();
            command.CommandText = query;
            
            Reader = command.ExecuteReader();

            DataSet ds = new DataSet();
            DataTable dataTable = new DataTable();
            ds.Tables.Add(dataTable);
            ds.EnforceConstraints = false;
            dataTable.Load(Reader);
            Reader.Close();
           
            return ds;
        }



        public bool loginCheck(MySqlConnection conn, string query, string id, string pw)
        {
            string userid, userpw;
            
            MySqlDataReader Reader;
            MySqlCommand command = conn.CreateCommand();
            command.CommandText = query;
            Reader = command.ExecuteReader();

            DataSet ds = new DataSet();
            DataTable dataTable = new DataTable();
            ds.Tables.Add(dataTable);
            ds.EnforceConstraints = false;
            dataTable.Load(Reader);
            Reader.Close();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                userid = row["MST06AID"].ToString();
                userpw = row["MST06PWD"].ToString();

                if (userid == id && userpw == pw)
                    return true;
            }

            return false;
        }

        public void InsertLoginHistory(MySqlConnection conn, string query, string userid)
        {
            MySqlDataReader Reader;
            MySqlCommand command = conn.CreateCommand();
            command.CommandText = query;
            Reader = command.ExecuteReader();

    
        }
    }
}
