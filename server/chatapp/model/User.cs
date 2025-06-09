namespace chatapp.model
{
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
