using System;
using System.Linq.Expressions;
using AutoMapper;
using miry.manage_base.domain.IRepository;
using miry.manage_base.domain.Models;
using miry.manage_base.service.IService;

namespace miry.manage_base.service.Service
{
    public class BaseService<TEntity, TReadDto, TCreateDto, TUpdateDto, TRepository> : IBaseService<TEntity, TReadDto, TCreateDto, TUpdateDto> where TRepository : IBaseRepository<TEntity> where TEntity: BaseEntity
    {
        protected readonly TRepository _repository;
        protected readonly IMapper _mapper;

        public BaseService(TRepository repository, IMapper mapper)
		{
            _repository = repository;
            _mapper = mapper;

        }

        public IQueryable<TReadDto> GetData(Expression<Func<TEntity, bool>>? filter)
        {
            var results = _repository.GetEntities(filter);
            return _mapper.Map<IQueryable<TReadDto>>(results);
        }

        public async Task<TReadDto> GetDataById(Guid id)
        {
            try
            {
                var entity = await _repository.GetEntity(id);
                if (entity == null)
                {
                    throw new ArgumentNullException();
                }
                return _mapper.Map<TReadDto>(entity);
            }
            catch { throw; }
        }

        public async Task<bool> Remove(Guid id)
        {
            try
            {
                await _repository.RemoveEntity(id);
                return _repository.SaveChanges();
            }
            catch { throw; }
        }

        public async Task<TReadDto> Save(TCreateDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            await _repository.AddAsync(entity);
            if (_repository.SaveChanges())
            {
                return _mapper.Map<TReadDto>(entity);
            }
            else
            {
                throw new ArgumentException($"Error occurs while creating entity { nameof(TEntity) }");
            }
        }

        public async Task<bool> Update(TUpdateDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            await _repository.Update(entity);
            return _repository.SaveChanges();
        }

    }
}

