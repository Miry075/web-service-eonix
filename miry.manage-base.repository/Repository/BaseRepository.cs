using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using miry.manage_base.domain.IRepository;
using miry.manage_base.domain.Models;

namespace miry.manage_base.repository.Repository
{
	public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity: BaseEntity
    {
        protected readonly DbContext _context;

        public BaseRepository(DbContext context)
		{
            _context = context;
		}

        public async Task AddAsync(TEntity entity)
        {
            if (entity == null) { throw new ArgumentNullException(); }

            await _context.Set<TEntity>().AddAsync(entity);
        }

        public IQueryable<TEntity> GetEntities(Expression<Func<TEntity, bool>>? filter)
        {
            var dbSet = _context.Set<TEntity>();
            if (filter == null)
            {
                return  dbSet.AsQueryable();
            }
            return dbSet.Where(filter).AsQueryable();
        }

        public async Task<TEntity?> GetEntity(Guid id)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(entity => entity.Id == id);
        }

        public async Task RemoveEntity(Guid id)
        {
           
            try
            {
                var inDb = await GetEntity(id);
                if (inDb == null)
                {
                    throw new ArgumentNullException("Not found in Db");
                }
                _context.Entry(inDb).State = EntityState.Deleted;
            }
            catch { throw; }
        }
      
        public async Task Update(TEntity entity)
        {
          
            var dbSet = _context.Set<TEntity>();
            try
            {
                var inDb = await GetEntity(entity.Id);
                if (inDb == null)
                {
                throw new ArgumentNullException("Not found in Db");
                }
                dbSet.Attach(entity);
                dbSet.Entry(entity).State = EntityState.Modified;
            }
            catch
            {
                throw;
            }
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }

      
    }
}

