using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace ServerProgram.DB
{
    class MySqlManage : IDisposable
    {

        public MySqlManage(string connectionString)
        {
            
            connection = new MySqlConnection(connectionString);

            connection.Open();
  
        }
        ~MySqlManage()
        {
            if (!isDispose)
            {
                Dispose();
            }
        }


        private bool isDispose = false;
        MySqlConnection connection;
        public MySqlConnection Connection { get { return connection; } }


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

        public bool InsertMariaDB(MySqlConnection conn, string query)
        {
            MySqlDataReader Reader;
            MySqlCommand command = conn.CreateCommand();
            bool rtrn = false;
            command.CommandText = query;
            try
            {
                Reader = command.ExecuteReader();
                Reader.Close();

                rtrn = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                rtrn = false;
            }
            
            return rtrn;
    
        }


        public void Dispose()
        {
            isDispose = true;
            //... 리소스를 해제함...
            GC.SuppressFinalize(this);
        }


    }
}
