using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;
using ClientApp.common.Class;
using System;
using System.Text;
using ClientApp.common.Enum;
using ClientApp.common;
namespace ClientApp.utils 
{
    public class NetworkUtils
    {
        private NetworkUtils() { }
        public static Packet Read(BinaryReader reader)
        {
            PacketTypeEnum type = (PacketTypeEnum)reader.ReadInt32();
            int dataLength = reader.ReadInt32();
            byte[] data = reader.ReadBytes(dataLength);
            int from = reader.ReadInt32();
            int to = reader.ReadInt32();
            string time = reader.ReadString();
            DateTime createAt = JsonConvert.DeserializeObject<DateTime>(time);
            if (type == PacketTypeEnum.SENDFILE || type == PacketTypeEnum.HISTORYFILE)
            {
                string fileName = reader.ReadString();
                return new PacketFile(type, data, from, to, createAt,fileName);
            }
            return new Packet(type, data, from, to,createAt);
        }
        public static void Write(BinaryWriter writer, Packet packet)
        {
            writer.Write((int)packet.Type);
            writer.Write(packet.DataLength);
            writer.Write(packet.Data);
            writer.Write(packet.From);
            writer.Write(packet.To);
            writer.Write(JsonConvert.SerializeObject(packet.createAt));
            if (packet is PacketFile)
            {
                writer.Write(((PacketFile)packet).fileName);
            }
            writer.Flush();
        }
    }
}
