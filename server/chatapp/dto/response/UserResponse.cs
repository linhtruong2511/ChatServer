using chatapp.common.Class;
using chatapp.model;

namespace chatapp.dto.response
{
    public class UserResponse
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public UserResponse(User user)
        {
            this.ID = user.ID;
            this.Name = user.Name;
        }
        public UserResponse(UserSession userSession)
        {
            this.ID = userSession.ID;
            this.Name = userSession.Name;
        }
    }
}
