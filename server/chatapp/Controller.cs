using chatapp.common;
using chatapp.common.Class;
using chatapp.context;
using chatapp.dto;
using chatapp.dto.request;
using chatapp.dto.response;
using chatapp.gui.event_;
using chatapp.repository;
using chatapp.service;
using chatapp.service.managelist;
using chatapp.util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
namespace chatapp
{
    internal class Controller
    {
        private TcpClient TcpClient;
        private event EventHandler ServiceEvent;
        private ManageUser manageSessionUser;
        public UserService userService;
        public MessageService messageService;
        StreamReader reader;
        StreamWriter writer;
        MainForm gui;

        
        public Controller(TcpClient tcpClient, MainForm gui) 
        {
            userService = new UserService();
            messageService = new MessageService();
            this.TcpClient = tcpClient;
            this.gui = gui;
            reader = new StreamReader(tcpClient.GetStream());
            writer = new StreamWriter(tcpClient.GetStream());
            writer.AutoFlush = true;
        }

        /// <summary>
        /// tạo luồng đọc,ghi và giao tiếp với client
        /// </summary>
        /// <returns></returns>
        public async Task HandleClient(App app,EventHandler eventServer)
        {
            try
            {
                while (true) 
                {
                    Packet packet = await NetworkUtils.ReadStreamAsync(reader);
                    gui.ShowAction(packet.Data);
                    int value = await HandleTask(packet);
                    if (value < 0) break;
                }// nếu giá trị trả về sau xử lý > 0 thì vẫn tiếp tục đọc còn nếu không thì kết thúc
            }
            catch (Exception ex)
            {
                Console.WriteLine("is close");
                Console.WriteLine(ex.Message);
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
        /// <param Name="packet">thông điệp nhận</param>
        /// <param Name="writer">luồng ghi</param>
        /// <param Name="reader">luồng đọc</param>
        /// <returns>giá trị xác định xem có đọc tiếp hay không:
        /// nếu <0 thì không đọc nữa <=> disconnect
        /// nếu >0 thì đọc tiếp
        /// </returns>
        public async Task<int> HandleTask(Packet packet)//xử lý thông điệp nhận được  
        {
            switch (packet.Type)
            {
                case PacketTypeEnum.LOGIN:

                    LoginRequest requestLogin = ConvertUtils.PacketDataToDTO<LoginRequest>(packet);
                    
                    bool loginSuccess = userService.Login(requestLogin.username, requestLogin.password);

                    if (loginSuccess)
                    {
                        // thong bao dang nhap thanh cong
                        Packet notification = new Packet(PacketTypeEnum.NOTIFICATION, "login success", 0, packet.From);
                        await NetworkUtils.WriteStreamAsync(writer, notification);
                        string data = notification.Data;
                        // dự kiến viết tiếp ở đây
                        // trả về lịch sử các tin nhắn ,
                        Packet historyMessages = new Packet(PacketTypeEnum.HISTORYMESSAGES, JsonConvert.SerializeObject(messageService.GetAllMessage(packet.From,2)),0, packet.From); 
                        await NetworkUtils.WriteStreamAsync(writer, historyMessages);

                        // và danh sách các user,group của toàn bộ server
                        Packet userInfo = new Packet(PacketTypeEnum.USERINFO, JsonConvert.SerializeObject(ManageUser.UserResponses), 0, packet.From);
                        await NetworkUtils.WriteStreamAsync(writer, userInfo);
                        
                        gui.AddMessage(data); // hiện thị thông báo trên giao

                        gui.ShowAction($"{requestLogin.username} success to login");
                        
                        // cập nhật userSession trong list 
                        for(int i=0;i<ManageUser.UserSessions.Count;i++)
                        {
                            UserSession userSession = ManageUser.UserSessions[i];
                            if (userSession.username == requestLogin.username)
                            {
                                userSession.reader = reader;
                                userSession.writer = writer;
                                userSession.realtimeIP = TcpClient.Client.RemoteEndPoint.ToString();
                                userSession.isOnline = true;
                                break;
                            }
                        }
                        return 1;
                    }
                    else
                    {
                        //thong bao dang nhap that bai
                        Packet notification = new Packet(PacketTypeEnum.NOTIFICATION, "login fail", 0, packet.From);
                        await NetworkUtils.WriteStreamAsync(writer, notification);
                        string data = notification.Data;
                        gui.AddMessage(notification.Data);

                        gui.ShowAction($"someone with {TcpClient.Client.RemoteEndPoint} ip was fail to login");
                        return 1;// không close vội cho client nhập lại 
                    }
                case PacketTypeEnum.SENDMESSAGE:
                    //chuyển đổi dữ liệu nhận được thành đối tượng Message

                    SendMessageRequest requestMessage = ConvertUtils.PacketDataToDTO<SendMessageRequest>(packet);
                    Console.WriteLine(requestMessage.Contents);
                    foreach (UserSession userSession in ManageUser.UserSessions)
                    {
                        if (userSession.ID == packet.To && userSession.isOnline)
                        {
                            await messageService.SendMessage(userSession, packet.From, requestMessage.Contents, packet.To);
                        }
                        else if (userSession.ID == packet.To && !userSession.isOnline) // người dùng offline
                        {
                            // lưu thông tin vào cơ sở dữ liệu
                            messageService.SaveMessage(packet.From, requestMessage.Contents, packet.To);
                        }
                        else 
                        { 
                            continue; 
                        }
                    }
                    return 1;
                case PacketTypeEnum.DISCONNECT:
                    return -1;

            }
            return 1;
        }
    }
}
