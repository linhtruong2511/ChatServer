using chatapp.common.Class;
using chatapp.dto;
using chatapp.dto.request;
using chatapp.dto.response;
using chatapp.model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace chatapp.util
{
    internal class ConvertUtils
    {
        private ConvertUtils() { }
        /// <summary>
        /// chuyển từ packet sang Object
        /// </summary>
        /// <typeparam Name="T"></typeparam>
        /// <param Name="packet"></param>
        /// <returns>1 đối tượng của kiểu cần chuyển tới</returns>
        public static T PacketDataToDTO<T>(Packet packet)
        {
            return JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(packet.Data));
        }
        public static UserSession UserToUserSession(User user)
        {
            return new UserSession(user.ID,user.name,user.username,user.password,null,null,user.IP,user.Status);
        }
        /// <summary>
        /// chuyển từ list<UserSession> sang list<User>
        /// </summary>
        /// <param Name="userlist"></param>
        /// <returns>list<usersession></returns>
        public static List<UserSession> UserListToUserSessionList(List<User> users)
        {
            List<UserSession> userSessions = new List<UserSession>();
            foreach(User i in users)
            {
                userSessions.Add(ConvertUtils.UserToUserSession(i));
            }
            return userSessions;
        }
        public static List<UserResponse> UserListToUserResponseList(List<User> users)
        {
            List<UserResponse> userResponses = new List<UserResponse>();
            foreach(User i in users)
            {
                userResponses.Add(new UserResponse(i));
            }
            return userResponses;
        }
    }
}
