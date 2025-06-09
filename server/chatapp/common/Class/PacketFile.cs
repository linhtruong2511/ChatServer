using chatapp.dto;
using System;

namespace chatapp.common.Class
{
    internal class PacketFile : Packet
    {
        public string fileName { get; set; }
        public PacketFile(PacketTypeEnum type, byte[] data, int from, int to, DateTime timestamp, string fileName) : base(type, data, from, to, timestamp)
        {
            this.fileName = fileName;
        }
        public PacketFile(PacketTypeEnum type, byte[] data, int from, int to, string fileName) : base(type, data, from, to)
        {
            this.fileName = fileName;
        }
    }
}
