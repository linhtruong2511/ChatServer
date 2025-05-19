using chatapp.common;
using chatapp.dto;
using chatapp.util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chatapp.service.userservice
{
    internal class DefaultCase
    {
        /// <summary>
        /// lớp xử lý khi packet sai format
        /// </summary>
        private DefaultCase() { }
        /// <summary>
        /// in ra thông báo sai format rồi gửi cho người gửi
        /// </summary>
        /// <param name="packet">gói tin</param>
        /// <param name="writer">luồng ghi của người gửi</param>
        /// <returns>-1</returns>
        public static async Task<int> IncorrectPacketFormat(Packet packet,StreamWriter writer)
        {
            Console.WriteLine($"{packet.From} send a incorrect format packet");
            Packet noficationpacket = new Packet(PacketTypeEnum.NOTIFICATION, "incorrect format packet", 0, packet.From);
            await NetworkUtils.WriteStreamAsync(writer, noficationpacket);
            return -1;
        }
    }
}
