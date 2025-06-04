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
        //public void SaveFile(int source,int destination, byte[] data,string filename,DateTime createAt,bool status=false)
        //{
        //    string query = "insert into files (source, destination, data, filename, createAt,status) values (@source, @destination, @data, @filename, @createAt,@status)";
        //    using (SqlCommand cmd = new SqlCommand(query, conn))
        //    {
        //        cmd.Parameters.AddWithValue("@source", source);
        //        cmd.Parameters.AddWithValue("@destination", destination);
        //        cmd.Parameters.AddWithValue("@data", data);
        //        cmd.Parameters.AddWithValue("@filename", filename);
        //        cmd.Parameters.AddWithValue("@createAt", createAt);
        //        cmd.Parameters.AddWithValue("@status", status);
        //        cmd.ExecuteNonQuery();
        //    }
        //}
        public void SaveFile(int source, int destination, byte[] data, string filename, DateTime createAt, bool status = false)
        {
            string query = "INSERT INTO files (source, destination, data, filename, createAt, status) " +
                           "VALUES (@source, @destination, @data, @filename, @createAt, @status)";
            Console.WriteLine(data.Length);
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.Add("@source", SqlDbType.Int).Value = source;
                cmd.Parameters.Add("@destination", SqlDbType.Int).Value = destination;
                cmd.Parameters.Add("@data", SqlDbType.VarBinary, data.Length).Value = data; // RÕ KIỂU DỮ LIỆU!
                cmd.Parameters.Add("@filename", SqlDbType.NVarChar, 255).Value = filename;
                cmd.Parameters.Add("@createAt", SqlDbType.DateTime).Value = createAt;
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = status;

                cmd.ExecuteNonQuery();
            }
        }
        public List<FileInfos> GetAllFile(int source,int destination)
        {
            string query = "select * from files where (source=@source and destination=@destination) or (destination=@source and source=@destination)";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@source", source);
                cmd.Parameters.AddWithValue("@destination", destination);
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
