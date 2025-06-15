using chatapp.common;
using chatapp.common.Class;
using chatapp.context;
using chatapp.dto;
using chatapp.dto.request;
using chatapp.dto.response;
using chatapp.model;
using chatapp.service;
using chatapp.util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.Contracts;
using System.IO;
using System.Net.Sockets;
using System.Text;
namespace chatapp
{
    internal class Controller
    {
        private TcpClient TcpClient;
        public UserService userService;

        public MessageService messageService;
        // service để quản lý file, gửi file, lưu file, lấy file từ cơ sở dữ liệu
        public FileService fileService;

        public HandleController handleController;

        // sử dụng BinaryReader và BinaryWriter để đọc và ghi dữ liệu từ/to luồng mạng
        public BinaryReader reader;
        public BinaryWriter writer;

        // khoá để đồng bộ hoá việc đọc và ghi dữ liệu từ/to luồng mạng, tránh tình trạng nhiều luồng cùng truy cập vào
        public readonly object lock_reader = new object();
        public readonly object lock_writer = new object();

        public string username = "empty";
        // giao diện chính của ứng dụng để có thể hiển thị thông báo
        public MainForm gui;

        public Controller(TcpClient tcpClient, MainForm gui) 
        {
            userService = new UserService();
            messageService = new MessageService();
            fileService = new FileService();
            handleController = new HandleController();
            this.TcpClient = tcpClient;
            this.gui = gui;
            reader = new BinaryReader(tcpClient.GetStream());
            writer = new BinaryWriter(tcpClient.GetStream());
        }

        public void HandleClient(App app,EventHandler eventServer)
        {
            try
            {
                while (true) 
                {
                    Packet packet = NetworkUtils.Read(reader,lock_reader);
                    int value = HandleTask(packet);
                    if (value < 0) break;
                }
                // nếu giá trị trả về sau xử lý > 0 thì vẫn tiếp tục đọc còn nếu không thì kết thúc
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
        //xử lý thông điệp nhận được - chỉ trả về <0 khi có yêu cầu disconnect
        public int HandleTask(Packet packet)
        {
            Console.WriteLine(packet.createAt.ToString("HH:mm:ss:fffffff"));
            switch (packet.Type)
            {
                case PacketTypeEnum.LOGIN:

                    LoginRequest requestLogin = ConvertUtils.PacketDataToDTO<LoginRequest>(packet);

                    if (handleController.IsCorrectUserInformation(requestLogin))
                    {
                        if (handleController.IsUserOnline(requestLogin))
                        {
                            // thông báo không thể đăng nhập vì đã có người sử dụng account này rồi 
                            Packet notification = new Packet(PacketTypeEnum.NOTIFICATION,Encoding.UTF8.GetBytes("Can't login because this account is online"), 0, packet.From);
                            NetworkUtils.Write(writer, notification,lock_writer);
                        }
                        else
                        {
                            // lưu thông tin user đã kết nối cho controller
                            username = requestLogin.username;

                            // nếu đăng nhập thành công trả về id và name của user (không cần thông báo login success nữa để nó là 1 type của packet luôn) 
                            Packet successLogin = new Packet(PacketTypeEnum.SUCCESSLOGIN,Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(handleController.GetUserLoginInformation(requestLogin))), 0, packet.From);
                            NetworkUtils.Write(writer, successLogin, lock_writer);
                            
                            // tiếp đó trả về danh sách các user tồn tại trong cơ sở dữ liệu về cho client để có thể hiển thị trên ui và kết nối
                            Packet userInfo = new Packet(PacketTypeEnum.USERINFO, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(ManageUser.UserResponses)), 0, packet.From);
                            NetworkUtils.Write(writer, userInfo, lock_writer);
                            
                            // update thông tin của user vừa được login vào danh sách quản lý usersession
                            handleController.UpdateUserSessions(writer, reader, TcpClient, requestLogin,lock_reader,lock_writer);

                            // update thông tin của user vừa được login lên db
                            userService.UpdateUserStatusInDB(requestLogin.username, true);

                            // thông báo lên cho server
                            gui.ShowAction($"{requestLogin.username} success to login");
                        }
                    }
                    else
                    {
                        // thông báo đăng nhập thất bại do không đúng thông tin
                        Packet notification = new Packet(PacketTypeEnum.NOTIFICATION, Encoding.UTF8.GetBytes("Can't login because incorrect username and password"), 0, packet.From);
                        NetworkUtils.Write(writer, notification, lock_writer);
                        
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
                            messageService.SendMessage(userSession, packet.From, requestMessage.Contents, packet.To,packet.createAt);
                            break;
                        }
                        else if (userSession.ID == packet.To && !userSession.isOnline) // người dùng offline
                        {
                            // lưu thông tin vào cơ sở dữ liệu
                            messageService.SaveMessage(packet.From, requestMessage.Contents, packet.To,packet.createAt);
                            break;
                        }
                        else 
                        { 
                            continue; 
                        }
                    }
                    gui.ShowAction($"{username} send message");
                    // fix: Tìm kiếm
                    return 1;
                
                case PacketTypeEnum.SENDFILE:
                    foreach (UserSession userSession in ManageUser.UserSessions)
                    {
                        if (userSession.ID == packet.To && userSession.isOnline)
                        {
                            fileService.SendFile(userSession.writer, (PacketFile)packet, lock_writer);
                            break;
                        }
                        else if (userSession.ID == packet.To && !userSession.isOnline)
                        {
                            fileService.SaveFile((PacketFile)packet);
                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }
                    gui.ShowAction($"{username} send file");
                    return 1;

                case PacketTypeEnum.DISCONNECT:
                    handleController.DisconnectUser(reader, writer, TcpClient, userService, username);
                    gui.ShowAction($"{username} is disconnect");
                    return -1;

                case PacketTypeEnum.HISTORYCHAT:
                    // lấy thời gian tải lịch sử chat từ packet để làm gì ?
                    DateTime timeStampLoad = JsonConvert.DeserializeObject<DateTime>(Encoding.UTF8.GetString(packet.Data));
                    List<Message> messages = messageService.GetAllMessage(packet.From, packet.To, timeStampLoad);
                    if (messages.Count != 0)
                    {
                        // nếu có tin nhắn thì lấy tất cả file liên quan đến cuộc trò chuyện này
                        List<FileInfos> files = fileService.GetAllFile(packet.From, packet.To, timeStampLoad, messages[messages.Count - 1].CreateAt);
                        // tạo một danh sách các đối tượng chat để sắp xếp theo thời gian tạo
                        NetworkUtils.Write(writer, new Packet(PacketTypeEnum.NUMBEROFCHATLOAD, BitConverter.GetBytes(messages.Count + files.Count), 0, packet.From), lock_writer);
                        for (int i = 0; i < messages.Count; i++)
                        {
                            Packet packetmessage = new Packet(PacketTypeEnum.HISTORYMESSAGES, Encoding.UTF8.GetBytes(messages[i].Contents), messages[i].Source, messages[i].Destination, messages[i].CreateAt);
                            NetworkUtils.Write(writer, packetmessage, lock_writer);
                        }
                        for (int i = 0; i < files.Count; i++)
                        {
                            Packet packetfile = new PacketFile(PacketTypeEnum.HISTORYFILE, files[i].Data, files[i].Source, files[i].Destination, files[i].CreateAt, files[i].FileName);
                            NetworkUtils.Write(writer, packetfile, lock_writer);
                        }
                    }
                    else
                    {
                        FileInfos file = fileService.GetAFile(packet.From,packet.To,packet.createAt);
                        if (file != null)
                        {
                            NetworkUtils.Write(writer, new Packet(PacketTypeEnum.NUMBEROFCHATLOAD, BitConverter.GetBytes(1), 0, packet.From), lock_writer);
                            NetworkUtils.Write(writer, new PacketFile(PacketTypeEnum.HISTORYFILE, file.Data, file.Source, file.Destination, file.FileName),lock_writer);
                        }
                        else
                        {
                            NetworkUtils.Write(writer, new Packet(PacketTypeEnum.NOTIFICATION, Encoding.UTF8.GetBytes("Không còn tin nhắn để load"), 0, packet.From), lock_writer);
                        }
                    }
                    gui.ShowAction($"{username} load history chat");
                    break;

                case PacketTypeEnum.DELETEFILE:
                    fileService.DeleteFile(packet.From, packet.To, packet.createAt);
                    foreach(UserSession us in ManageUser.UserSessions)
                    {
                        if(us.ID == packet.To && us.isOnline)
                        {
                            NetworkUtils.Write(us.writer, packet, us.lock_writer);
                            break;
                        }
                    }
                    break;
                case PacketTypeEnum.DELETEMESSAGE:
                    Console.WriteLine(packet.createAt.ToString("HH:mm:ss:ffffff"));
                    Console.WriteLine(packet.From + "/" + packet.To);
                    messageService.DeleteMessage(packet.From, packet.To, packet.createAt);
                    foreach (UserSession us in ManageUser.UserSessions)
                    {
                        if (us.ID == packet.To && us.isOnline)
                        {
                            NetworkUtils.Write(us.writer, packet, us.lock_writer);
                            break;
                        }
                    }   
                    break;
                case PacketTypeEnum.CHANGEMESSAGE:
                    break;
            }
            return 1;
        }
    }
}
