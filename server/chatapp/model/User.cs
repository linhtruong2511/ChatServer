using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chatapp.model
{
    /// <summary>
    /// lớp đại diện cho thực thể ở bảng clientdatabase trong cơ sở dữ liệu
    /// </summary>
    internal class User
    {
        public string username { get; set; }
        public string password { get; set; }
        public bool state { get; set; }
        public string realtimeIP { get; set; }
        public User(string username,string password,string realtimeIP,bool state=true)
        {
            this.username = username;
            this.password = password;
            this.realtimeIP = realtimeIP;
            this.state = state;
        }
        public User() { }

        public User(SqlDataReader reader) 
        {
            
        }
    }
}
