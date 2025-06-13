using chatapp.context;
using chatapp.model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chatapp.repository
{
    public class PositionRepository
    {
        public List<Position> GetAllPostsition()
        {
            using(SqlCommand cmd = new SqlCommand("select * from positions", Database.GetConnection()))
            {
                List<Position> positions = new List<Position>();
                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        positions.Add(new Position
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Name = reader["name"].ToString()
                        });
                    }
                }
                return positions;
            }
        }

        public Position GetPositionById(int id)
        {
            using(SqlCommand cmd = new SqlCommand("select * from positions where id = @id", Database.GetConnection()))
            {
                cmd.Parameters.AddWithValue("@id", id);
                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Position
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Name = reader["name"].ToString()
                        };
                    }
                }
            }
            return null; // Trả về null nếu không tìm thấy
        }
    }
}
