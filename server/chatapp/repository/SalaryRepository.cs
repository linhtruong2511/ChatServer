using chatapp.context;
using chatapp.model;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chatapp.repository
{
    public class SalaryRepository
    {
        public void ExportSalariesToExcel(List<Salary> salaries, string filePath)
        {
            var wb = new XLWorkbook();
            var ws = wb.Worksheets.Add("Salary");

            //Header
            ws.Cell(1, 1).Value = "Mã lương";
            ws.Cell(1, 2).Value = "Mã nhân viên";
            ws.Cell(1, 3).Value = "Lương cơ bản";
            ws.Cell(1, 4).Value = "Lương thưởng";
            ws.Cell(1, 5).Value = "Tháng lương";

            int row = 2;
            foreach (var salary in salaries)
            {
                ws.Cell(row, 1).Value = salary.Id;
                ws.Cell(row, 2).Value = salary.UserID;
                ws.Cell(row, 3).Value = salary.Base_Salary;
                ws.Cell(row, 4).Value = salary.Bonus;
                ws.Cell(row, 5).Value = salary.Date.ToString("dd/MM/yyyy");
                row++;
            }

            ws.Columns().AdjustToContents(); // Auto adjust column width
            ws.Range("A1:E1").Style.Font.Bold = true; // Make header bold   
            ws.Range("A1:E1").Style.Fill.BackgroundColor = XLColor.LightGray; // Set header background color    

            wb.SaveAs(filePath); // Save the workbook to the specified file path
        }

        public bool DeleteSalary(int id)
        {
            using (SqlCommand cmd = new SqlCommand("DELETE FROM Salary WHERE id = @id", Database.GetConnection()))
            {
                cmd.Parameters.AddWithValue("@id", id);
                return cmd.ExecuteNonQuery() > 0; // Returns true if a row was deleted
            }
        }

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

        public List<model.Salary> GetAllSalariesInMonth(int month)
        {
            List<model.Salary> salaries = new List<model.Salary>();
            using (var cmd = new System.Data.SqlClient.SqlCommand("SELECT id, user_id, base_salary, bonus, date FROM Salary WHERE MONTH(date) = @month", chatapp.context.Database.GetConnection()))
            {
                cmd.Parameters.AddWithValue("@month", month);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        model.Salary salary = new model.Salary
                        {
                            UserID = reader.GetInt32(0),
                            Id = reader.GetInt32(1),
                            Base_Salary = reader.GetDecimal(2),
                            Bonus = reader.GetDecimal(3),
                            Date = reader.GetDateTime(4) // Assuming the 5th column is the date
                        };
                        salaries.Add(salary);
                    }
                }
            }
            return salaries;
        }

        public List<Salary> getAllSalariesInMonthAndYear(int month, int year)
        {
            List<Salary> salaries = new List<Salary>();
            using (var cmd = new System.Data.SqlClient.SqlCommand("SELECT id, user_id, base_salary, bonus, date FROM Salary WHERE MONTH(date) = @month AND YEAR(date) = @year", chatapp.context.Database.GetConnection()))
            {
                cmd.Parameters.AddWithValue("@month", month);
                cmd.Parameters.AddWithValue("@year", year);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Salary salary = new Salary
                        {
                            Id = reader.GetInt32(0),
                            UserID = reader.GetInt32(1),
                            Base_Salary = reader.GetDecimal(2),
                            Bonus = reader.GetDecimal(3),
                            Date = reader.GetDateTime(4) // Assuming the 5th column is the date
                        };
                        salaries.Add(salary);
                    }
                }
            }
            return salaries;
        }

        public void AddSalary(Salary salary)
        {
            using (SqlCommand cmd = new SqlCommand("insert into salary (user_id, date, base_salary, bonus) values (@userId, @date, @baseSalary, @bonus)", Database.GetConnection()))
            {
                cmd.Parameters.AddWithValue("@userId", salary.UserID);
                cmd.Parameters.AddWithValue("@date", salary.Date);
                cmd.Parameters.AddWithValue("@baseSalary", salary.Base_Salary);
                cmd.Parameters.AddWithValue("@bonus", salary.Bonus);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
