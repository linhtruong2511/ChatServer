using chatapp.common.Class;
using chatapp.context;
using chatapp.dto.request;
using chatapp.dto.response;
using chatapp.model;
using chatapp.util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace chatapp
{
    public class HandleController
    {
        public ManageUser manageUser { get; }
        /// <summary>
        /// User có online hay không ?
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns>trạng thái user</returns>
        public bool IsUserOnline(LoginRequest loginRequest)
        {
            foreach(UserSession userSession in ManageUser.UserSessions)
            {
                if(loginRequest.username == userSession.username)
                {
                    return userSession.isOnline;
                }
            }
            return false;
        }
        /// <summary>
        /// Thông tin đăng nhập chính xác hay không ?
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns>thông tin sai/đúng</returns>
        public bool IsCorrectUserInformation(LoginRequest loginRequest)
        {
            foreach (UserSession userSession in ManageUser.UserSessions)
            {
                if (loginRequest.username == userSession.username && loginRequest.password == userSession.password)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// trả về id và name của user yêu cầu đăng nhập
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns>trả về nếu tồn tài , nếu không trả về id=0,name=""</returns>
        public UserResponse GetUserLoginInformation(LoginRequest loginRequest)
        {
            foreach (User user in ManageUser.Users)
            {
                if (loginRequest.username == user.username )
                {
                    return new UserResponse(user);
                }
            }
            return new UserResponse(new User());
        }
        public void UpdateUserSessions(StreamWriter writer,StreamReader reader,TcpClient tcpClient,LoginRequest loginRequest)
        {
            for (int i = 0; i < ManageUser.UserSessions.Count; i++)
            {
                UserSession userSession = ManageUser.UserSessions[i];
                if (userSession.username == loginRequest.username)
                {
                    userSession.reader = reader;
                    userSession.writer = writer;
                    userSession.realtimeIP = tcpClient.Client.RemoteEndPoint.ToString();
                    userSession.isOnline = true;
                    break;
                }
            }
        }
    }
}
