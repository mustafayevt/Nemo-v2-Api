using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Nemo_v2_Data.Entities;
using Nemo_v2_Repo.Abstraction;
using Nemo_v2_Repo.DbContexts;

namespace Nemo_v2_Repo.Repositories.EFRepository
{
    public class EFRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        protected DbContext context;
        protected DbSet<TEntity> dbSet;

        public EFRepository(ApplicationContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual List<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            params Expression<Func<TEntity, object>>[] includes)
        {
            try
            {
                IQueryable<TEntity> query = dbSet;

                foreach (Expression<Func<TEntity, object>> include in includes)
                    query = query.Include(include);

                if (filter != null)
                    query = query.Where(filter);

                if (orderBy != null)
                    query = orderBy(query);

                return query.ToList();
            }
            catch (Exception e)
            {
                throw new NullReferenceException($"{typeof(TEntity).Name} Not Found");
            }
        }

        public virtual IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            try
            {
                IQueryable<TEntity> query = dbSet;

                if (filter != null)
                    query = query.Where(filter);

                if (orderBy != null)
                    query = orderBy(query);

                if (!query.Any()) throw new NullReferenceException($"{typeof(TEntity).Name} Not Found");
                return query;
            }
            catch (Exception e)
            {
                throw new NullReferenceException($"{typeof(TEntity).Name} Not Found");
            }
        }

        public virtual TEntity GetById(object id)
        {
            try
            {
                return dbSet.Find(id);
            }
            catch (Exception e)
            {
                throw new NullReferenceException($"{typeof(TEntity).Name} Not Found");
            }
        }

        public virtual TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> filter = null,
            params Expression<Func<TEntity, object>>[] includes)
        {
            try
            {
                IQueryable<TEntity> query = dbSet;

                foreach (Expression<Func<TEntity, object>> include in includes)
                    query = query.Include(include);

                return query.FirstOrDefault(filter);
            }
            catch (Exception e)
            {
                throw new NullReferenceException($"{typeof(TEntity).Name} Not Found");
            }
        }

        public virtual TEntity Insert(TEntity entity)
        {
            try
            {
                // entity.AddedDate = DateTime.Now;
                // entity.ModifiedDate = DateTime.Now;
                var result = dbSet.Add(entity);
                context.SaveChanges();
                return result.Entity;
            }
            catch (Exception e)
            {
                if (e.InnerException != null) throw e.InnerException;
                throw;
            }
        }

        public IEnumerable<TEntity> InsertMany(IEnumerable<TEntity> entities)
        {
            try
            {
                var addedEntities = new List<TEntity>();
                foreach (var entity in entities)
                {
                    // entity.AddedDate = DateTime.Now;
                    // entity.ModifiedDate = DateTime.Now;
                    addedEntities.Add(dbSet.Add(entity).Entity);
                }
                context.SaveChanges();
                return addedEntities;
            }
            catch (Exception e)
            {
                if (e.InnerException != null) throw e.InnerException;
                throw;
            }
        }

        public virtual TEntity Update(TEntity entity, string[] notUpdateProperties)
        {
            try
            {
                // entity.ModifiedDate = DateTime.Now;
                var oldEntity = context.Set<TEntity>().First(g => g.Id == entity.Id);
                 entity.AddedDate = oldEntity.AddedDate;
                context.Entry(oldEntity).CurrentValues.SetValues(entity);

                context.SaveChanges();
                return context.Entry(oldEntity).Entity;
            }
            catch (Exception e)
            {
                if (e.InnerException != null) throw e.InnerException;
                throw;
            }
        }

        public virtual void Delete(object id)
        {
            try
            {
                TEntity entityToDelete = dbSet.Find(id);
                if (context.Entry(entityToDelete).State == EntityState.Detached)
                {
                    dbSet.Attach(entityToDelete);
                }

                dbSet.Remove(entityToDelete);
            }
            catch (Exception e)
            {
                if (e.InnerException != null) throw e.InnerException;
                throw;
            }
        }
    }
}