using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chatapp.common.Class
{
    /// <summary>
    /// lớp lưu thông tin liên quan đến user trong phiên làm việc server:
    /// ->username : tên user
    /// ->password : mật khẩu
    /// ->reader : luồng đọc của server với user
    /// ->writer : luồng ghi của server với user
    /// ->Status: trạng thái của user
    /// ->
    /// </summary>
    public class UserSession
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public StreamReader reader { get; set; }
        public StreamWriter writer { get; set; }
        public bool isOnline { get; set; }
        public string realtimeIP { get; set; }

        public UserSession(int ID,string username,string name, string password, StreamReader reader, StreamWriter writer, string realtimeIP="",bool state=true)
        {
            this.ID = ID;
            this.Name = name;
            this.username = username;
            this.password = password;
            this.reader = reader;
            this.writer = writer;
            this.isOnline = state;
            this.realtimeIP = realtimeIP;
        }
        public UserSession() { }
    }
}
