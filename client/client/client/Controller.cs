using chatapp.common;
using chatapp.dto;
using chatapp.dto.request;
using chatapp.util;
using client.gui;
using client.server;
using client.service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace client
{
    public class Controller
    {
        public string username { get; set; }
        public StreamReader reader { get; set; }
        public StreamWriter writer { get; set; }
        public Controller() 
        {
        
        }
        ~Controller()
        {
            reader.Dispose();
            writer.Dispose();
        }
        public async Task HandleReadingTask(StreamReader reader,Gui gui)
        {
            while (true)
            {
                Packet packet = await NetworkUtils.ReadStreamAsync(reader);
                if (packet.Type == PacketTypeEnum.SENDMESSAGE)
                {
                    Console.WriteLine($"\n{packet.From} : {JsonConvert.DeserializeObject<SendMessageRequest>(packet.Data).Contents}");
                }
                else if (packet.Type == PacketTypeEnum.HISTORYMESSAGES)
                {
                    List<Message> messages = JsonConvert.DeserializeObject<List<Message>>(packet.Data);
                    gui.ShowMessages(messages);
                }
                else if (packet.Type == PacketTypeEnum.USERINFO)
                {
                    ManageUserInfos.userInfos = JsonConvert.DeserializeObject<List<UserInfo>>(packet.Data);
                }
                else if (packet.Type == PacketTypeEnum.ADDUSERINFO)
                {
                    ManageUserInfos.userInfos.Add(JsonConvert.DeserializeObject<UserInfo>(packet.Data));
                }
                else
                    break;
            }
        }
        public async Task HandleDisconnect()
        {
           
        }
    }
}
