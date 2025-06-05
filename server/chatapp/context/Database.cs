using System.Data.SqlClient;

namespace chatapp.context
{
    internal class Database
    {
        string connectionString = "Server=VUONGQUAN;Database=ChatApp;Trusted_Connection=True;TrustServerCertificate=True;";
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
