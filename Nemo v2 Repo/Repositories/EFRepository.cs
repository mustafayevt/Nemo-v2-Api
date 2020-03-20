using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Nemo_v2_Data.Entities;
using Nemo_v2_Repo.Abstraction;
using Nemo_v2_Repo.DbContexts;

namespace Nemo_v2_Repo.Repositories
{
    public class EFRepository<T> : IRepository<T> where T : BaseEntity  
    {  
        private readonly ApplicationContext context;  
        private DbSet<T> entities;  
        string errorMessage = string.Empty;  
  
        public EFRepository(ApplicationContext context)  
        {  
            this.context = context;  
            entities = context.Set<T>();  
        }  
        public IEnumerable<T> GetAll()  
        {  
            return entities.AsEnumerable();  
        }  
  
        public T Get(long id)  
        {  
            return entities.SingleOrDefault(s => s.Id == id);  
        }  
        public T Insert(T entity)  
        {  
            if (entity == null)  
            {  
                throw new ArgumentNullException("entity");  
            }

            try
            {
                var entityEntry = entities.Add(entity);  
                context.SaveChanges();
                return entityEntry.Entity;
            }
            catch (Exception e)
            {
                throw e.InnerException;
            }
        }  
  
        public T Update(T entity)  
        {  
            if (entity == null)  
            {  
                throw new ArgumentNullException("entity");  
            }

            try
            {
                context.SaveChanges();
                return entities.SingleOrDefault(x=>x.Id==entity.Id);
            }
            catch (Exception e)
            {
                throw e.InnerException;
            }
        }  
  
        public void Delete(T entity)  
        {  
            if (entity == null)  
            {  
                throw new ArgumentNullException("entity");  
            }  
            entities.Remove(entity);  
            context.SaveChanges();  
        }  
        public void Remove(T entity)  
        {  
            if (entity == null)  
            {  
                throw new ArgumentNullException("entity");  
            }  
            entities.Remove(entity);              
        }  
  
        public void SaveChanges()  
        {  
            context.SaveChanges();  
        }  
    }  
}