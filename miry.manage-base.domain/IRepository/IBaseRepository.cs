using System;
using System.Linq.Expressions;
using miry.manage_base.domain.Models;

namespace miry.manage_base.domain.IRepository
{
	public interface IBaseRepository<TEntity> where TEntity : BaseEntity
	{
        bool SaveChanges();

        Task AddAsync(TEntity entity);

        IQueryable<TEntity> GetEntities(Expression<Func<TEntity, bool>>? filter);

        Task<TEntity?> GetEntity(Guid id);

        Task RemoveEntity(Guid id);

        Task Update(TEntity entity);

    }
}

