using chatapp.common;
using chatapp.common.Class;
using chatapp.dto;
using chatapp.dto.request;
using chatapp.model;
using chatapp.repository;
using chatapp.service.managelist;
using chatapp.util;
using chatapp.gui.event_;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace chatapp.service.userservice
{
    /// <summary>
    /// lớp xử lý các trường hợp người dùng đăng ký
    /// </summary>
    internal class SignUpCase
    {
        /// <summary>
        /// xử lý các trường hợp người gửi muốn đăng ký
        /// </summary>
        private SignUpCase() { }
        /// <summary>
        /// tên người dùng đã tồn tại , gửi thông báo về cho người gửi
        /// </summary>
        /// <param Name="packet"></param>
        /// <param Name="writer"></param>
        /// <returns>1</returns>
        public static async Task<int> UsernameUsed(Packet packet,StreamWriter writer,App app,EventHandler server)
        {
            //Console.WriteLine($"someone want to create a account but fail");
            //Packet notificationpacket = new Packet(PacketTypeEnum.NOTIFICATION, "sign up fail due to username is used", "Server", packet.From);
            //await NetworkUtils.WriteStreamAsync(writer, notificationpacket);
            //server?.Invoke(app, new SignUpFailEvent(packet.From));
            return 1;
        }
        /// <summary>
        /// gửi thông báo đăng ký thành công
        /// thao tác lưu người dùng vào cơ sở dữ liệu
        /// </summary>
        /// <param Name="packet"></param>
        /// <param Name="user">lớp lưu username và password</param>
        /// <param Name="writer">luồng ghi</param>
        /// <param Name="reader">luồng đọc</param>
        /// <param Name="manageusersessionlist">danh sách các user trong phiên</param>
        /// <param Name="tcpclient">socket kết nối client với server</param>
        /// <returns>1</returns>
        public static async Task<int> SuccessSignUp(Packet packet,LoginRequest user,StreamWriter writer,StreamReader reader,ManageUserSessionList manageusersessionlist,TcpClient tcpclient,App app,EventHandler serverevent)
        {
            //Console.WriteLine($"{packet.From} is created");
            ////UserRepository.Insert(ConvertUtils.UserRequestToUser(user, tcpclient));
            //manageusersessionlist.AddUser(new UserSession(user.username, user.password, reader, writer));
            //Packet notificationpacket = new Packet(PacketTypeEnum.NOTIFICATION, "success sign up", "Server", packet.From);
            //await NetworkUtils.WriteStreamAsync(writer, notificationpacket);
            //serverevent?.Invoke(app, new SignUpSuccessEvent(packet.From));
            
            return 1;
        }
    }
}
