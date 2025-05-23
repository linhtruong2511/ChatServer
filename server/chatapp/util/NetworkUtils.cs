using chatapp.dto;
using chatapp.dto.request;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace chatapp.util
{
    internal class NetworkUtils
    {
        private NetworkUtils() { }
        /// <summary>
        /// chuyển dữ liệu đọc được sang dạng Packet
        /// </summary>
        /// <param Name="stream"></param>
        /// <returns>gói tin Packet</returns>
        public static async Task<Packet> ReadStreamAsync(StreamReader stream)
        {
            string data = await stream.ReadLineAsync();
            if (data == null) // xảy ra khi kết nối bị ngắt
            {
                return new Packet(common.PacketTypeEnum.DISCONNECT, "", 0, 0);
            }
            else 
            {
                return JsonConvert.DeserializeObject<Packet>(data);
            }
        }
        /// <summary>
        /// chuyển packet thành chuỗi gửi đi
        /// </summary>
        /// <param Name="stream"></param>
        /// <param Name="packet"></param>
        /// <returns>void</returns>
        public static async Task WriteStreamAsync(StreamWriter stream, Packet packet)
        {
            Console.WriteLine(packet.Data);
            await stream.WriteLineAsync(JsonConvert.SerializeObject(packet)); 
        }
    }
}