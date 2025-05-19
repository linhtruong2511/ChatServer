using chatapp.context;
using chatapp.model;
using chatapp.util;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace chatapp.repository
{
    internal class UserRepository
    {
        private SqlConnection connection;
        public UserRepository() 
        {
            this.connection = Database.GetConnection();
        }

        public List<User> getAllUser ()
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
        /// <param name="User"></param>
        /// <returns>số lượng user được thêm</returns>
        public int Insert(User User)
        {
            //using (SqlCommand cmd = new SqlCommand($"insert into {clientdatabase}(username,password,state,realtimeIP) values (@username,@password,@state,@realtimeIP)", connection))
            //{
            //    cmd.Parameters.AddWithValue("@username", User.username);
            //    cmd.Parameters.AddWithValue("@password", User.password);
            //    cmd.Parameters.AddWithValue("@state", User.state);
            //    cmd.Parameters.AddWithValue("realtimeIP", User.realtimeIP);
            //    return cmd.ExecuteNonQuery();
            //}
            return 1;
        }
        /// <summary>
        /// xoá 1 user sử dụng username
        /// </summary>
        /// <param name="username">tên user cần xoá</param>
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
        /// <param name="username">tên</param>
        /// <param name="state">trạng thái muốn đổi sang</param>
        /// <returns>số lượng user được đổi</returns>
        public int SetState(string username, bool state)
        {
            //using (SqlCommand cmd = new SqlCommand($"update {clientdatabase} set state=@state where username=@username"))
            //{
            //    cmd.Parameters.AddWithValue("@state", state);
            //    cmd.Parameters.AddWithValue("@username", username);
            //    return cmd.ExecuteNonQuery();
            //}

            return 1;
        }
        /// <summary>
        /// lấy tất cả user trong database
        /// </summary>
        /// <returns>list các user</returns>
        //public List<User> GetAllUser()
        //{
        //    List<User> userlist = new List<User>();
        //        using (SqlCommand cmd = new SqlCommand($"select * from {clientdatabase}"))
        //        {
        //            using (SqlDataReader reader = cmd.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    userlist.Add(SqlUtils<User>.SqlReaderToEntity(reader));
        //                }
        //            }
        //        }
        //        return userlist;
        //}
        /// <summary>
        /// chuyển trạng thái của tất cả user thành 0 ->reset trạng thái
        /// </summary>
        /// <returns>số lượng user bị chuyển</returns>
        public int SetAllUserStateToFalse()
        {
            using (SqlCommand cmd = new SqlCommand($"update users set state=0"))
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
            using (SqlCommand cmd = new SqlCommand($"update user set realtimeIP='empty'"))
            {
                return cmd.ExecuteNonQuery();
            }
        }
        /// <summary>
        /// chuyển realtimeIP của username sang giá trị ip
        /// </summary>
        /// <param name="username"></param>
        /// <param name="ip"></param>
        /// <returns>số user bị chuyển</returns>
        public int SetRealTimeIP(string username,string ip)
        {
            //using (SqlCommand cmd = new SqlCommand($"update {clientdatabase} set realtimeIP=@realtimeIP where username=@username"))
            //{
            //    cmd.Parameters.AddWithValue("@realtimeIP", ip);
            //    cmd.Parameters.AddWithValue("@username", username);
            //    return cmd.ExecuteNonQuery();
            //}

            return 1;
        }

        public bool CheckPasswordUser(string username, string password)
        {
            using (SqlCommand cmd = new SqlCommand("select id from users where username=@username and password=@password", connection))
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
