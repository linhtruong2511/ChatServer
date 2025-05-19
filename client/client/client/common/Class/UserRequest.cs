using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chatapp.dto.request
{
    public class UserRequest
    {
        public string username { get; set; }
        public string password { get; set; }
        public UserRequest(string username, string password)
        {
            this.username = username;
            this.password = password;
        }
    }
}
