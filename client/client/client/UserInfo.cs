using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client
{
    public class UserInfo
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public UserInfo(int iD, string name)
        {
            ID = iD;
            Name = name;
        }
        public UserInfo() { }
    }
}
