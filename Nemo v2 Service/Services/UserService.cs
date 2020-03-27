using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Nemo_v2_Data.Entities;
using Nemo_v2_Repo.Abstraction;
using Nemo_v2_Service.Abstraction;

namespace Nemo_v2_Service.Services
{
    public class UserService : IUserService
    {
        private IRepository<User> _userRepository;
        private IRepository<Role> _roleRepository;

        public UserService(IRepository<User> userRepository,
            IRepository<Role> roleRepository)
        {
            this._userRepository = userRepository;
            this._roleRepository = roleRepository;
        }

        public IEnumerable<User> GetUsers()
        {
            return _userRepository.Get();
        }

        public IEnumerable<User> GetUsersByRestaurantId(long RestId)
        {
            return _userRepository.Query(user => user.RestaurantId == RestId)
                .Include(user => user.UserRoles)
                .ThenInclude(role => role.Role);
        }

        public User GetUser(long id)
        {
            return _userRepository.Query(user => user.Id == id)
                .Include(user => user.UserRoles)
                .ThenInclude(role => role.Role).First();
        }

        public User InsertUser(User user, IEnumerable<long> rolesId)
        {
            var roles = _roleRepository.Query(x => rolesId.Contains(x.Id)).ToList();
            var userRoles = new List<UserRole>();
            roles.ForEach(x=> userRoles.Add(new UserRole()
            {
                UserId = user.Id,
                RoleId = x.Id,
                //AddedDate = DateTime.Now,
                //ModifiedDate = DateTime.Now
            }));
            
            user.UserRoles = userRoles;
            return _userRepository.Insert(user);
        }

        public User UpdateUser(User user, IEnumerable<long> rolesId)
        {
            if(_userRepository.Query(x=>x.Id ==user.Id).AsNoTracking() ==null) throw new NullReferenceException("User Not Found");
            var roles = _roleRepository.Query(x => rolesId.Contains(x.Id)).ToList();
            var userRoles = new List<UserRole>();
            roles.ForEach(x=> userRoles.Add(new UserRole()
            {
                UserId = user.Id,
                RoleId = x.Id,
                Role = x,
                //AddedDate = DateTime.Now,
                //ModifiedDate = DateTime.Now
            }));
            user.UserRoles = userRoles.ToList();
            return _userRepository.Update(user);
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