using ClientApp.common.Enum;
using System;

namespace ClientApp.common.Class
{
    public class Packet
    {
        public PacketTypeEnum Type { get; set; }
        public byte[] Data { get; set; }
        public int From { get; set; }
        public int To { get; set; }
        public int DataLength { get; set; }
        public DateTime createAt { get; set; }
        public Packet(PacketTypeEnum type, byte[] data, int from, int to)
        {
            Type = type;
            Data = data;
            From = from;
            To = to;
            DataLength = data.Length;
            createAt = DateTime.Now;
        }
        public Packet(PacketTypeEnum type, byte[] data, int from, int to, DateTime createAt)
        {
            Type = type;
            Data = data;
            From = from;
            To = to;
            DataLength = data.Length;
            this.createAt = createAt;
        }
        public Packet() { }
    }
}
