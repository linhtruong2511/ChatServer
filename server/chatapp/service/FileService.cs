using chatapp.common.Class;
using chatapp.repository;
using chatapp.util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace chatapp.service
{
    internal class FileService
    {
        public FileRepository fileRepository { get; set; }
        public FileService()
        {
            fileRepository = new FileRepository();
        }
        public void SaveFile(PacketFile packet, bool state = false)
        {
            fileRepository.SaveFile(packet.From, packet.To, packet.Data, packet.fileName, packet.createAt, state);
        }
        public void SendFile(BinaryWriter writer, PacketFile packet, Object lock_writer)
        {
            fileRepository.SaveFile(packet.From, packet.To, packet.Data, packet.fileName, packet.createAt, true);
            NetworkUtils.Write(writer, packet, lock_writer);
        }
        public List<FileInfos> GetAllFile(int source, int destination,DateTime from,DateTime to)
        {
            return fileRepository.GetAllFile(source, destination,from,to);
        }
        public void UpdateFileStatus(int id, bool status)
        {
            fileRepository.UpdateFileStatus(id, status);
        }
    }
}
