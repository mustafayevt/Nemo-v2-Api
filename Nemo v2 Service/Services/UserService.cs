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

        public User GetUsersByRestaurantIdAndPassword(long RestId, string Password)
        {
            return _userRepository.Query(user => user.RestaurantId == RestId && user.Password == Password)
                .Include(user => user.UserRoles)
                .ThenInclude(role => role.Role).First();
        }

        public User GetUser(long id)
        {
            return _userRepository.Query(user => user.Id == id)
                .Include(user => user.UserRoles)
                .ThenInclude(role => role.Role).First();
        }

        public User InsertUser(User user)
        {
            if (user.UserRoles?.Any() ?? false)
            {
                if (user.UserRoles.Any(x => x.Role.Id == 0))
                {
                    IEnumerable<Role> newRoles;
                    newRoles = _roleRepository
                        .InsertMany(user.UserRoles
                            .Where(x => x.Role.Id == 0)
                            .Select(x => x.Role)).ToList();

                    user.UserRoles.RemoveAll(x => x.Role.Id == 0);
                }

                user.UserRoles.ForEach(x =>
                {
                    x.UserId = user.Id;
                    x.RoleId = x.Role.Id;
                    x.Role = null;
                });
            }

            return _userRepository.Insert(user);
        }

        public User UpdateUser(User user)
        {
            if (user.UserRoles?.Any() ?? false)
            {
                if (user.UserRoles.Any(x => x.Role.Id == 0))
                {
                    IEnumerable<Role> newRoles;
                    newRoles = _roleRepository
                        .InsertMany(user.UserRoles
                            .Where(x => x.Role.Id == 0)
                            .Select(x => x.Role)).ToList();

                    user.UserRoles.RemoveAll(x => x.Role.Id == 0);
                }

                user.UserRoles.ForEach(x =>
                {
                    x.UserId = user.Id;
                    x.RoleId = x.Role.Id;
                    x.Role = null;
                });
            }

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