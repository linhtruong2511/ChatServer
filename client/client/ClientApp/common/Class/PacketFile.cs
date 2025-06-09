using ClientApp.common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.common.Class
{
    public class PacketFile : Packet
    {
        public string fileName { get; set; }
        public PacketFile(PacketTypeEnum type, byte[] data, int from, int to, DateTime createAt,string fileName):base(type, data, from, to, createAt)
        {
            this.fileName = fileName;
        }
        public PacketFile(PacketTypeEnum type, byte[] data, int from, int to,string fileName):base(type,data,from,to)
        {
            this.fileName = fileName;
        }
    }
}
