using chatapp.common.Class;
using chatapp.dto.request;
using chatapp.model;
using chatapp.repository;
using chatapp.util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace chatapp.service.managelist
{
    /// <summary>
    /// quản lý danh sách usersession trong phiên làm việc của server
    /// </summary>
    internal class ManageUserSessionList
    {
        public List<UserSession> usersessionlist { get; set; }
        public ManageUserSessionList(List<UserSession> userlist)
        {
            this.usersessionlist = userlist;
        }

        public ManageUserSessionList()
        {
            //this.usersessionlist =ConvertUtils.UserListToUserSessionList(UserRepository.GetAllUser());
        }
        /// <summary>
        /// Thêm 1 usersession
        /// </summary>
        /// <param Name="usersession"></param>
        public void AddUser(UserSession usersession)
        {
            this.usersessionlist.Add(usersession);
        }
        /// <summary>
        /// thay đổi trạng thái Status của user
        /// </summary>
        /// <param Name="username">tên user</param>
        /// <param Name="state">trạng thái muốn đặt lại</param>
        public void SetState(string username,bool state)
        {
            foreach(UserSession us in usersessionlist)
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
        public void SetIP(string username, string ip)
        {
            foreach (UserSession us in usersessionlist)
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
            foreach(UserSession us in usersessionlist)
            {
                if(us.realtimeIP == ep.ToString())
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
        public void SetWriterAndReader(string username,StreamWriter writer,StreamReader reader)
        {
            foreach(UserSession us in usersessionlist)
            {
                if(us.username == username)
                {
                    us.writer = writer;
                    us.reader = reader;
                }
            }
        }
        
        public void Disconnect(string username)
        {
            foreach(UserSession us in usersessionlist)
            {
                if(us.username == username)
                {
                    SetIP(username, "empty");
                    SetWriterAndReader(username, null, null);
                    SetState(username, false);
                }
            }
        }
    }
}
