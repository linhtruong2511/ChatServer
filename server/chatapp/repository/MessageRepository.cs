using chatapp.context;
using chatapp.model;
using chatapp.util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace chatapp.repository
{
    public class MessageRepository
    {
        SqlConnection conn;
        public MessageRepository()
        {
            conn = Database.GetConnection();
        }

        public List<Message> GetAllMessages(int source, int destination,DateTime from)
        {
            string query = "select top 5 * from message where ((Source=@Source and Destination=@Destination) or (Source=@Destination and Destination=@Source)) and(CreateAt < @from) order by CreateAt desc";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Source", source);
                cmd.Parameters.AddWithValue("@Destination", destination);
                cmd.Parameters.Add("@from", SqlDbType.DateTime2).Value = from;
                SqlDataReader reader = cmd.ExecuteReader();
                List<Message> messages = new List<Message>();
                while (reader.Read())
                {
                    Message message = SqlUtils<Message>.SqlReaderToEntity(reader);
                    messages.Add(message);
                }
                reader.Close();
                return messages;
            }
        }

        public void SaveMessage (int source, int destination, string contents, int status = 1)
        {
            DateTime dateTime = DateTime.Now;
            string query = "insert into message (Source, Destination, contents, Status, CreateAt) values (@Source, @Destination, @contents, @Status, @CreateAt)";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Source", source);
                cmd.Parameters.AddWithValue("@Destination", destination);
                cmd.Parameters.AddWithValue("@contents", contents);
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.Parameters.Add("@CreateAt", SqlDbType.DateTime2).Value = dateTime;
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateMessageStatus(int id,bool status)
        {
            string query = "update message set status=@status where id=@id";
            using (SqlCommand cmd = new SqlCommand(query, conn)) 
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@status", status);
                cmd.ExecuteNonQuery();
            } 
        } 
        public void DeleteMessage(int Source,int Destination,DateTime createAt)
        {
            string query = "delete from message where (Source=@Source and Destination=@Destination) or (Source=@Destination and Destination=@Source) and CreateAt=@CreateAt";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Source", Source);
                cmd.Parameters.AddWithValue("@Destination", Destination);
                cmd.Parameters.Add("@CreateAt", SqlDbType.DateTime2).Value = createAt;
                cmd.ExecuteNonQuery();
            }
        }
    }
}
