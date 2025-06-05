using chatapp.common;
using System;

namespace chatapp.dto
{
    /// <summary>
    /// Lớp gói tin :
    /// ->Type : định dạng của gói tin
    /// ->Data : nội dung gói tin
    /// ->From : user gửi
    /// ->To : User nhận
    /// ->Status : Trạng thái gói tin
    /// </summary>
    internal class Packet
    {
        public PacketTypeEnum Type {  get; set; }
        public byte[] Data { get; set; }
        public int From { get; set; }
        public int To { get; set; }
        public int DataLength { get; set; }
        public DateTime createAt { get; set; }
        public Packet(PacketTypeEnum type, byte[] data, int from, int to,DateTime timestamp)
        {
            Type = type;
            Data = data;
            From = from;
            To = to;
            DataLength = data.Length;
            createAt = timestamp;
        }
        public Packet(PacketTypeEnum type, byte[] data, int from, int to)
        {
            Type = type;
            Data = data;
            From = from;
            To = to;
            DataLength = data.Length;
            createAt = DateTime.Now;
        }

        public Packet() { }
    }
}
