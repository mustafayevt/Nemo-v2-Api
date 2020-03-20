using System;
using System.Collections.Generic;
using Nemo_v2_Data.Entities;
using Nemo_v2_Repo.Abstraction;
using Nemo_v2_Service.Abstraction;

namespace Nemo_v2_Service.Services
{
    public class UserService:IUserService  
    {  
        private IRepository<User> _userRepository;  
        private IRepository<Role> _roleRepository;  
        private IRepository<UserRole> _userRoleRepository;  
  
        public UserService(IRepository<User> userRepository,
            IRepository<Role> roleRepository,
            IRepository<UserRole> userRoleRepository)  
        {  
            this._userRepository = userRepository;
            this._roleRepository = roleRepository;
            this._userRoleRepository = userRoleRepository;
        }  
  
        public IEnumerable<User> GetUsers()  
        {  
            return _userRepository.GetAll();  
        }  
  
        public User GetUser(long id)  
        {  
            return _userRepository.Get(id);  
        }  
  
        public User InsertUser(User user)  
        {  
            return _userRepository.Insert(user);  
        }  
        public User UpdateUser(User user)  
        {
            try
            {
                return _userRepository.Update(user);
            }
            catch (Exception e)
            {
                throw e;
            }
        }  
  
        public void DeleteUser(long id)  
        {              
            // UserProfile userProfile = userProfileRepository.Get(id);  
            // userProfileRepository.Remove(userProfile);  
            User user = GetUser(id);  
            _userRepository.Remove(user);  
            _userRepository.SaveChanges();  
        }  
    }  
}