using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
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
            return _userRepository.Get();
        }

        public IEnumerable<User> GetUsersByRestaurantId(long RestId)
        {
            return _userRepository.Query(user => user.RestaurantId==RestId)
                .Include(user => user.UserRoles)
                .ThenInclude(role => role.Role);
        }

        public User GetUser(long id)
        {
            return _userRepository.Query(user => user.Id == id)
                .Include(user => user.UserRoles)
                .ThenInclude(role => role.Role).First();
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
            _userRepository.Delete(user);
        }  
    }  
}