using System.Collections.Generic;
using Nemo_v2_Data.Entities;

namespace Nemo_v2_Service.Abstraction
{
    public interface IRoleService
    {
        IEnumerable<Role> Get();  
        IEnumerable<Role> GetRolesByRestaurantId(long RestId);  
        Role GetRole(long id);  
        Role InsertRole(Role role);  
        Role UpdateRole(Role role);  
        void DeleteRole(long id);  
    }
}