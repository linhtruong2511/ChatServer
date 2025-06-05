using chatapp.model;
using chatapp.repository;
using System.Collections.Generic;

namespace chatapp.service
{
    public class UserService
    {
        private UserRepository UserRepository;
        public UserService() {
            UserRepository = new UserRepository();
        }
        public bool Login(string username, string password)
        {
            bool userLoginSuccess = UserRepository.CheckPasswordUser(username, password);
            if (userLoginSuccess) 
                return true;
            return false;

        }
        public void UpdateUserStatusInDB(string username,bool status)
        {
            UserRepository.SetStatus(username, status);
        }
        public void UpdateUserIPInDB(string username,string ip)
        {
            UserRepository.SetRealTimeIP(username, ip);
        }
        
        public List<User> GetAllUser()
        {
            return UserRepository.GetAllUser();
        }
        public void ResetAllUserStatus()
        {
            UserRepository.SetAllUserStatusToFalse();
        }
        public void ResetAllUserIP()
        {
            UserRepository.SetAllUserIPToEmpty();
        }
    }
}
