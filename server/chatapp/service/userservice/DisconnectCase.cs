using chatapp.common;
using chatapp.dto;
using chatapp.repository;
using chatapp.service.managelist;
using chatapp.util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using chatapp.gui.event_;

namespace chatapp.service.userservice
{
    /// <summary>
    /// lớp xử lý các trường hợp của gói tin ngắt kết nối
    /// </summary>
    internal class DisconnectCase
    {
        private DisconnectCase() { }
        /// <summary>
        /// thông báo xác nhận ngắt kết nối
        /// thao tác với cơ sở dữ liêu lưu trạng thái của user
        /// </summary>
        /// <param name="packet"></param>
        /// <param name="writer"></param>
        /// <returns>-1</returns>
        public static async Task<int> SuccessDisconnect(Packet packet,StreamWriter writer,App app,EventHandler serverevent,UserRepository userRepository)
        {
            //Console.WriteLine($"{packet.From} disconnect");
            //Packet notificationpacket = new Packet(PacketTypeEnum.NOTIFICATION, "disconnect success", ", packet.From);
            //UserRepository.SetState(packet.From, false);
            //await NetworkUtils.WriteStreamAsync(writer, notificationpacket);
            //serverevent?.Invoke(app, new DisconnectEvent(packet.From));
            return -1;
        }
    }
}
