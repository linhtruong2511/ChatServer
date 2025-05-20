using chatapp.common.Class;
using chatapp.model;
using chatapp.repository;
using chatapp.service;
using chatapp.util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace chatapp.context
{
    internal class ManageSessionUser
    {
        public static List<UserSession> UserSessions { get; set; }
        public UserService UserService { get; set; }

        public ManageSessionUser() {
            UserSessions = new List<UserSession>();
            UserService = new UserService();
            UserSessions = ConvertUtils.UserListToUserSessionList(UserService.GetAllUser());
        }
        public static void AddUserSession(UserSession userSession) {
            UserSessions.Add(userSession);
        }

        public static void RemoveUserSession(UserSession userSession) {
            UserSessions.Remove(userSession);
        }

        public List<UserSession> GetAllUserSessions() {
            return UserSessions;
        }
        /// <summary>
        /// thay đổi trạng thái Status của user
        /// </summary>
        /// <param name="username">tên user</param>
        /// <param name="state">trạng thái muốn đặt lại</param>
        public void SetState(string username, bool state)
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
        /// <param name="username">tên user</param>
        /// <param name="ip">đặt lai ip</param>
        public void SetIP(string username, string ip)
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
        /// <param name="ep">địa chỉ</param>
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
        /// <param name="username"></param>
        /// <param name="writer"></param>
        /// <param name="reader"></param>
        public void SetWriterAndReader(string username, StreamWriter writer, StreamReader reader)
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
                    SetState(username, false);
                }
            }
        }
    }
}
