using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
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
        public static int count= 0;
        public static int numberOfLoadChat = 0;

        public Context()
        {
            try
            {
                TcpClient = new TcpClient();
                TcpClient.Connect(IPAddress.Loopback, 5000);
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
            count = 0;
            numberOfLoadChat = 0;
            DestinationId = destinationId;
        }
    }
}
