using chatapp.common;
using chatapp.common.Class;
using chatapp.context;
using chatapp.dto;
using chatapp.dto.request;
using chatapp.gui.event_;
using chatapp.model;
using chatapp.repository;
using chatapp.service.managelist;
using chatapp.service.userservice;
using chatapp.util;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace chatapp.service
{
    internal class UserService
    {
        private UserRepository userRepository;
        public UserService() {
            userRepository = new UserRepository();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="packet"></param>
        /// <param name="writer"></param>
        /// <param name="reader"></param>
        /// <param name="manageusersessionlist"></param>
        /// <returns></returns>
        public bool Login(string username, string password)
        {
            bool userLoginSucess = userRepository.CheckPasswordUser(username, password);
            if (userLoginSucess) return true;
            return false;

        }
        public static async Task<int> SignUp(Packet packet, StreamWriter writer,StreamReader reader,ManageUserSessionList manageusersessionlist,TcpClient tcpclient,App app,EventHandler serverevent)
        {
            LoginRequest user = ConvertUtils.PacketDataToDTO<LoginRequest>(packet);
            for (int i = 0; i < manageusersessionlist.usersessionlist.Count; i++)
            {
                if (manageusersessionlist.usersessionlist[i].username == user.username)
                {
                    return await SignUpCase.UsernameUsed(packet,writer,app,serverevent);
                }
            }
            return await SignUpCase.SuccessSignUp(packet, user, writer, reader, manageusersessionlist, tcpclient,app,serverevent);
        }
        //public static async Task<int> Disconnect(Packet packet, StreamWriter writer, ManageUserSessionList manageusersessionlist,App app,EventHandler serverevent)
        //{
        //    //for (int i = 0; i < manageusersessionlist.usersessionlist.Count; i++)
        //    //{
        //    //    if (manageusersessionlist.usersessionlist[i].username == packet.From)
        //    //    {
        //    //        //return await DisconnectCase.SuccessDisconnect(packet, writer,app,serverevent);
        //    //    }
        //    //}
        //    //return -1;
        //}
        public static async Task<int> Default(Packet packet, StreamWriter writer)
        {
            return await DefaultCase.IncorrectPacketFormat(packet, writer);
        }
        
        public List<User> getAllUser()
        {
            return userRepository.getAllUser();
        }
    }
}
