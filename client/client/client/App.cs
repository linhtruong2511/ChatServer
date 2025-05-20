using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using client.gui;
namespace client
{
    public class App
    {
        private TcpClient tcpClient;
        public StreamReader reader { get; set; }
        public StreamWriter writer { get; set; }
        public EventHandler clientevent { get; set; }
        public Controller controller { get; set; }
        public int MyID { get; } = 1; 
        public App() 
        {
            Console.WriteLine("Client is online");
            tcpClient = new TcpClient();
            tcpClient.Connect(new IPEndPoint(IPAddress.Loopback, 5000));
            controller = new Controller();
            reader = new StreamReader(tcpClient.GetStream());
            writer = new StreamWriter(tcpClient.GetStream());
            writer.AutoFlush = true;
        }
        ~App()
        {
            tcpClient.Dispose();
            reader.Dispose();
            writer.Dispose();
        }
        public async Task Start(Gui gui)
        {
            try
            {
                Task reading = Task.Run(async () => await controller.HandleReadingTask(reader,gui));
                await reading;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Server close");
                MessageBox.Show("Server close", "Notification");
            }
        }
    }
}
