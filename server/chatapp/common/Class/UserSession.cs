using System.IO;

namespace chatapp.common.Class
{
    /// <summary>
    /// lớp lưu thông tin liên quan đến user trong phiên làm việc server:
    /// ->username : tên user
    /// ->password : mật khẩu
    /// ->reader : luồng đọc của server với user
    /// ->writer : luồng ghi của server với user
    /// ->lock_writer : khoá gán với luồng ghi của user
    /// ->lock_reader : khoá gán với luồng đọc của user
    /// ->Status: trạng thái của user
    /// ->
    /// </summary>
    public class UserSession
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public BinaryReader reader { get; set; }
        public BinaryWriter writer { get; set; }
        public bool isOnline { get; set; }
        public string realtimeIP { get; set; }

        public object lock_reader { get; set; }

        public object lock_writer { get; set; }
        public UserSession(int ID,string name,string username, string password, BinaryReader reader, BinaryWriter writer, string realtimeIP="",bool state=true)
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
