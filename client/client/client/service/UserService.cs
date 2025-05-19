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
            Console.WriteLine(ConvertPacketDataUtils.DTOToPacketData<UserRequest>(ur));
            NetworkUtils.WriteStream(writer, new Packet(PacketTypeEnum.LOGIN, ConvertPacketDataUtils.DTOToPacketData<UserRequest>(ur),0,0));
            notification = NetworkUtils.ReadStream(reader).Data;
            Console.WriteLine(notification);
            return notification;
        }
        public static void Disconnect(StreamReader reader,StreamWriter writer)
        {
            NetworkUtils.WriteStream(writer, new Packet(PacketTypeEnum.DISCONNECT, "",0,0));
            string nofitication = NetworkUtils.ReadStream(reader).Data;
            if(nofitication == "disconnect success")
            {

            }
            Console.WriteLine(nofitication);
        }
    }
}
