using chatapp.common.Class;
using chatapp.context;
using chatapp.util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;

namespace chatapp.repository
{
    public class FileRepository
    {
        SqlConnection conn;
        public FileRepository()
        {
            conn = Database.GetConnection();
        }
        public void SaveFile(int Source, int Destination, byte[] data, string filename, DateTime CreateAt, bool status = false)
        {
            string query = "insert into files (Source, Destination, data, filename, CreateAt,status) values (@Source, @Destination, @data, @filename, @CreateAt,@status)";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Source", Source);
                cmd.Parameters.AddWithValue("@Destination", Destination);
                cmd.Parameters.AddWithValue("@data", data);
                cmd.Parameters.AddWithValue("@filename", filename);
                cmd.Parameters.AddWithValue("@CreateAt", CreateAt);
                cmd.Parameters.AddWithValue("@status", status);
                cmd.ExecuteNonQuery();
            }
        }
        public List<FileInfos> GetAllFile(int source,int destination,DateTime from,DateTime to)
        {
            string query = "select * from files where ((Source=@Source and Destination=@Destination) or (Destination=@Source and Source=@Destination)) and (CreateAt < @from and CreateAt > @to) order by CreateAt desc";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Source", source);
                cmd.Parameters.AddWithValue("@Destination", destination);
                cmd.Parameters.AddWithValue("@from", from);
                cmd.Parameters.AddWithValue("@to", to);
                SqlDataReader reader = cmd.ExecuteReader();
                List<FileInfos> files = new List<FileInfos>();
                while (reader.Read())
                {
                    FileInfos file = SqlUtils<FileInfos>.SqlReaderToEntity(reader);
                    files.Add(file);
                }
                reader.Close();
                return files;
            }
        }
        public void UpdateFileStatus(int id,bool status)
        {
            string query = "update files set status=@status where id=@id";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@status",status);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
