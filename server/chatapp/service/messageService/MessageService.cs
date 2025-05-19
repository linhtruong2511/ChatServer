using chatapp.common;
using chatapp.common.Class;
using chatapp.context;
using chatapp.dto;
using chatapp.model;
using chatapp.repository;
using chatapp.service.managelist;
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
            List<UserSession> userSessions = ManageSessionUser.UserSessions;
            StreamWriter writer = userSession.writer;
            messageRepository.SaveMessage(source, toUserId, content);
            await NetworkUtils.WriteStreamAsync(writer, new Packet(PacketTypeEnum.SENDMESSAGE, content, source, toUserId));
        }

        public void SaveMessage(int source, string content, int toUserId)
        {
           messageRepository.SaveMessage(source, toUserId, content); 
        }   
    }
}
