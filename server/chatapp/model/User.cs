using System;

namespace chatapp.model
{
    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Status { get; set; }
        public string IP { get; set; }
        public int PositionId { get; set; }
        public int DepartmentId { get; set; }
        public string Phone { get; set; }   
        public string Address { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;

        public User(string username,string password,string IP,bool state=true)
        {
            this.Username = username;
            this.Password = password;
            this.IP = IP;
            this.Status = state;
        }
        public User() { }
    }
}
