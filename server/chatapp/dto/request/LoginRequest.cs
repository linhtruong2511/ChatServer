namespace chatapp.dto.request
{
    /// <summary>
    /// lưu username và password của user
    /// </summary>
    public class LoginRequest
    {
        public string username { get; set; }
        public string password { get; set; }
    }
}
