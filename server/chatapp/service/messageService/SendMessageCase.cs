//using chatapp.common;
//using chatapp.common.Class;
//using chatapp.dto;
//using chatapp.repository;
//using chatapp.util;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace chatapp.service.messageservice
//{
//    /// <summary>
//    /// xử lý các trường hợp gửi message
//    /// </summary>
//    internal class SendMessageCase
//    {
//        private SendMessageCase() { }
//        /// <summary>
//        /// trả thông báo không tìm thấy người nhận
//        /// </summary>
//        /// <param name="packet">gói tin</param>
//        /// <param name="writer">luồng ghi của người gửi</param>
//        /// <returns>1</returns>
//        public static async Task<int> NotFoundRecipient(Packet packet,StreamWriter writer)
//        {
//            Console.WriteLine($"{packet.From} send {packet.Data} to {packet.To} (user not exsist)");
//            Packet nofiticationpacket = new Packet(PacketTypeEnum.NOTIFICATION, "Cannot find destination user", "Server", packet.From);
//            await NetworkUtils.WriteStreamAsync(writer, nofiticationpacket);
//            return 1;
//        }
//        /// <summary>
//        /// lưu tin nhắn vào database với trạng thái = 0 
//        /// </summary>
//        /// <param name="packet">gói tin</param>
//        /// <returns>1 (số lượng tin nhắn được lưu)</returns>
//        public static int RecipientOffline(Packet packet)
//        {
//            Console.WriteLine($"{packet.From} send {packet.Data} to {packet.To}(offline)");
//            return MessageRepository.Insert(ConvertUtils.PacketToMessage(packet, 0));
//        }
//        /// <summary>
//        /// lưu tin nhắn vào database với trạng thái = 1
//        /// rồi gửi tin nhắn tới người nhận
//        /// </summary>
//        /// <param name="packet"></param>
//        /// <param name="writer"></param>
//        /// <param name="recipient"></param>
//        /// <returns></returns>
//        public static async Task<int> RecipientFound(Packet packet,UserSession recipient)
//        {
//            Console.WriteLine($"{packet.From} send {packet.Data} to {packet.To} (success)");
//            await NetworkUtils.WriteStreamAsync(recipient.writer, packet);
//            return MessageRepository.Insert(ConvertUtils.PacketToMessage(packet, 1));
//        }
//    }
//}
