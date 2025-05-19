using chatapp.dto.request;
using client.server;
using client.service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace client
{
    public class Controller
    {
        public string username { get; set; }
        public StreamReader reader { get; set; }
        public StreamWriter writer { get; set; }
        public CancellationTokenSource cts { get; set; }
        public CancellationToken ct { get; set; }
        public Controller() 
        {
        
        }
        ~Controller()
        {
            reader.Dispose();
            writer.Dispose();
        }
        public async Task HandleReadingTask(StreamReader reader)
        {
            while (true) // dừng khi disconnect
            {
                await MessageService.ReceiveMessage(reader);
            }
        }
        public async Task HandleWritingTask(StreamWriter writer)
        {
            while (true)// dừng khi disconnect
            {
                await MessageService.SendMessage(writer, username);
            }
        }
        public async Task HandleDisconnect()
        {
           
        }
    }
}
