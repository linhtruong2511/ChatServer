using chatapp.common;
using chatapp.dto;
using chatapp.util;
using client.gui;
using Newtonsoft.Json;
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
        private ManageUserInfos manageUserInfos;
        private MessageService() { }
        public static async Task SendMessage(StreamWriter writer,SendMessageRequest message,int destinationid=2)
        {
            await NetworkUtils.WriteStreamAsync(writer, new Packet(PacketTypeEnum.SENDMESSAGE,JsonConvert.SerializeObject(message),1,destinationid));
        }
    }
}
