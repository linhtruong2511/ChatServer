using chatapp.common.Class;
using chatapp.context;
using chatapp.dto.request;
using chatapp.dto.response;
using chatapp.model;
using chatapp.service;
using System.IO;
using System.Net.Sockets;

namespace chatapp
{
    public class HandleController
    {
        public ManageUser manageUser { get; }
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
        public void UpdateUserSessions(BinaryWriter writer,BinaryReader reader,TcpClient tcpClient,LoginRequest loginRequest,object lock_reader,object lock_writer)
        {
            for (int i = 0; i < ManageUser.UserSessions.Count; i++)
            {
                UserSession userSession = ManageUser.UserSessions[i];
                if (userSession.username == loginRequest.username)
                {
                    userSession.reader = reader;
                    userSession.writer = writer;
                    userSession.lock_reader = lock_reader;
                    userSession.lock_writer = lock_writer;
                    userSession.realtimeIP = tcpClient.Client.RemoteEndPoint.ToString();
                    userSession.isOnline = true;
                    break;
                }
            }
        }
        public void DisconnectUser(BinaryReader reader,BinaryWriter writer,TcpClient tcpClient,UserService userService,string username)
        {
            userService.UpdateUserStatusInDB(username, false);
            userService.UpdateUserIPInDB(username, "empty");
            ManageUser.SetStatus(username, false);
            ManageUser.SetIP(username, "empty");
            reader.Dispose();
            writer.Dispose();
        }
    }
}
