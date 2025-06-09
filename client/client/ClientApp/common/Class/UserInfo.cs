namespace ClientApp.common.Class
{
    public class UserInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public UserInfo(int id, string Name)
        {
            Id = id;
            this.Name = Name;
        }
        public UserInfo() { }
    }
}
