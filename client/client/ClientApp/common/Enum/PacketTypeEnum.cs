using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.common.Enum
{
    public enum PacketTypeEnum
    {
        NOTIFICATION = 1,
        LOGIN = 2,
        SUCCESSLOGIN = 3,
        DISCONNECT = 4,
        SENDMESSAGE = 5,
        HISTORYMESSAGES = 6,
        USERINFO = 7,
        ADDUSERINFO = 8,
        HISTORYFILE = 9,
        SENDFILE = 10,
        HISTORYCHAT = 11,
        DELETEMESSAGE = 12,
        DELETEFILE = 13,
        CHANGEMESSAGE = 14,
        GROUPINFO = 15,
        ADDTOAGROUP = 16,
        NUMBEROFCHATLOAD = 17,
    }
}
