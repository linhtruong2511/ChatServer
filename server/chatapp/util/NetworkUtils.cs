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
            try
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
                    Console.WriteLine(timestamp);
                    createAt = JsonConvert.DeserializeObject<DateTime>(timestamp);
                    if(packetType == PacketTypeEnum.SENDFILE || packetType == PacketTypeEnum.HISTORYFILE)
                    {
                        fileName = reader.ReadString();
                    }
                }
                Console.WriteLine(packetType);
                if (fileName == "")
                {
                    return new Packet(packetType, bytes, from, to,createAt);
                }
                else
                {
                    return new PacketFile(packetType, bytes, from, to, createAt, fileName);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading packet: " + ex.Message);
                Console.WriteLine(ex.StackTrace);
                return new Packet(PacketTypeEnum.DISCONNECT, new byte[0], 0, 0);
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
                    Console.WriteLine(packet.DataLength);
                }
                writer.Flush();
            }
        }
    }
}