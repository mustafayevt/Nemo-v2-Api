using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Nemo_v2_Data.Entities;
using Nemo_v2_Repo.Abstraction;
using Nemo_v2_Service.Abstraction;

namespace Nemo_v2_Service.Services
{
    public class RoleService:IRoleService
    {
        private IRepository<User> _userRepository;  
        private IRepository<Role> _roleRepository;  
  
        public RoleService(IRepository<User> userRepository,
            IRepository<Role> roleRepository)  
        {  
            this._userRepository = userRepository;
            this._roleRepository = roleRepository;
        }  
        
        public IEnumerable<Role> Get()
        {
            return _roleRepository.Get();
        }

        public IEnumerable<Role> GetRolesByRestaurantId(long RestId)
        {
            return _roleRepository.Query(x => x.RestaurantId == RestId);
        }

        public Role GetRole(long id)
        {
            return _roleRepository.GetById(id);
        }

        public Role InsertRole(Role role)
        {
            return _roleRepository.Insert(role);
        }

        public Role UpdateRole(Role role)
        {
            return _roleRepository.Update(role);
        }

        public void DeleteRole(long id)
        {
            _roleRepository.Delete(id);
        }
    }
}