using System.Collections.Generic;
using Nemo_v2_Data.Entities;

namespace Nemo_v2_Service.Abstraction
{
    public  interface IUserService  
    {  
        IEnumerable<User> GetUsers();  
        IEnumerable<User> GetUsersByRestaurantId(long RestId);  
        User GetUser(long id);  
        User InsertUser(User user);  
        User UpdateUser(User user);  
        void DeleteUser(long id);  
    }  
}