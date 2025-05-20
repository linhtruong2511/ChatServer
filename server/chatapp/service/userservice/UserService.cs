using chatapp.dto;
using chatapp.model;
using chatapp.repository;
using chatapp.service.userservice;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace chatapp.service
{
    internal class UserService
    {
        private UserRepository UserRepository;
        public UserService() {
            UserRepository = new UserRepository();
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
            bool userLoginSuccess = UserRepository.CheckPasswordUser(username, password);
            if (userLoginSuccess) 
                return true;
            return false;

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
        
        public List<User> GetAllUser()
        {
            return UserRepository.GetAllUser();
        }
    }
}
