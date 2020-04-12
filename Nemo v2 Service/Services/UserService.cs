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
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<User> GetUsers()
        {
            return _unitOfWork.UserRepository.Get();
        }

        public IEnumerable<User> GetUsersByRestaurantId(long RestId)
        {
            return _unitOfWork.UserRepository.Query(user => user.RestaurantId == RestId)
                .Include(user => user.UserRoles)
                .ThenInclude(role => role.Role);
        }

        public User GetUsersByRestaurantIdAndPassword(long RestId, string Password)
        {
            return _unitOfWork.UserRepository.Query(user => user.RestaurantId == RestId && user.Password == Password)
                .Include(user => user.UserRoles)
                .ThenInclude(role => role.Role).First();
        }

        public User GetUser(long id)
        {
            return _unitOfWork.UserRepository.Query(user => user.Id == id)
                .Include(user => user.UserRoles)
                .ThenInclude(role => role.Role).First();
        }

        public User InsertUser(User user)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                if (user.UserRoles?.Any() ?? false)
                {
                    if (user.UserRoles.Any(x => x.Role.Id == 0))
                    {
                        IEnumerable<Role> newRoles;
                        newRoles = _unitOfWork.RoleRepository
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

                var result = _unitOfWork.UserRepository.Insert(user);
                _unitOfWork.Save();
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception e)
            {
                _unitOfWork.Rollback();
                throw ;
            }
        }

        public User UpdateUser(User user)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                if (user.UserRoles?.Any() ?? false)
                {
                    if (user.UserRoles.Any(x => x.Role.Id == 0))
                    {
                        IEnumerable<Role> newRoles;
                        newRoles = _unitOfWork.RoleRepository
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

                var result = _unitOfWork.UserRepository.Update(user);
                _unitOfWork.Save();
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception e)
            {
                _unitOfWork.Rollback();
                throw ;
            }
        }

        public void DeleteUser(long id)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                _unitOfWork.UserRepository.Delete(id);
                _unitOfWork.Save();
                _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                _unitOfWork.Rollback();
                throw ;
            }
        }
    }
}