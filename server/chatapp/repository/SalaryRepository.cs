using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chatapp.repository
{
    public class SalaryRepository
    {
        public int MaxSalary()
        {
            int maxSalary = 0;
            using (var cmd = new System.Data.SqlClient.SqlCommand("select max(base_salary) from salary", chatapp.context.Database.GetConnection()))
            {
                var result = cmd.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    maxSalary = Convert.ToInt32(result);
                }
            }
            return maxSalary;
        }

        public int MinSalary()
        {
            int minSalary = 0;
            using (var cmd = new System.Data.SqlClient.SqlCommand("select min(base_salary) from Salary", chatapp.context.Database.GetConnection()))
            {
                var result = cmd.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    minSalary = Convert.ToInt32(result);
                }
            }
            return minSalary;
        }

        public int AverageSalary()
        {
            int averageSalary = 0;
            using (var cmd = new System.Data.SqlClient.SqlCommand("select avg(base_salary) from salary", chatapp.context.Database.GetConnection()))
            {
                var result = cmd.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    averageSalary = Convert.ToInt32(result);
                }
            }
            return averageSalary;
        }
    }
}
