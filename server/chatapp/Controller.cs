using chatapp.common;
using chatapp.common.Class;
using chatapp.context;
using chatapp.dto;
using chatapp.dto.request;
using chatapp.dto.response;
using chatapp.gui.event_;
using chatapp.model;
using chatapp.repository;
using chatapp.service;
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
        public HandleController handleController;
        public StreamReader reader;
        public StreamWriter writer;
        public string username = "empty";
        public MainForm gui;
        public Controller(TcpClient tcpClient, MainForm gui) 
        {
            userService = new UserService();
            messageService = new MessageService();
            handleController = new HandleController();
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
                    int value = await HandleTask(packet);
                    if (value < 0) break;
                }// nếu giá trị trả về sau xử lý > 0 thì vẫn tiếp tục đọc còn nếu không thì kết thúc
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{username} is close");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            finally
            {
                // đóng kết nối với user khi có lỗi
                handleController.DisconnectUser(reader, writer, TcpClient, userService, username);
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
        public async Task<int> HandleTask(Packet packet)//xử lý thông điệp nhận được - chỉ trả về <0 khi có yêu cầu disconnect
        {
            switch (packet.Type)
            {
                case PacketTypeEnum.LOGIN:

                    LoginRequest requestLogin = ConvertUtils.PacketDataToDTO<LoginRequest>(packet);

                    if (handleController.IsCorrectUserInformation(requestLogin))
                    {
                        if (handleController.IsUserOnline(requestLogin))
                        {
                            // thông báo không thể đăng nhập vì đã có người sử dụng account này rồi 
                            Packet notification = new Packet(PacketTypeEnum.NOTIFICATION, "Can't login because this account is online", 0, packet.From);
                            await NetworkUtils.WriteStreamAsync(writer, notification);
                        }
                        else
                        {
                            // lưu thông tin user đã kết nối cho controller
                            username = requestLogin.username;

                            // nếu đăng nhập thành công trả về id và name của user (không cần thông báo login success nữa để nó là 1 type của packet luôn) 
                            Packet successLogin = new Packet(PacketTypeEnum.SUCCESSLOGIN,JsonConvert.SerializeObject(handleController.GetUserLoginInformation(requestLogin)), 0, packet.From);
                            await NetworkUtils.WriteStreamAsync(writer, successLogin);
                            
                            // tiếp đó trả về danh sách các user tồn tại trong cơ sở dữ liệu về cho client để có thể hiển thị trên ui và kết nối
                            Packet userInfo = new Packet(PacketTypeEnum.USERINFO, JsonConvert.SerializeObject(ManageUser.UserResponses), 0, packet.From);
                            await NetworkUtils.WriteStreamAsync(writer, userInfo);
                            
                            // update thông tin của user vừa được login vào danh sách quản lý usersession
                            handleController.UpdateUserSessions(writer, reader, TcpClient, requestLogin);

                            // update thông tin của user vừa được login lên db
                            userService.UpdateUserStatusInDB(requestLogin.username, true);

                            // thông báo lên cho server
                            gui.ShowAction($"{requestLogin.username} success to login");
                        }
                    }
                    else
                    {
                        // thông báo đăng nhập thất bại do không đúng thông tin
                        Packet notification = new Packet(PacketTypeEnum.NOTIFICATION, "Can't login because incorrect username and password", 0, packet.From);
                        await NetworkUtils.WriteStreamAsync(writer, notification);
                        
                        // thông báo lên cho server
                        gui.ShowAction($"someone with {TcpClient.Client.RemoteEndPoint} ip was fail to login");
                    }
                    return 1;
                case PacketTypeEnum.SENDMESSAGE:

                    //chuyển đổi dữ liệu nhận được thành đối tượng Message
                    SendMessageRequest requestMessage = ConvertUtils.PacketDataToDTO<SendMessageRequest>(packet);
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
                case PacketTypeEnum.HISTORYMESSAGES:
                    List<HistoryMessage> historyMessages = new List<HistoryMessage>();
                    List<Message> messages = messageService.GetAllMessage(packet.From, packet.To);
                    int index = 0;

                    while(index<messages.Count)
                    {

                        // update status của những tin nhắn == 0 thành 1 
                        if (messages[index].Status == 0)
                        {
                            messageService.UpdateMessageStatus(messages[index].Id, true);
                        }

                        // thêm vào danh sách tin nhắn gửi đi 
                        historyMessages.Add(new HistoryMessage(messages[index]));
                        index++;

                        // mỗi lần gửi 5 tin nhắn cho user 
                        if (historyMessages.Count == 5 || index >= messages.Count)
                        {
                            Packet historyMessagePacket = new Packet(PacketTypeEnum.HISTORYMESSAGES, JsonConvert.SerializeObject(historyMessages), 0, packet.From);
                            await NetworkUtils.WriteStreamAsync(writer, historyMessagePacket);
                            historyMessages.Clear();
                        }
                    }
                    // khi hết tin nhắn gửi thông báo kết thúc
                    Packet endSendHistoryMessages = new Packet(PacketTypeEnum.NOTIFICATION, "end", 0, packet.From);
                    await NetworkUtils.WriteStreamAsync(writer, endSendHistoryMessages);
                    return 1;
                case PacketTypeEnum.DISCONNECT:
                    // đóng kết nối
                    handleController.DisconnectUser(reader, writer, TcpClient, userService, username);
                    gui.ShowAction($"{username} is disconnect");
                    return -1;

            }
            return 1;
        }
    }
}
