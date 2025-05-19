using chatapp.context;
using chatapp.model;
using chatapp.util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace chatapp.repository
{
    public class MessageRepository
    {
        SqlConnection conn;
        public MessageRepository()
        {
            conn = Database.GetConnection();
        }

        //public List<Message> GetAllMessages(int source, int destination) 
        //{
        //    string query = $"select * from message where source=@source and destination=@destination";
        //    using (SqlCommand cmd = new SqlCommand(query, conn))
        //    {
        //        cmd.Parameters.AddWithValue("@source", source);
        //        cmd.Parameters.AddWithValue("@destination", destination);
        //        SqlDataReader reader = cmd.ExecuteReader();
        //        List<Message> messages = new List<Message>();
        //        while (reader.Read())
        //        {
        //            Message message = SqlUtils<Message>.SqlReaderToEntity(reader);
        //            messages.Add(message);
        //        }
        //        reader.Close();
        //        return messages;
        //    }
        //}

        public void SaveMessage (int source, int destination, string contents, int status = 1)
        {
            DateTime dateTime = DateTime.Now;
            string query = $"insert into message (source, destination, contents, status, createAt) values (@source, @destination, @contents, @status, @createAt)";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@source", source);
                cmd.Parameters.AddWithValue("@destination", destination);
                cmd.Parameters.AddWithValue("@contents", contents);
                cmd.Parameters.AddWithValue("@status", status);
                cmd.Parameters.AddWithValue("@createAt", dateTime);

                cmd.ExecuteNonQuery();
            }
        }


















        /// <summary>
        /// lưu 1 mess vào database
        /// </summary>
        /// <param name="message"></param>
        /// <returns>số mess được lưu</returns>
        //public int Insert(Message message)
        //{
        //    //using (SqlCommand cmd = new SqlCommand($"insert into {messdatabase}(data,source,destination,state,timestamp) values (@data,@source,@destination,@state,@timestamp)", connect))
        //    //{
        //    //    cmd.Parameters.AddWithValue("@data", message.data);
        //    //    cmd.Parameters.AddWithValue("@source", message.source);
        //    //    cmd.Parameters.AddWithValue("@destination", message.destination);
        //    //    cmd.Parameters.AddWithValue("@state", message.state);
        //    //    cmd.Parameters.AddWithValue("@timestamp", message.timestamp);
        //    //    return cmd.ExecuteNonQuery();
        //    //}
        //    return 1;
        //}
        /// <summary>
        /// xoá 1 mess trong database
        /// </summary>
        /// <param name="message"></param>
        /// <returns>số mess được xoá</returns>
        //public static int Delete(Message message) 
        //{
        //    using (SqlConnection connect = new SqlConnection(connection))
        //    {
        //        connect.Open();
        //        using (SqlCommand cmd = new SqlCommand($"delete from {messdatabase} where id = @id", connect))
        //        {
        //            cmd.Parameters.AddWithValue("@id", message.Id);
        //            return cmd.ExecuteNonQuery();
        //        }
        //    }
        //}
        /// <summary>
        /// lấy 20 tin nhắn gần nhất kể từ thời điểm timestamp
        /// </summary>
        /// <param name="username">user muốn lấy tin nhắn</param>
        /// <param name="timestamp">thời gian mốc</param>
        /// <returns>list 20 mess được lấy hoặc ít hơn nếu không đủ 20 tin</returns>
        //public static List<Message> GetHistoryMess(string username,DateTime timestamp) 
        //{
        //    List<Message> messlist = new List<Message>();
        //    using (SqlConnection connect = new SqlConnection(connection))
        //    {
        //        connect.Open();
        //        using (SqlCommand cmd = new SqlCommand($"select top 20 * from {messdatabase} where [to]=@to and timestamp < @timestamp",connect))
        //        {
        //            cmd.Parameters.AddWithValue("@to", username);
        //            cmd.Parameters.AddWithValue("@timestamp", timestamp);
        //            using (SqlDataReader reader = cmd.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    messlist.Add(SqlUtils<Message>.SqlReaderToEntity(reader));//đọc thông tin từ database vào class Message
        //                }
        //            }
        //        }
        //    }
        //    return messlist;
        //}
        /// <summary>
        /// lấy về các tin nhắn được gửi đến khi user offlinne
        /// </summary>
        /// <param name="username"></param>
        /// <returns>danh sách tin nhắn</returns>
        //public static List<Message> GetMessWhenUserOffline(string username)
        //{
        //    List<Message> messagelist = new List<Message>();
        //    using (SqlConnection connect = new SqlConnection(connection))
        //    {
        //        connect.Open();
        //        using (SqlCommand cmd = new SqlCommand($"select * from {messdatabase} where destination=@destination and state=@state order by timestamp", connect))
        //        {
        //            cmd.Parameters.AddWithValue("@destination",username);
        //            cmd.Parameters.AddWithValue("@state",0);
        //            using (SqlDataReader reader = cmd.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    Message message = SqlUtils<Message>.SqlReaderToEntity(reader);
        //                    messagelist.Add(message);
        //                    SetMessageState(message, true);
        //                }
        //            }
        //        }
        //    }
        //    return messagelist;
        //}
        /// <summary>
        /// thay đổi trạng thái của message thành state
        /// </summary>
        /// <param name="message">message cần thay đổi</param>
        /// <param name="state">trạng thái muốn thay đổi</param>
        /// <returns>1 (số message được thay đổi)</returns>
        //public static int SetMessageState(Message message,bool state)
        //{
        //    using (SqlConnection connect = new SqlConnection(connection))
        //    {
        //        connect.Open();
        //        using (SqlCommand cmd = new SqlCommand($"update {messdatabase} set state=@state where id=@id", connect))
        //        {
        //            cmd.Parameters.AddWithValue("@state",state);
        //            cmd.Parameters.AddWithValue("@id", message.Id);
        //            return cmd.ExecuteNonQuery();
        //        }
        //    }
        //}
    }
}
