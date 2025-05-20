using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chatapp.common
{
    internal enum PacketTypeEnum
    {
        NOTIFICATION,
        LOGIN,
        SIGNUP,
        DISCONNECT,
        SENDMESSAGE,
        HISTORYMESSAGES,
        USERINFO,
        ADDUSERINFO,
    }
}
