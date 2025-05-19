using chatapp.managelist;
using chatapp.repository;
using chatapp.service.managelist;
using chatapp.service;
using chatapp.gui.event_;
using chatapp.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using chatapp.context;
using System.Runtime.InteropServices.ComTypes;
using System.Windows.Forms;

namespace chatapp
{
    internal class App
    {
        public TcpListener TcpListener { get; set; }
        public ManageSessionUser ManageSessionUser { get; set; } 
        public ManageTaskList TaskList{ get; set; }
        public event EventHandler serverEvent;
        public MainForm gui;

        public App(MainForm gui) 
        {
            // mở connection cho toàn app
            Database db = new Database();
            db.OpenConnection();

            // quản lý user connect tới là việc của toàn bộ app nên để context cho nó bao bọc app lại => ở nơi đâu cũng có thể sử dụng để danh sách các user connect này
            // như vậy sẽ đỡ mất công truyền xuống
            ManageSessionUser = new ManageSessionUser();
            TcpListener = new TcpListener(System.Net.IPAddress.Loopback, 5000);
            this.gui = gui;
        }
        ~App()
        {
            TcpListener.Stop();
        }
        public void Start()
        {
            TcpListener.Start(10);
            //Console.WriteLine("Server is online");
            //serverEvent?.Invoke(this, new ServerOnlineEvent());
            //
            //ServerOnlineEvent sẽ hiển thị tất cả những thứ mà admin có quyền quản
            //lý lên UI của server
            //
            do
            {
                TcpClient tcpClient = TcpListener.AcceptTcpClient();
                Task client = new Task(async() =>
                {
                    //Controller controller = new Controller(tcpClient, gui);
                    Controller controller = new Controller(tcpClient, gui);
                    await controller.HandleClient(this,serverEvent);
                });
                //TaskList.AddTask(client);
                client.Start();
            } while (true);
        }
    }
}
