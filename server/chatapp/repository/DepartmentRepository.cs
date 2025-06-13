using chatapp.context;
using chatapp.model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chatapp.repository
{
    public class DepartmentRepository
    {
        public int CountDepartment()
        {
            using (SqlCommand cmd = new SqlCommand("select count(*) from departments", Database.GetConnection()))
            {
                return (int)cmd.ExecuteScalar() != null ? Convert.ToInt32(cmd.ExecuteScalar()) : 0;
            }
        }

        public List<Department> GetDepartments()
        {
            List<Department> departments = new List<Department>();
            using (SqlCommand cmd = new SqlCommand("select * from departments", Database.GetConnection()))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        departments.Add(new Department
                        {
                            Name = reader["name"].ToString(),
                            Id = Convert.ToInt32(reader["id"])
                        });
                    }
                }
            }
            return departments;
        }

        public void AddDepartment(Department department)
        {
            using (SqlCommand cmd = new SqlCommand("insert into departments(name) values(@name)", Database.GetConnection()))
            {
                cmd.Parameters.AddWithValue("@name", department.Name);
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateDepartment(Department department)
        {
            using (SqlCommand cmd = new SqlCommand("update departments set name = @name where id = @id", Database.GetConnection()))
            {
                cmd.Parameters.AddWithValue("@name", department.Name);
                cmd.Parameters.AddWithValue("@id", department.Id);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteDepartment(int id)
        {
            using (SqlCommand cmd = new SqlCommand("delete from departments where id = @id", Database.GetConnection()))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public Department GetDepartmentById(int id)
        {
            Department department = null;
            using (SqlCommand cmd = new SqlCommand("select * from departments where id = @id", Database.GetConnection()))
            {
                cmd.Parameters.AddWithValue("@id", id);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        department = new Department
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Name = reader["name"].ToString()
                        };
                    }
                }
            }
            return department;
        }
    }
}
