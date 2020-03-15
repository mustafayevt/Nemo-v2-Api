using System.Collections.Generic;
using Nemo_v2_Data.Entities;

namespace Nemo_v2_Service.Abstraction
{
    public  interface IUserService  
    {  
        IEnumerable<User> GetUsers();  
        User GetUser(long id);  
        void InsertUser(User user);  
        void UpdateUser(User user);  
        void DeleteUser(long id);  
    }  
}