using chatapp.common;
using chatapp.common.Class;
using chatapp.context;
using chatapp.dto;
using chatapp.dto.request;
using chatapp.gui.event_;
using chatapp.repository;
using chatapp.service;
using chatapp.service.managelist;
using chatapp.util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
namespace chatapp
{
    internal class Controller
    {
        private TcpClient tcpClient;
        private event EventHandler ServiceEvent;
        private ManageSessionUser manageSessionUser; //danh sách các lưu thông tin cần thiết các user trong phiên
        public UserService userService;
        public MessageService messageService;
        StreamReader reader;
        StreamWriter writer;
        MainForm gui;

        
        public Controller(TcpClient tcpClient, MainForm gui) 
        {
            userService = new UserService();
            messageService = new MessageService();
            this.tcpClient = tcpClient;
            this.gui = gui;
            reader = new StreamReader(tcpClient.GetStream());
            writer = new StreamWriter(tcpClient.GetStream());
        }

        /// <summary>
        /// tạo luồng đọc,ghi và giao tiếp với client
        /// </summary>
        /// <returns></returns>
        public async Task HandleClient(App app,EventHandler eventServer)
        {
            writer.AutoFlush = true;
            try
            {
                while (true) 
                {
                    Packet packet = await NetworkUtils.ReadStreamAsync(reader);
                    int value = await HandleTask(packet, writer);
                    if (value < 0) break;
                }// nếu giá trị trả về sau xử lý > 0 thì vẫn tiếp tục đọc còn nếu không thì kết thúc
            }
            catch (Exception ex)
            {
                Console.WriteLine("is close");
            }
            finally
            {
                reader.Dispose();
                writer.Dispose();
            }
        }
        /// <summary>
        /// sử lý thông điệp nhận được 
        /// </summary>
        /// <param name="packet">thông điệp nhận</param>
        /// <param name="writer">luồng ghi</param>
        /// <param name="reader">luồng đọc</param>
        /// <returns>giá trị xác định xem có đọc tiếp hay không:
        /// nếu <0 thì không đọc nữa <=> disconnect
        /// nếu >0 thì đọc tiếp
        /// </returns>
        public async Task<int> HandleTask(Packet packet,StreamWriter writer)//xử lý thông điệp nhận được  
        {

            switch (packet.Type)
            {
                case PacketTypeEnum.LOGIN:

                    //chuyển đổi dữ liệu nhận được thành đối tượng UserRequest
                    LoginRequest requestLogin = ConvertUtils.PacketDataToDTO<LoginRequest>(packet);
                    
                    bool loginSuccess = userService.Login(requestLogin.username, requestLogin.password);

                    if (loginSuccess)
                    {
                        // thong bao dang nhap thanh cong
                        Packet notification = new Packet(PacketTypeEnum.NOTIFICATION, "login success", 0, packet.From);
                        await NetworkUtils.WriteStreamAsync(writer, notification);


                        string data = notification.Data;
                        gui.AddMessage(data); // hiện thị thông báo trên giao 
                        

                        UserSession userSession = new UserSession();
                        userSession.id = packet.From;
                        userSession.writer = writer;
                        userSession.reader = reader;
                        userSession.username = requestLogin.username;


                        ManageSessionUser.AddUserSession(userSession);
                        return 1;
                    }
                    else
                    {
                        //thong bao dang nhap that bai
                        Packet notification = new Packet(PacketTypeEnum.NOTIFICATION, "login fail", 0, packet.From);
                        await NetworkUtils.WriteStreamAsync(writer, notification);
                        string data = notification.Data;
                        gui.AddMessage(notification.Data);
                        return -1;
                    }
                case PacketTypeEnum.SENDMESSAGE:
                    //chuyển đổi dữ liệu nhận được thành đối tượng Message


                    List<UserSession> userSessions = ManageSessionUser.UserSessions;

                    SendMessageResquest requestMessage = ConvertUtils.PacketDataToDTO<SendMessageResquest>(packet);

                    foreach (UserSession userSession in userSessions)
                    {
                        if (userSession.id == packet.To && userSession.isOnline)
                        {
                            await messageService.SendMessage(userSession, packet.From, requestMessage.Contents, packet.To);
                        }
                        else if (userSession.id == packet.To && !userSession.isOnline) // người dùng offline
                        {
                            // lưu thông tin vào cơ sở dữ liệu
                            messageService.SaveMessage(packet.From, requestMessage.Contents, packet.To);
                        }
                        else return -1;
                    }
                    return 1;
                case PacketTypeEnum.DISCONNECT:
                    return -1;

            }
            return 1;
        }
    }
}
