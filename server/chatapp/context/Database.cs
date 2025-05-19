using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chatapp.context
{
    internal class Database
    {
        string connectionString = "Server=192.168.208.78,1433;Database=ChatApp;User Id=linhtruong4;Password=Linhtruong@;Encrypt=True;TrustServerCertificate=True;";
        private static SqlConnection conn;

        public Database()
        {
            conn = new SqlConnection(connectionString);
        }

        public static SqlConnection GetConnection()
        {
            return conn;
        }

        public void OpenConnection()
        {
            if (conn.State == System.Data.ConnectionState.Closed)
            {
                conn.Open();
            }
        }

        public void CloseConnection()
        {
            if (conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
            }
        }   
    }
}
