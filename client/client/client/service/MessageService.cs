using chatapp.common;
using chatapp.dto;
using chatapp.util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client.service
{
    internal class MessageService
    {
        private MessageService() { }
        public static async Task SendMessage(StreamWriter writer,string username)
        {
            Console.Write("Enter username to send message : ");
            string destinationusername = Console.ReadLine();
            Console.Write("Enter message : ");
            string message = Console.ReadLine();
            await NetworkUtils.WriteStreamAsync(writer, new Packet(PacketTypeEnum.SENDMESSAGE, message,username, destinationusername));
        }
        public static async Task ReceiveMessage(StreamReader reader)
        {
            Packet packet = await NetworkUtils.ReadStreamAsync(reader);
            if (packet.Type == PacketTypeEnum.SENDMESSAGE)
            {
                Console.WriteLine($"\n{packet.From} : {packet.Data}");
            }
            else
            {
                Console.WriteLine(packet.Data);
            }
        }
    }
}
