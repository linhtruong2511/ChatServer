using chatapp.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chatapp.dto
{
    internal class Packet
    {
        public PacketTypeEnum Type {  get; set; }
        public string Data { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public int Status { get; set; } = 200;
        public Packet(PacketTypeEnum type, string data, string from, string to, int status=200)
        {
            Type = type;
            Data = data;
            From = from;
            To = to;
            Status = status;
        }
    }
}
