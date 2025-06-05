using chatapp.common;
using chatapp.common.Class;
using chatapp.context;
using chatapp.dto;
using chatapp.model;
using chatapp.repository;
using chatapp.util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chatapp.service
{
    internal class MessageService
    {
        MessageRepository messageRepository;

        public MessageService()
        {
            messageRepository = new MessageRepository();
        }

        // neu co bug thi o day
        public async Task SendMessage(UserSession userSession, int source, string content, int toUserId)
        {
            // trạng thái message = 1 do đã được gửi
            messageRepository.SaveMessage(source, toUserId, content);
            await NetworkUtils.WriteStreamAsync(userSession.writer, new Packet(PacketTypeEnum.SENDMESSAGE, content, source, toUserId));
        }

        public void SaveMessage(int source, string content, int toUserId)
        {
            // trạng thái message = 0 do chưa được gửi
            messageRepository.SaveMessage(source, toUserId, content,0); 
        }
        public List<Message> GetAllMessage(int source,int destination = 2)
        {
            return messageRepository.GetAllMessages(source,destination);
        }
        public void UpdateMessageStatus(int id,bool status)
        {
            messageRepository.UpdateMessageStatus(id,status);
        }
    }
}
