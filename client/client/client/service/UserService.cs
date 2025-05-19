using chatapp.common;
using chatapp.dto;
using chatapp.dto.request;
using chatapp.util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client.server
{
    internal class UserService
    {
        private UserService() { }
        public static string Login(StreamReader reader,StreamWriter writer,UserRequest ur)
        {
            string notification;
            NetworkUtils.WriteStream(writer, new Packet(PacketTypeEnum.LOGIN, ConvertPacketDataUtils.DTOToPacketData<UserRequest>(ur),ur.username,"Server"));
            notification = NetworkUtils.ReadStream(reader).Data;
            Console.WriteLine(notification);
            return notification;
        }
        public static string SignUp(StreamReader reader,StreamWriter writer,UserRequest ur)
        {
            string nofitication;
            NetworkUtils.WriteStream(writer, new Packet(PacketTypeEnum.SIGNUP, ConvertPacketDataUtils.DTOToPacketData<UserRequest>(ur), "default", "Server"));
            nofitication = NetworkUtils.ReadStream(reader).Data;
            Console.WriteLine(nofitication);
            return nofitication;
        }
        public static void Disconnect(StreamReader reader,StreamWriter writer)
        {
            NetworkUtils.WriteStream(writer, new Packet(PacketTypeEnum.DISCONNECT, "", "default", "Server"));
            string nofitication = NetworkUtils.ReadStream(reader).Data;
            if(nofitication == "disconnect success")
            {

            }
            Console.WriteLine(nofitication);
        }
    }
}
