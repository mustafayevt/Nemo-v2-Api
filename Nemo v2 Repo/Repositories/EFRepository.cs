using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nemo_v2_Data.Entities;
using Nemo_v2_Repo.Abstraction;
using Nemo_v2_Repo.DbContexts;

namespace Nemo_v2_Repo.Repositories
{
    public class EFRepository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private DbContext context;
    private DbSet<TEntity> dbSet;
 
    public EFRepository(ApplicationContext context)
    {
        this.context = context;
        this.dbSet = context.Set<TEntity>();
    }
 
    public virtual List<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includes)
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
 
    public virtual IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
    {
        try
        {
            IQueryable<TEntity> query = dbSet;
 
            if (filter != null)
                query = query.Where(filter);
 
            if (orderBy != null)
                query = orderBy(query);
 
            if(!query.Any()) throw new NullReferenceException("User Not Found");
            return query;
        }
        catch (Exception e)
        {
            throw new NullReferenceException("User Not Found");
        }
    }
 
    public virtual TEntity GetById(object id)
    {
        return dbSet.Find(id);
    }
 
    public virtual TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = dbSet;
 
        foreach (Expression<Func<TEntity, object>> include in includes)
            query = query.Include(include);
 
        return query.FirstOrDefault(filter);
    }
 
    public virtual TEntity Insert(TEntity entity)
    {
        return dbSet.Add(entity).Entity;
    }
 
    public virtual TEntity Update(TEntity entity)
    {
        dbSet.Attach(entity);
        context.Entry(entity).State = EntityState.Modified;
        return context.Entry(entity).Entity;
    }
 
    public virtual void Delete(object id)
    {
        TEntity entityToDelete = dbSet.Find(id);
        if (context.Entry(entityToDelete).State == EntityState.Detached)
        {
            dbSet.Attach(entityToDelete);
        }
        dbSet.Remove(entityToDelete);
    }
}
}