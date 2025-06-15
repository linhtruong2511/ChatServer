using chatapp.common.Class;
using chatapp.context;
using chatapp.util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

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
                cmd.Parameters.Add("@CreateAt",SqlDbType.DateTime2).Value = CreateAt;
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
                cmd.Parameters.Add("@from",SqlDbType.DateTime2).Value = from;
                cmd.Parameters.Add("@to", SqlDbType.DateTime2).Value = to;
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
        public void DeleteFile(int Source,int Destination,DateTime createAt)
        {
            string query = "delete from files where Source=@Source and Destination=@Destination and CreateAt=@CreateAt";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Source", Source);
                cmd.Parameters.AddWithValue("@Destination", Destination);
                cmd.Parameters.Add("@CreateAt", SqlDbType.DateTime2).Value = createAt;
                cmd.ExecuteNonQuery();
            }
        }
        public FileInfos GetAFile(int Source,int Destination,DateTime createAt)
        {
            string query = "select top 1 * from files where (Source=@Source and Destination=@Destination) or (Source=@Destination and Destination=@Source) and createAt<@createAt";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Source", Source);
                cmd.Parameters.AddWithValue("@Destination", Destination);
                cmd.Parameters.Add("@createAt",SqlDbType.DateTime2).Value = createAt;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return SqlUtils<FileInfos>.SqlReaderToEntity(reader);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
    }
}
