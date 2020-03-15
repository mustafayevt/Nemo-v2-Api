using System.Collections.Generic;
using Nemo_v2_Data.Entities;
using Nemo_v2_Repo.Abstraction;
using Nemo_v2_Service.Abstraction;

namespace Nemo_v2_Service.Services
{
    public class UserService:IUserService  
    {  
        private IRepository<User> userRepository;  
  
        public UserService(IRepository<User> userRepository)  
        {  
            this.userRepository = userRepository;  
        }  
  
        public IEnumerable<User> GetUsers()  
        {  
            return userRepository.GetAll();  
        }  
  
        public User GetUser(long id)  
        {  
            return userRepository.Get(id);  
        }  
  
        public void InsertUser(User user)  
        {  
            userRepository.Insert(user);  
        }  
        public void UpdateUser(User user)  
        {  
            userRepository.Update(user);  
        }  
  
        public void DeleteUser(long id)  
        {              
            // UserProfile userProfile = userProfileRepository.Get(id);  
            // userProfileRepository.Remove(userProfile);  
            User user = GetUser(id);  
            userRepository.Remove(user);  
            userRepository.SaveChanges();  
        }  
    }  
}