using chatapp.model;
using ClosedXML.Excel;
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

        public List<Attendance> GetAllAttendance()
        {
            List<Attendance> attendanceList = new List<Attendance>();
            using (var cmd = new System.Data.SqlClient.SqlCommand("select top 30 * from attendance order by date desc", chatapp.context.Database.GetConnection()))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Attendance attendance = new Attendance
                        {
                            // Assuming UCAttendance has properties that match the database columns
                            // e.g. Id = reader.GetInt32(0), Name = reader.GetString(1), etc.
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            UserId = reader.GetInt32(reader.GetOrdinal("User_Id")),
                            Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                            CheckIn = reader.GetTimeSpan(reader.GetOrdinal("Check_In")),
                            CheckOut = reader.GetTimeSpan(reader.GetOrdinal("Check_Out"))
                        };
                        attendanceList.Add(attendance);
                    }
                }
            }
            return attendanceList;
        }
        
        public int CountAttendanceToday()
        {
            int count = 0;
            using (var cmd = new System.Data.SqlClient.SqlCommand("select count(*) from attendance where Date = @Date", chatapp.context.Database.GetConnection()))
            {
                cmd.Parameters.AddWithValue("@Date", DateTime.Now.Date);
                var result = cmd.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    count = Convert.ToInt32(result);
                }
            }
            return count;
        }

        public bool AddAttendance(Attendance attendance)
        {
            using (var cmd = new System.Data.SqlClient.SqlCommand("INSERT INTO attendance (User_Id, Date, Check_In, Check_Out) VALUES (@UserId, @Date, @CheckIn, @CheckOut)", chatapp.context.Database.GetConnection()))
            {
                cmd.Parameters.AddWithValue("@UserId", attendance.UserId);
                cmd.Parameters.AddWithValue("@Date", attendance.Date);
                cmd.Parameters.AddWithValue("@CheckIn", attendance.CheckIn);
                cmd.Parameters.AddWithValue("@CheckOut", attendance.CheckOut);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool DeleteAttendance(int id)
        {
            using (var cmd = new System.Data.SqlClient.SqlCommand("DELETE FROM attendance WHERE Id = @Id", chatapp.context.Database.GetConnection()))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0; // Returns true if a row was deleted
            }
        }

        public bool UpdateAttendance(Attendance attendance)
        {
            using (var cmd = new System.Data.SqlClient.SqlCommand("UPDATE attendance SET User_Id = @UserId, Date = @Date, Check_In = @CheckIn, Check_Out = @CheckOut WHERE Id = @Id", chatapp.context.Database.GetConnection()))
            {
                cmd.Parameters.AddWithValue("@Id", attendance.Id);
                cmd.Parameters.AddWithValue("@UserId", attendance.UserId);
                cmd.Parameters.AddWithValue("@Date", attendance.Date);
                cmd.Parameters.AddWithValue("@CheckIn", attendance.CheckIn);
                cmd.Parameters.AddWithValue("@CheckOut", attendance.CheckOut);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public Attendance GetAttendanceById(int id)
        {
            Attendance attendance = null;
            using (var cmd = new System.Data.SqlClient.SqlCommand("SELECT * FROM attendance WHERE Id = @Id", chatapp.context.Database.GetConnection()))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        attendance = new Attendance
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            UserId = reader.GetInt32(reader.GetOrdinal("User_Id")),
                            Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                            CheckIn = reader.GetTimeSpan(reader.GetOrdinal("Check_In")),
                            CheckOut = reader.GetTimeSpan(reader.GetOrdinal("Check_Out"))
                        };
                    }
                }
            }
            return attendance;
        }

        public List<Attendance> GetAttendanceByUserId(int userId)
        {
            List<Attendance> attendanceList = new List<Attendance>();
            using (var cmd = new System.Data.SqlClient.SqlCommand("SELECT * FROM attendance WHERE User_Id = @UserId", chatapp.context.Database.GetConnection()))
            {
                cmd.Parameters.AddWithValue("@UserId", userId);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Attendance attendance = new Attendance
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            UserId = reader.GetInt32(reader.GetOrdinal("User_Id")),
                            Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                            CheckIn = reader.GetTimeSpan(reader.GetOrdinal("Check_In")),
                            CheckOut = reader.GetTimeSpan(reader.GetOrdinal("Check_Out"))
                        };
                        attendanceList.Add(attendance);
                    }
                }
            }
            return attendanceList;
        }

        public void ExportToExcel(string filePath, List<Attendance> data)
        {
            var wb = new XLWorkbook();
            var ws = wb.Worksheets.Add("Attendance");

            //Header
            ws.Cell(1,1).Value = "Mã chấm công";
            ws.Cell(1,2).Value = "Mã nhân viên";
            ws.Cell(1, 3).Value = "Ngày chấm công";
            ws.Cell(1, 4).Value = "Giờ vào";
            ws.Cell(1, 5).Value = "Giờ ra";

            int row = 2;
            foreach (var attendance in data)
            {
                ws.Cell(row, 1).Value = attendance.Id;
                ws.Cell(row, 2).Value = attendance.UserId;
                ws.Cell(row, 3).Value = attendance.Date.ToString("dd/MM/yyyy");
                ws.Cell(row, 4).Value = attendance.CheckIn.ToString(@"hh\:mm\:ss");
                ws.Cell(row, 5).Value = attendance.CheckOut.ToString(@"hh\:mm\:ss");
                row++;
            }

            ws.Columns().AdjustToContents(); // Auto adjust column width
            ws.Range("A1:E1").Style.Font.Bold = true; // Make header bold   
            ws.Range("A1:E1").Style.Fill.BackgroundColor = XLColor.LightGray; // Set header background color    

            wb.SaveAs(filePath); // Save the workbook to the specified file path
        }

        public List<Attendance> SearchAttendance(string searchTerm)
        {
            List<Attendance> attendanceList = new List<Attendance>();
            using (var cmd = new System.Data.SqlClient.SqlCommand("SELECT * FROM attendance WHERE User_Id LIKE @SearchTerm OR Date LIKE @SearchTerm", chatapp.context.Database.GetConnection()))
            {
                cmd.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Attendance attendance = new Attendance
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            UserId = reader.GetInt32(reader.GetOrdinal("User_Id")),
                            Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                            CheckIn = reader.GetTimeSpan(reader.GetOrdinal("Check_In")),
                            CheckOut = reader.GetTimeSpan(reader.GetOrdinal("Check_Out"))
                        };
                        attendanceList.Add(attendance);
                    }
                }
            }
            return attendanceList;
        }

        public List<Attendance> GetAttendanceByDate(DateTime date)
        {
            List<Attendance> attendanceList = new List<Attendance>();
            using (var cmd = new System.Data.SqlClient.SqlCommand("SELECT * FROM attendance WHERE Date = @Date", chatapp.context.Database.GetConnection()))
            {
                cmd.Parameters.AddWithValue("@Date", date.Date);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Attendance attendance = new Attendance
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            UserId = reader.GetInt32(reader.GetOrdinal("User_Id")),
                            Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                            CheckIn = reader.GetTimeSpan(reader.GetOrdinal("Check_In")),
                            CheckOut = reader.GetTimeSpan(reader.GetOrdinal("Check_Out"))
                        };
                        attendanceList.Add(attendance);
                    }
                }
            }
            return attendanceList;
        }

        public List<Attendance> GetAllAttendanceOlderThan(int page)
        {
            List<Attendance> attendanceList = new List<Attendance>();
            using (var cmd = new System.Data.SqlClient.SqlCommand("SELECT * FROM attendance ORDER BY Date DESC OFFSET @Offset ROWS FETCH NEXT 30 ROWS ONLY", chatapp.context.Database.GetConnection()))
            {
                cmd.Parameters.AddWithValue("@Date", DateTime.Now.Date);
                cmd.Parameters.AddWithValue("@Offset", (page) * 30);
                using (var reader = cmd.ExecuteReader())
                {
                    try
                    {
                        while (reader.Read())
                        {
                            Attendance attendance = new Attendance
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                UserId = reader.GetInt32(reader.GetOrdinal("User_Id")),
                                Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                                CheckIn = reader.GetTimeSpan(reader.GetOrdinal("Check_In")),
                                CheckOut = reader.GetTimeSpan(reader.GetOrdinal("Check_Out"))
                            };
                            attendanceList.Add(attendance);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error fetching attendance data: " + ex.Message);
                    }
                }
            }
            return attendanceList;
        }
    }
}
