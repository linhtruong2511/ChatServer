using chatapp.dto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chatapp.util
{
    internal class ConvertPacketDataUtils
    {
        private ConvertPacketDataUtils() { }
        public static T PacketDataToDTO<T>(Packet packet)
        {
            return JsonConvert.DeserializeObject<T>(packet.Data);
        }
        public static string DTOToPacketData<T>(T Object)
        {
            return JsonConvert.SerializeObject(Object);
        }
    }
}
