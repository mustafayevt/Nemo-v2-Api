using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Nemo_v2_Data.Entities;

namespace Nemo_v2_Repo.Abstraction
{
    public interface IRepository<TEntity>
	{
		List<TEntity> Get(
			Expression<Func<TEntity, bool>> filter = null,
			Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
			params Expression<Func<TEntity, object>>[] includes);
 
		/// <summary>
		/// Get query for entity
		/// </summary>
		/// <param name="filter"></param>
		/// <param name="orderBy"></param>
		/// <returns></returns>
		IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null);
 
		/// <summary>
		/// Get single entity by primary key
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		TEntity GetById(object id);
 
		/// <summary>
		/// Get first or default entity by filter
		/// </summary>
		/// <param name="filter"></param>
		/// <param name="includes"></param>
		/// <returns></returns>
		TEntity GetFirstOrDefault(
			Expression<Func<TEntity, bool>> filter = null,
			params Expression<Func<TEntity, object>>[] includes);
 
		/// <summary>
		/// Insert entity to db
		/// </summary>
		/// <param name="entity"></param>
		TEntity Insert(TEntity entity);
 
		/// <summary>
		/// Update entity in db
		/// </summary>
		/// <param name="entity"></param>
		TEntity Update(TEntity entity);
 
		/// <summary>
		/// Delete entity from db by primary key
		/// </summary>
		/// <param name="id"></param>
		void Delete(object id);
    }
}