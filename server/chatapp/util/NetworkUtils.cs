using chatapp.common;
using chatapp.common.Class;
using chatapp.dto;
using Newtonsoft.Json;
using System;
using System.IO;

namespace chatapp.util
{
    internal class NetworkUtils
    {
        private NetworkUtils() { }
        public static Packet Read(BinaryReader reader,object lock_reader)
        {
                PacketTypeEnum packetType;
                int packetLength,from,to;
                byte[] bytes;
                string timestamp;
                DateTime createAt;
                string fileName = "";
                lock (lock_reader)
                {
                    packetType = (PacketTypeEnum)reader.ReadInt32();
                    packetLength = reader.ReadInt32();
                    bytes = reader.ReadBytes(packetLength);
                    from = reader.ReadInt32();
                    to = reader.ReadInt32();
                    timestamp = reader.ReadString();
                    createAt = JsonConvert.DeserializeObject<DateTime>(timestamp);
                    if(packetType == PacketTypeEnum.SENDFILE || packetType == PacketTypeEnum.HISTORYFILE)
                    {
                        fileName = reader.ReadString();
                        return new PacketFile(packetType, bytes, from, to, createAt, fileName);
                    }
                    else
                    {
                        return new Packet(packetType, bytes, from, to, createAt);
                    }
                }
        }
        public static void Write(BinaryWriter writer, Packet packet,object lock_writer)
        {
            lock(lock_writer)
            {
                writer.Write((int)packet.Type);
                writer.Write(packet.DataLength);
                writer.Write(packet.Data);
                writer.Write(packet.From);
                writer.Write(packet.To);
                writer.Write(JsonConvert.SerializeObject(packet.createAt));
                if(packet is PacketFile)
                {
                    writer.Write(((PacketFile)packet).fileName);
                }
                writer.Flush();
            }
        }
    }
}