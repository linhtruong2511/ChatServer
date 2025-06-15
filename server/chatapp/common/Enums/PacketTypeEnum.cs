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
        HISTORYCHAT =11,
        DELETEMESSAGE = 12,
        DELETEFILE = 13,
        CHANGEMESSAGE = 14,
        GROUPINFO = 15,
        ADDTOAGROUP = 16,
        NUMBEROFCHATLOAD = 17,
    }
}
