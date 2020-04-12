using System;
using System.Collections.Generic;
using Nemo_v2_Data.Entities;
using Nemo_v2_Repo.Abstraction;
using Nemo_v2_Service.Abstraction;

namespace Nemo_v2_Service.Services
{
    public class RoleService:IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Role> Get()
        {
            return _unitOfWork.RoleRepository.Get();
        }

        public IEnumerable<Role> GetRolesByRestaurantId(long RestId)
        {
            return _unitOfWork.RoleRepository.Query(x => x.RestaurantId == RestId);
        }

        public Role GetRole(long id)
        {
            return _unitOfWork.RoleRepository.GetById(id);
        }

        public Role InsertRole(Role role)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                var result = _unitOfWork.RoleRepository.Insert(role);
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

        public Role UpdateRole(Role role)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                var result = _unitOfWork.RoleRepository.Insert(role);
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

        public void DeleteRole(long id)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                _unitOfWork.RoleRepository.Delete(id);
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