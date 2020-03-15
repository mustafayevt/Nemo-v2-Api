using System.Collections.Generic;
using Nemo_v2_Data.Entities;

namespace Nemo_v2_Repo.Abstraction
{
    public interface IRepository<T> where T : BaseEntity  
    {  
        IEnumerable<T> GetAll();  
        T Get(long id);  
        void Insert(T entity);  
        void Update(T entity);  
        void Delete(T entity);  
        void Remove(T entity);  
        void SaveChanges();  
    } 
}