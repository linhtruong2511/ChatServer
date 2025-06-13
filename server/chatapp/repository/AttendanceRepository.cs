using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chatapp.repository
{
    public class AttendanceRepository
    {
        public int CountAttendance()
        {
            int count = 0;
            using (var cmd = new System.Data.SqlClient.SqlCommand("select count(*) from attendance", chatapp.context.Database.GetConnection()))
            {
                var result = cmd.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    count = Convert.ToInt32(result);
                }
            }
            return count;
        }
    }
}
