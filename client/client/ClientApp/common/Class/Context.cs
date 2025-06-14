using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientApp.common.Class
{
    public class Context
    {
        public static TcpClient TcpClient { get; set; }
        public static BinaryReader Reader { get; set; }
        public static BinaryWriter Writer { get; set; }
        public static int MyId { get; set; }
        public static string Name { get; set; }
        public static int DestinationId { get; set; }
        public static List<UserInfo> Users { get; set; }
        public static SortedList<DateTime, ChatObject> chatObjects = new SortedList<DateTime, ChatObject>();
        public static DateTime lastChatLoad = DateTime.Now;
        public static int lastChatIndex = 0;


        public Context()
        {
            try
            {
                TcpClient = new TcpClient();
                TcpClient.Connect("192.168.177.102", 5000);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Could not connect to the server. Please ensure the server is running.", "Connection Error");
            }
            Reader = new BinaryReader(TcpClient.GetStream());
            Writer = new BinaryWriter(TcpClient.GetStream());
            Users = new List<UserInfo>();
        }
        public static void Reset(int destinationId)
        {
            chatObjects.Clear();
            lastChatLoad = DateTime.Now;
            lastChatIndex = 0;
            DestinationId = destinationId;
        }
    }
}
