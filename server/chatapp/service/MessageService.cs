using chatapp.common;
using chatapp.common.Class;
using chatapp.dto;
using chatapp.model;
using chatapp.repository;
using chatapp.util;
using System;
using System.Collections.Generic;
using System.Text;

namespace chatapp.service
{
    internal class MessageService
    {
        MessageRepository messageRepository;

        public MessageService()
        {
            messageRepository = new MessageRepository();
        }
        public void SendMessage(UserSession userSession, int source, string content, int toUserId,DateTime createAt)
        {
            // trạng thái message = 1 do đã được gửi
            messageRepository.SaveMessage(source, toUserId, content,createAt);
            NetworkUtils.Write(userSession.writer, new Packet(PacketTypeEnum.SENDMESSAGE, Encoding.UTF8.GetBytes(content), source, toUserId), userSession.lock_writer);
        }

        public void SaveMessage(int source, string content, int toUserId,DateTime createAt)
        {
            // trạng thái message = 0 do chưa được gửi
            messageRepository.SaveMessage(source, toUserId, content,createAt,0);
        }
        public List<Message> GetAllMessage(int source, int destination, DateTime from)
        {
            return messageRepository.GetAllMessages(source, destination, from);
        }
        public void UpdateMessageStatus(int id, bool status)
        {
            messageRepository.UpdateMessageStatus(id, status);
        }
        public void DeleteMessage(int Source, int Destination, DateTime createAt)
        {
            messageRepository.DeleteMessage(Source, Destination, createAt);
        }
    }
}
