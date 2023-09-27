using System;
using System.Linq.Expressions;

namespace miry.manage_base.service.IService
{
	public interface IBaseService<TEntity, TReadDto, TCreateDto, TUpdateDto>
	{
        IQueryable<TReadDto> GetData(Expression<Func<TEntity, bool>>? filter);

        Task<TReadDto> Save(TCreateDto dto);

        Task<TReadDto> GetDataById(Guid id);

        Task<bool> Update(TUpdateDto dto);

        Task<bool> Remove(Guid id);
    }
}

