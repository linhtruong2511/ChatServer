using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientApp.common.Class
{
    public class User
    {
        public UserInfo userInfo { get; set; }
        public UCcontacts uCcontacts { get; set; }
        public User(UserInfo userInfo,UCcontacts uCcontacts) 
        {
            this.userInfo = userInfo;
            this.uCcontacts = uCcontacts;
        }
    }
}
