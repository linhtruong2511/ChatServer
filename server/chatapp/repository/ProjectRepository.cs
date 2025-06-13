using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chatapp.repository
{
    public class ProjectRepository
    {
        public int CountProject()
        {
            using (var cmd = new System.Data.SqlClient.SqlCommand("select count(*) from projects", chatapp.context.Database.GetConnection()))
            {
                return (int)cmd.ExecuteScalar() != null ? Convert.ToInt32(cmd.ExecuteScalar()) : 0;
            }
        }

        public int countFinishedProject()
        {
            using (var cmd = new System.Data.SqlClient.SqlCommand("select count(*) from projects where finished = 1", chatapp.context.Database.GetConnection()))
            {
                return (int)cmd.ExecuteScalar() != null ? Convert.ToInt32(cmd.ExecuteScalar()) : 0;
            }
        }

        public int countUnfinishedProject()
        {
            using (var cmd = new System.Data.SqlClient.SqlCommand("select count(*) from projects where finished = 0", chatapp.context.Database.GetConnection()))
            {
                return (int)cmd.ExecuteScalar() != null ? Convert.ToInt32(cmd.ExecuteScalar()) : 0;
            }
        }
    }
}
