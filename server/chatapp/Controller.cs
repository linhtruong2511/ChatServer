﻿using chatapp.common;
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
        public FileService fileService;
        public HandleController handleController;
        public BinaryReader reader;
        public BinaryWriter writer;
        public readonly object lock_reader = new object();
        public readonly object lock_writer = new object();
        public string username = "empty";
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
        public int HandleTask(Packet packet)//xử lý thông điệp nhận được - chỉ trả về <0 khi có yêu cầu disconnect
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
                            messageService.SendMessage(userSession, packet.From, requestMessage.Contents, packet.To);
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
                case PacketTypeEnum.SENDFILE:
                    foreach (UserSession userSession in ManageUser.UserSessions)
                    {
                        if (userSession.ID == packet.To && userSession.isOnline)
                        {
                            fileService.SendFile(userSession.writer, (PacketFile)packet, lock_writer);
                        }
                        else if (userSession.ID == packet.To && !userSession.isOnline)
                        {
                            fileService.SaveFile((PacketFile)packet);
                        }
                        else
                        {
                            continue;
                        }
                    }
                    return 1;
                case PacketTypeEnum.DISCONNECT:
                    handleController.DisconnectUser(reader, writer, TcpClient, userService, username);
                    gui.ShowAction($"{username} is disconnect");
                    return -1;
                case PacketTypeEnum.HISTORYCHAT:
                    DateTime timestampload = JsonConvert.DeserializeObject<DateTime>(Encoding.UTF8.GetString(packet.Data));
                    List<Message> messages = messageService.GetAllMessage(packet.From, packet.To, timestampload);
                    
                    if (messages.Count != 0)
                    {
                        List<FileInfos> files = fileService.GetAllFile(packet.From, packet.To, timestampload, messages[messages.Count - 1].CreateAt);
                        SortedList<DateTime, ChatObject> chatObjects = new SortedList<DateTime, ChatObject>();
                        for (int i = 0; i < messages.Count; i++)
                        {
                            chatObjects.Add(messages[i].CreateAt, messages[i]);
                        }
                        for (int i = 0; i < files.Count; i++)
                        {
                            chatObjects.Add(files[i].CreateAt, files[i]);
                        }
                        for (int i = chatObjects.Count-1; i >=0; i--)
                        {
                            if (chatObjects.Values[i] is Message)
                            {
                                Message message = (Message)chatObjects.Values[i];
                                Packet packetmessage = new Packet(PacketTypeEnum.HISTORYMESSAGES, Encoding.UTF8.GetBytes(message.Contents), message.Source, message.Destination, message.CreateAt);
                                NetworkUtils.Write(writer, packetmessage, lock_writer);
                            }
                            else
                            {
                                FileInfos file = (FileInfos)chatObjects.Values[i];
                                Packet packetfile = new PacketFile(PacketTypeEnum.HISTORYFILE, file.Data, file.Source, file.Destination, file.CreateAt, file.FileName);
                                NetworkUtils.Write(writer, packetfile, lock_writer);
                            }
                        }
                    }
                    break;
                case PacketTypeEnum.DELETEFILE:
                    Console.WriteLine("receive change message");
                    //fileService.DeleteFile(packet.From, packet.To, packet.createAt);
                    NetworkUtils.Write(writer, packet, lock_writer);
                    break;
                case PacketTypeEnum.DELETEMESSAGE:
                    Console.WriteLine("receive delete message");
                    //messageService.DeleteMessage(packet.From, packet.To, packet.createAt);
                    NetworkUtils.Write(writer, packet, lock_writer);
                    break;
                case PacketTypeEnum.CHANGEMESSAGE:
                    break;

            }
            return 1;
        }
    }
}
