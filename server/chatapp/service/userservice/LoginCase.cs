using chatapp.common.Class;
using chatapp.common;
using chatapp.dto;
using chatapp.model;
using chatapp.repository;
using chatapp.service.managelist;
using chatapp.gui.event_;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using chatapp.dto.request;
using System.IO;
using chatapp.util;
using System.Runtime.CompilerServices;

namespace chatapp.service.userservice
{
    /// <summary>
    /// lớp xử lý các trường hợp người dùng đăng nhập
    /// </summary>
    internal class LoginCase
    {
        private LoginCase() { }
        /// <summary>
        /// gửi thông báo đăng nhập thành công
        /// thay đổi trạng thái cơ sở dữ liệu
        /// thay đổi ip thực tế
        /// </summary>
        /// <param name="packet"></param>
        /// <param name="user"></param>
        /// <param name="writer"></param>
        /// <param name="reader"></param>
        /// <param name="tcpclient"></param>
        /// <returns></returns>
        //public static async Task<int> SuccessLogin()
        //    Packet packet,
        //    LoginRequest user,
        //    StreamWriter writer,
        //    StreamReader reader,
        //    TcpClient tcpclient,
        //    ManageUserSessionList manageusersessionlist,
        //    App app,
        //    EventHandler serverevent,
        //    UserRepository userRepository)
        //{
        //    Console.WriteLine($"{packet.From} login");// thông báo cho server
        //    Packet notificationpacket = new Packet(PacketTypeEnum.NOTIFICATION, "success login", "Server", packet.From);//tạo 1 packet thông báo
        //    manageusersessionlist.SetWriterAndReader(user.username, writer, reader);//thêm luồng đọc và ghi 
        //    manageusersessionlist.SetState(user.username, true);//bật trạng thái người dùng
        //    manageusersessionlist.SetIP(user.username,tcpclient.Client.RemoteEndPoint.ToString());//thêm địa chỉ ip đang sử dụng
        //    userRepository.SetState(user.username, true);
        //    userRepository.SetRealTimeIP(user.username, tcpclient.Client.RemoteEndPoint.ToString());//thay đổi ip sang ip đang online
        //    await NetworkUtils.WriteStreamAsync(writer, notificationpacket);// gửi thông điệp về cho client đã đăng nhập thành công
        //    List<Message> messagelist = MessageRepository.GetMessWhenUserOffline(user.username);//lấy ra danh sách các tin nhắn khi client offline
        //    foreach (Message mess in messagelist)//gửi tin nhắn chưa được gửi đến user
        //    {
        //        await NetworkUtils.WriteStreamAsync(writer, new Packet(PacketTypeEnum.SENDMESSAGE, mess.Contents, mess.Source, mess.Destination));
        //    }
        //    serverevent?.Invoke(app, new LoginSuccessEvent(packet.From));
           //return 1;
        //}
        /// <summary>
        /// gửi thông báo sai mật khẩu
        /// </summary>
        /// <param name="packet"></param>
        /// <param name="writer"></param>
        /// <returns>1</returns>
        //public static async Task<int> IncorrectPassword(Packet packet,StreamWriter writer,App app,EventHandler serverevent)
        //{
        //    //Console.WriteLine($"someone try to login {packet.From} account but password is incorrect");
        //    //Packet notificationpacket = new Packet(PacketTypeEnum.NOTIFICATION, "login fail due to password is incorrect", "Server", packet.From);
        //    //await NetworkUtils.WriteStreamAsync(writer, notificationpacket);
        //    //serverevent?.Invoke(app,new LoginFailEvent(packet.From));
        //    return 1;
        //}
        /// <summary>
        /// gửi thông báo không tồn tại tên người dùng
        /// </summary>
        /// <param name="writer"></param>
        /// <returns>1</returns>
        //public static async Task<int> NotExistUsername(Packet packet, StreamWriter writer, App app, EventHandler serverevent)
        //{
        //    Console.WriteLine($"refuse login request ");
        //    Packet notificationpacket = new Packet(PacketTypeEnum.NOTIFICATION, "login fail due to username is not Exist", "Server", packet.From);
        //    await NetworkUtils.WriteStreamAsync(writer, notificationpacket);
        //    serverevent?.Invoke(app, new LoginFailEvent(packet.From));
        //    return 1;
        //}
    }
}
