using chatapp.context;
using chatapp.model;
using chatapp.util;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace chatapp.repository
{
    internal class UserRepository
    {
        private SqlConnection connection;
        public UserRepository() 
        {
            this.connection = Database.GetConnection();
        }

        public List<User> GetAllUser ()
        {
            // xử lý db lấy toàn bộ user làm gì gì đó
            List<User> users = new List<User>();

            using (SqlCommand cmd = new SqlCommand("select * from users", connection))
            {
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    User user = SqlUtils<User>.SqlReaderToEntity(reader);
                    users.Add(user);
                }
                reader.Close();
            }
            return users;
        }


        /// <summary>
        /// thêm 1 user -> database
        /// </summary>
        /// <param Name="User"></param>
        /// <returns>số lượng user được thêm</returns>
        public int Insert(User User)
        {
            //using (SqlCommand cmd = new SqlCommand($"insert into {clientdatabase}(username,password,Status,IP) values (@username,@password,@Status,@IP)", connection))
            //{
            //    cmd.Parameters.AddWithValue("@username", User.username);
            //    cmd.Parameters.AddWithValue("@password", User.password);
            //    cmd.Parameters.AddWithValue("@Status", User.Status);
            //    cmd.Parameters.AddWithValue("IP", User.IP);
            //    return cmd.ExecuteNonQuery();
            //}
            return 1;
        }
        /// <summary>
        /// xoá 1 user sử dụng username
        /// </summary>
        /// <param Name="username">tên user cần xoá</param>
        /// <returns>số lượng user được xoá</returns>
        public int Delete(string username)
        {
            //using (SqlCommand cmd = new SqlCommand($"delete from {clientdatabase} where user=@user"))
            //{
            //    cmd.Parameters.AddWithValue("@user", username);
            //    return cmd.ExecuteNonQuery();
            //}

            return 1;
        }
        /// <summary>
        /// đổi trạng thái 1 user
        /// </summary>
        /// <param Name="username">tên</param>
        /// <param Name="state">trạng thái muốn đổi sang</param>
        /// <returns>số lượng user được đổi</returns>
        public void SetStatus(string username, bool status)
        {
            using (SqlCommand cmd = new SqlCommand($"update users set Status=@Status where username=@username",connection))
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
            using (SqlCommand cmd = new SqlCommand($"update users set Status=0",connection))
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
            using (SqlCommand cmd = new SqlCommand($"update users set IP='empty'",connection))
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
        public int SetRealTimeIP(string username,string ip)
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
    }
}
