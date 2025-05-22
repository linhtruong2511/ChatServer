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
    public class User
    {
        public int ID { get; set; }
        public string name { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public bool Status { get; set; }
        public string IP { get; set; }
        public User(string username,string password,string IP,bool state=true)
        {
            this.username = username;
            this.password = password;
            this.IP = IP;
            this.Status = state;
        }
        public User() { }
    }
}
