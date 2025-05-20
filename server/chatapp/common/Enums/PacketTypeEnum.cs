using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chatapp.common
{
    /// <summary>
    /// định dạng gói tin:
    /// ->NOFITICATION: gói tin thông báo
    /// ->LOGIN : gói tin đăng nhập
    /// ->SIGNUP : gói tin đăng ký
    /// ->DISCONNECT : gói tin huỷ kết nối
    /// -> SENDMESSAGE : gói tin gửi dữ liệu
    /// </summary>
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
