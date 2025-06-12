using System;
using System.Data.SqlClient;
using System.Net.Sockets;
using System.Threading.Tasks;
using chatapp.context;

namespace chatapp
{
    internal class App
    {
        public TcpListener TcpListener { get; set; }
        public ManageUser ManageSessionUser { get; set; } 
        public event EventHandler ServerEvent;
        public MainForm gui;

        public App(MainForm gui) 
        {
            // mở connection cho toàn app
            Database db = new Database();
            db.OpenConnection();

            SqlConnection conn = Database.GetConnection();

            // quản lý user connect tới là việc của toàn bộ app nên để context cho nó bao bọc app lại => ở nơi đâu cũng có thể sử dụng để danh sách các user connect này
            // như vậy sẽ đỡ mất công truyền xuống
            ManageSessionUser = new ManageUser();
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
            //ServerOnlineEvent sẽ hiển thị tất cả những thứ mà admin có quyền quản
            //lý lên UI của server
            //
            do
            {
                TcpClient tcpClient = TcpListener.AcceptTcpClient();
                Task client = new Task(() =>
                {
                    Controller controller = new Controller(tcpClient, gui);
                    controller.HandleClient(this,ServerEvent);
                });
                client.Start();
            } while (true);
        }
    }
}
