using chatapp.context;
using chatapp.model;
using chatapp.util;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Security.Permissions;

namespace chatapp.repository
{
    internal class UserRepository
    {
        private SqlConnection connection;
        public UserRepository()
        {
            this.connection = Database.GetConnection();
        }

        public List<User> GetAllUser()
        {
            // xử lý db lấy toàn bộ user làm gì gì đó
            List<User> users = new List<User>();

            using (SqlCommand cmd = new SqlCommand("select * from users", connection))
            {
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    User userEntity = new User();
                    userEntity = ConvertUtils.ConvertReaderToUser(reader);
                    users.Add(userEntity);
                }
                reader.Close();
            }
            return users;
        }


        public void SetStatus(string username, bool status)
        {
            using (SqlCommand cmd = new SqlCommand($"update users set Status=@Status where username=@username", connection))
            {
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.ExecuteNonQuery();
            }
        }
        /// <summary>
        /// lấy tất cả user trong database
        /// </summary>
        /// <returns>list các user</returns>
        /// <summary>
        /// chuyển trạng thái của tất cả user thành 0 ->reset trạng thái
        /// </summary>
        /// <returns>số lượng user bị chuyển</returns>
        public int SetAllUserStatusToFalse()
        {
            using (SqlCommand cmd = new SqlCommand($"update users set Status=0", connection))
            {
                return cmd.ExecuteNonQuery();
            }
        }
        /// <summary>
        /// chuyển ip của tất cả user thành empty ->reset ip
        /// </summary>
        /// <returns>số lượng user bị chuyển</returns>
        public int SetAllUserIPToEmpty()
        {
            using (SqlCommand cmd = new SqlCommand($"update users set IP='empty'", connection))
            {
                return cmd.ExecuteNonQuery();
            }
        }
        /// <summary>
        /// chuyển IP của username sang giá trị ip
        /// </summary>
        /// <param Name="username"></param>
        /// <param Name="ip"></param>
        /// <returns>số user bị chuyển</returns>
        public int SetRealTimeIP(string username, string ip)
        {
            //using (SqlCommand cmd = new SqlCommand($"update {clientdatabase} set IP=@IP where username=@username"))
            //{
            //    cmd.Parameters.AddWithValue("@IP", ip);
            //    cmd.Parameters.AddWithValue("@username", username);
            //    return cmd.ExecuteNonQuery();
            //}

            return 1;
        }

        public bool CheckPasswordUser(string username, string password)
        {
            using (SqlCommand cmd = new SqlCommand("select ID from users where username=@username and password=@password", connection))
            {
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        public int CountUser()
        {
            using (SqlCommand cmd = new SqlCommand("select count(*) from users", connection))
            {
                return (int)cmd.ExecuteScalar();
            }
        }

        public void AddUser(User user)
        {
            using (SqlCommand cmd = new SqlCommand(
                "insert into users " +
                "(Name, Username, Password, address, phone, Position_Id, Department_Id) " +
                "values " +
                "(@Name, @Username, @Password, @address, @phone, @PositionId, @DepartmentId)",
                connection))
            {
                cmd.Parameters.AddWithValue("@Name", user.Name);
                cmd.Parameters.AddWithValue("@Username", user.Username);
                cmd.Parameters.AddWithValue("@Password", user.Password);
                cmd.Parameters.AddWithValue("@address", user.Address);
                cmd.Parameters.AddWithValue("@phone", user.Phone);
                cmd.Parameters.AddWithValue("@PositionId", user.PositionId);
                cmd.Parameters.AddWithValue("@DepartmentId", user.DepartmentId);
                cmd.ExecuteNonQuery();
            }
        }

        public void deleteUser(int userId)
        {
            using (SqlCommand cmd = new SqlCommand("delete from users where ID=@ID", connection))
            {
                cmd.Parameters.AddWithValue("@ID", userId);
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateUser(User user)
        {
            using (SqlCommand cmd = new SqlCommand(
                "update users set Name=@Name, Username=@Username, Password=@Password, address=@address, phone=@phone, PositionId=@PositionId, DepartmentId=@DepartmentId where ID=@ID",
                connection))
            {
                cmd.Parameters.AddWithValue("@ID", user.ID);
                cmd.Parameters.AddWithValue("@Name", user.Name);
                cmd.Parameters.AddWithValue("@Username", user.Username);
                cmd.Parameters.AddWithValue("@Password", user.Password);
                cmd.Parameters.AddWithValue("@address", user.Address);
                cmd.Parameters.AddWithValue("@phone", user.Phone);
                cmd.Parameters.AddWithValue("@PositionId", user.PositionId);
                cmd.Parameters.AddWithValue("@DepartmentId", user.DepartmentId);
                cmd.ExecuteNonQuery();
            }
        }

        public User GetUserByUsername(string username)
        {
            using (SqlCommand cmd = new SqlCommand("select * from users where username=@username", connection))
            {
                cmd.Parameters.AddWithValue("@username", username);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        User userEntity = new User();
                        userEntity = ConvertUtils.ConvertReaderToUser(reader);
                        return userEntity;
                    }
                    else
                    {
                        return null; // Không tìm thấy user
                    }
                }
            }
        }

        public User GetUserById(int id)
        {
            using (SqlCommand cmd = new SqlCommand("select * from users where ID=@ID", connection))
            {
                cmd.Parameters.AddWithValue("@ID", id);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return ConvertUtils.ConvertReaderToUser(reader);
                    }
                    else
                    {
                        return null; // Không tìm thấy user
                    }
                }
            }
        }

        public User getUserByDepartmentID(int departmentId)
        {
            using (SqlCommand cmd = new SqlCommand("select * from users where DepartmentId=@DepartmentId", connection))
            {
                cmd.Parameters.AddWithValue("@DepartmentId", departmentId);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return ConvertUtils.ConvertReaderToUser(reader);
                    }
                    else
                    {
                        return null; // Không tìm thấy user
                    }
                }
            }
        }

        public User getUserByPositionID(int positionId)
        {
            using (SqlCommand cmd = new SqlCommand("select * from users where PositionId=@PositionId", connection))
            {
                cmd.Parameters.AddWithValue("@PositionId", positionId);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return ConvertUtils.ConvertReaderToUser(reader);
                    }
                    else
                    {
                        return null; // Không tìm thấy user
                    }
                }
            }
        }

        public User SearchUserByName(string name)
        {
            using (SqlCommand cmd = new SqlCommand("select * from users where Name like @Name", connection))
            {
                cmd.Parameters.AddWithValue("@Name", "%" + name + "%");
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return SqlUtils<User>.SqlReaderToEntity(reader);
                    }
                    else
                    {
                        return null; // Không tìm thấy user
                    }
                }
            }
        }

        public List<User> SearchUserByFullInformation(User user)
        {
            List<string> conditions = new List<string>();
            List<SqlParameter> parameters = new List<SqlParameter>();

            if (!string.IsNullOrEmpty(user.Name))
            {
                conditions.Add("Name = @Name");
                parameters.Add(new SqlParameter("@Name", user.Name));
            }

            if (!string.IsNullOrEmpty(user.Username))
            {
                conditions.Add("Username = @Username");
                parameters.Add(new SqlParameter("@Username", user.Username));
            }

            if (!string.IsNullOrEmpty(user.Password))
            {
                conditions.Add("Password = @Password");
                parameters.Add(new SqlParameter("@Password", user.Password));
            }

            if (!string.IsNullOrEmpty(user.Address))
            {
                conditions.Add("Address = @Address");
                parameters.Add(new SqlParameter("@Address", user.Address));
            }

            if (!string.IsNullOrEmpty(user.Phone))
            {
                conditions.Add("Phone = @Phone");
                parameters.Add(new SqlParameter("@Phone", user.Phone));
            }

            if (user.PositionId != 0)
            {
                conditions.Add("PositionId = @PositionId");
                parameters.Add(new SqlParameter("@PositionId", user.PositionId));
            }

            if (user.DepartmentId != 0)
            {
                conditions.Add("DepartmentId = @DepartmentId");
                parameters.Add(new SqlParameter("@DepartmentId", user.DepartmentId));
            }

            string whereClause = conditions.Count > 0
                ? "WHERE " + string.Join(" AND ", conditions)
                : "";

            string query = "SELECT * FROM users " + whereClause;
            List<User> users = new List<User>();

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddRange(parameters.ToArray());

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        User userEntity = new User();
                        userEntity.Name = reader["Name"] as string;
                        userEntity.Username = reader["Username"] as string;
                        userEntity.Password = reader["Password"] as string;
                        userEntity.Address = reader["Address"] as string;
                        userEntity.Phone = reader["Phone"] as string;
                        userEntity.DepartmentId = reader["Department_Id"] != DBNull.Value ? (int)reader["Department_Id"] : -1;
                        userEntity.PositionId = reader["Position_Id"] != DBNull.Value ? (int)reader["Position_Id"] : -1;
                        userEntity.ID = reader["ID"] != DBNull.Value ? (int)reader["ID"] : -1;
                        userEntity.CreateAt = reader["CreateAt"] != DBNull.Value ? (DateTime)reader["CreateAt"] : DateTime.MinValue;
                        users.Add(userEntity);
                    }
                    return users;
                }
            }
        }

        public void DeleteUserByUsername(string username)
        {
            using (SqlCommand cmd = new SqlCommand("delete from users where Username=@Username", connection))
            {
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteUserById(int id)
        {
            using (SqlCommand cmd = new SqlCommand("delete from users where ID=@ID", connection))
            {
                cmd.Parameters.AddWithValue("@ID", id);
                cmd.ExecuteNonQuery();
            }
        }

        public List<User> GetAllUsersByDepartmentId(int departmentId)
        {
            List<User> users = new List<User>();
            using (SqlCommand cmd = new SqlCommand("select * from users where Department_Id=@DepartmentId", connection))
            {
                cmd.Parameters.AddWithValue("@DepartmentId", departmentId);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(ConvertUtils.ConvertReaderToUser(reader));
                    }
                }
            }
            return users;
        }

        public List<User> GetAllUsersByPositionId(int positionId)
        {
            List<User> users = new List<User>();
            using (SqlCommand cmd = new SqlCommand("select * from users where Position_Id=@PositionId", connection))
            {
                cmd.Parameters.AddWithValue("@PositionId", positionId);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(ConvertUtils.ConvertReaderToUser(reader));
                    }
                }
            }
            return users;
        }
    }
}
