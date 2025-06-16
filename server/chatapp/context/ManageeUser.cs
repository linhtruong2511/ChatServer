using chatapp.common;
using chatapp.common.Class;
using chatapp.dto;
using chatapp.dto.response;
using chatapp.model;
using chatapp.service;
using chatapp.util;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace chatapp.context
{
    public class ManageUser
    {
        public static List<UserSession> UserSessions { get; set; }
        public static List<UserResponse> UserResponses { get; set; }
        public static List<User> Users { get; set; }
        public UserService UserService { get; set; }

        public ManageUser() {
            UserService = new UserService();
            UserService.ResetAllUserStatus();
            UserService.ResetAllUserIP();
            Users = UserService.GetAllUser();
            UserSessions = ConvertUtils.UserListToUserSessionList(Users);
            UserResponses = ConvertUtils.UserListToUserResponseList(Users);
        }
        public void AddUser(UserSession userSession) {
            UserSessions.Add(userSession);
            UserResponses.Add(new UserResponse(userSession));
            ChangeClientUserInfos(userSession);
        }

        public static void RemoveUser(UserSession userSession) {
            UserSessions.Remove(userSession);
            UserResponses.Remove(new UserResponse(userSession));
        }
        public void ChangeClientUserInfos(UserSession userSession)
        {
            for (int i = 0; i < UserSessions.Count; i++)
            {
                if (UserSessions[i].isOnline)
                {
                    Packet packet = new Packet(PacketTypeEnum.ADDUSERINFO,Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new UserResponse(userSession))), 0, UserSessions[i].ID);
                    NetworkUtils.Write(UserSessions[i].writer,packet,userSession.lock_writer);
                }
            }
        }
        public List<UserSession> GetAllUserSessions() {
            return UserSessions;
        }
        /// <summary>
        /// thay đổi trạng thái Status của user
        /// </summary>
        /// <param Name="username">tên user</param>
        /// <param Name="state">trạng thái muốn đặt lại</param>
        public static void SetStatus(string username, bool state)
        {
            foreach (UserSession us in UserSessions)
            {
                if (us.username == username)
                {
                    us.isOnline = state;
                }
            }
        }
        /// <summary>
        /// thay đổi ip của user
        /// </summary>
        /// <param Name="username">tên user</param>
        /// <param Name="ip">đặt lai ip</param>
        public static void SetIP(string username, string ip)
        {
            foreach (UserSession us in UserSessions)
            {
                if (us.username == username)
                {
                    us.realtimeIP = ip;
                }
            }
        }
        /// <summary>
        /// Lấy tên user thông qua địa chỉ
        /// </summary>
        /// <param Name="ep">địa chỉ</param>
        /// <returns>tên user</returns>
        public string GetUsername(EndPoint ep)
        {
            foreach (UserSession us in UserSessions)
            {
                if (us.realtimeIP == ep.ToString())
                {
                    return us.username;
                }
            }
            return "";
        }
        /// <summary>
        /// thay đổi writer và reader
        /// </summary>
        /// <param Name="username"></param>
        /// <param Name="writer"></param>
        /// <param Name="reader"></param>
        public void SetWriterAndReader(string username, BinaryWriter writer, BinaryReader reader)
        {
            foreach (UserSession us in UserSessions)
            {
                if (us.username == username)
                {
                    us.writer = writer;
                    us.reader = reader;
                }
            }
        }

        public void Disconnect(string username)
        {   
            foreach (UserSession us in UserSessions)
            {
                if (us.username == username)
                {
                    SetIP(username, "empty");
                    SetWriterAndReader(username, null, null);
                    SetStatus(username, false);
                }
            }
        }
        public static void ChangePassword(string username, string newPassword)
        {
            foreach (UserSession us in UserSessions)
            {
                if (us.username == username)
                {
                    us.password = newPassword;
                }
            }
        }
    }
}
