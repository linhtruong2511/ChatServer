using chatapp.context;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chatapp.repository
{
    public class TaskRepository
    {
        public int CountTask()
        {
            using (SqlCommand cmd = new SqlCommand("select count(*) from task", Database.GetConnection()))
            {
                return (int) cmd.ExecuteScalar();
            }
        }
    }
}
