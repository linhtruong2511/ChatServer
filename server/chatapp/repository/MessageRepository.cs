using chatapp.context;
using chatapp.model;
using chatapp.util;
using System;
using System.Collections.Generic;
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

        public List<Message> GetAllMessages(int source, int destination)
        {
            string query = "select * from message where (source=@source and destination=@destination) or (source=@destination and destination=@source) order by createAt";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@source", source);
                cmd.Parameters.AddWithValue("@destination", destination);
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
            string query = "insert into message (source, destination, contents, Status, createAt) values (@source, @destination, @contents, @Status, @createAt)";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@source", source);
                cmd.Parameters.AddWithValue("@destination", destination);
                cmd.Parameters.AddWithValue("@contents", contents);
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.Parameters.AddWithValue("@createAt", dateTime);

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
    }
}
