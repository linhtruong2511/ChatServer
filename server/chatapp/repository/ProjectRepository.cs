using chatapp.context;
using chatapp.model;
using chatapp.util;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chatapp.repository
{
    public class ProjectRepository
    {
        public void RemoveUserInProject(int userId)
        {
            using(SqlCommand cmd  = new SqlCommand("delete user_project where user_id = @userId", Database.GetConnection()))
            {
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.ExecuteNonQuery();
            }
        }

        public void AddUserToProject(User user, int projectId, string role)
        {
            using (SqlCommand cmd = new SqlCommand("insert into user_project (user_id, project_id, role) values (@userId, @projectId, @role)", Database.GetConnection()))
            {
                cmd.Parameters.AddWithValue("@userId", user.ID);
                cmd.Parameters.AddWithValue("@projectId", projectId);
                cmd.Parameters.AddWithValue("@role", role);
                cmd.ExecuteNonQuery();
            }
        } 
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

        public List<Project> GetAllProject()
        {
            using (SqlCommand cmd = new SqlCommand("select * from projects", Database.GetConnection()))
            {
                List<Project> projects = new List<Project>();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Project project = new Project()
                    {
                        Id = (int)reader["Id"],
                        Name = reader["Name"].ToString(),
                        CreateAt = Convert.ToDateTime(reader["createAt"].ToString()),
                        DepartmentId = (int)reader["Department_id"],
                        Description = reader["Description"].ToString(),
                        Finished = (bool)reader["finished"]
                    };
                    projects.Add(project);
                }
                reader.Close();
                return projects;
            }
        }

        public User getUserChargeByProjectId(int projectId)
        {
            using (SqlCommand cmd = new SqlCommand("select * from user_project where project_id = @projectId", Database.GetConnection()))
            {
                cmd.Parameters.AddWithValue("@projectId", projectId);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    int userID = (int)reader["user_id"];
                    User user = new UserRepository().GetUserById(userID);
                    return user;
                }
                return null;
            }
        }

        public int countUserJoinProject(int projectId)
        {
            using (SqlCommand cmd = new SqlCommand("select count(*) from user_project where project_id = @projectId", Database.GetConnection()))
            {
                cmd.Parameters.AddWithValue("@projectId", projectId);
                return (int)cmd.ExecuteScalar() != null ? Convert.ToInt32(cmd.ExecuteScalar()) : 0;
            }
        }

        public List<UserProject> GetUsersInProject(int projectId)
        {
            using (SqlCommand cmd = new SqlCommand("select * from user_project where project_id = @projectId", Database.GetConnection()))
            {
                cmd.Parameters.AddWithValue("@projectId", projectId);
                SqlDataReader reader = cmd.ExecuteReader();
                Dictionary<int, string> userIdRole = new Dictionary<int, string>();
                List<UserProject> userProjects = new List<UserProject>();
                while (reader.Read())
                {
                    int userID = (int)reader["user_id"];
                    string role = reader["role"].ToString();
                    userIdRole[userID] = role;
                }
                reader.Close();
                foreach (int userId in userIdRole.Keys)
                {
                    User user = new UserRepository().GetUserById(userId);
                    if (user != null)
                    {
                        userProjects.Add(new UserProject()
                        {
                            Id = userId,
                            Name = user.Name,
                            Phone = user.Phone,
                            Address = user.Address,
                            Role = userIdRole[userId]
                        });
                    }
                }
                return userProjects;
            }
        }

        public bool MarkProjectAsFinished(int projectId)
        {
            using (SqlCommand cmd = new SqlCommand("update projects set finished = 1 where Id = @projectId", Database.GetConnection()))
            {
                cmd.Parameters.AddWithValue("@projectId", projectId);
                cmd.ExecuteNonQuery();
                return true;
            }
        }

        public bool MarkProjectAsUnfinished(int projectId)
        {
            using (SqlCommand cmd = new SqlCommand("update projects set finished = 0 where Id = @projectId", Database.GetConnection()))
            {
                cmd.Parameters.AddWithValue("@projectId", projectId);
                cmd.ExecuteNonQuery();
                return true;
            }
        }

        public bool AddProject(Project project)
        {
            using (SqlCommand cmd = new SqlCommand("insert into projects (Name, Description, Department_id, CreateAt, Finished) values (@Name, @Description, @DepartmentId, @CreateAt, @Finished)", Database.GetConnection()))
            {
                cmd.Parameters.AddWithValue("@Name", project.Name);
                cmd.Parameters.AddWithValue("@Description", project.Description);
                cmd.Parameters.AddWithValue("@DepartmentId", project.DepartmentId);
                cmd.Parameters.AddWithValue("@CreateAt", project.CreateAt);
                cmd.Parameters.AddWithValue("@Finished", project.Finished);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public List<Project> GetAndSortByName()
        {
            using (SqlCommand cmd = new SqlCommand("select * from projects order by Name", Database.GetConnection()))
            {
                List<Project> projects = new List<Project>();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Project project = new Project()
                    {
                        Id = (int)reader["Id"],
                        Name = reader["Name"].ToString(),
                        CreateAt = Convert.ToDateTime(reader["createAt"].ToString()),
                        DepartmentId = (int)reader["Department_id"],
                        Description = reader["Description"].ToString(),
                        Finished = (bool)reader["finished"]
                    };
                    projects.Add(project);
                }
                reader.Close();
                return projects;
            }
        }

        public List<Project> GetAndSortByDepartmentId()
        {
            using (SqlCommand cmd = new SqlCommand("select * from projects order by Department_id", Database.GetConnection()))
            {
                List<Project> projects = new List<Project>();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Project project = new Project()
                    {
                        Id = (int)reader["Id"],
                        Name = reader["Name"].ToString(),
                        CreateAt = Convert.ToDateTime(reader["createAt"].ToString()),
                        DepartmentId = (int)reader["Department_id"],
                        Description = reader["Description"].ToString(),
                        Finished = (bool)reader["finished"]
                    };
                    projects.Add(project);
                }
                reader.Close();
                return projects;
            }
        }
    }
}
