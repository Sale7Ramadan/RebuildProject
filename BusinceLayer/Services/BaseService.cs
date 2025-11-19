using AutoMapper;
using BusinceLayer.Interfaces;
using DataAccessLayer.Repositories;
using DataAccessLayer.Repositries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinceLayer.Services
{
    public class BaseService<TEntity, TDto, TCreateDto, TUpdateDto> : 
        IBaseService<TEntity, TDto, TCreateDto, TUpdateDto> where TEntity : class where TDto : class
    {
        protected readonly IBaseRepositories<TEntity> _repository;
        protected readonly IMapper _mapper;

        public BaseService(IBaseRepositories<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public virtual async Task<TDto> AddAsync(TCreateDto createDto)
        {
            var entity = _mapper.Map<TEntity>(createDto);
            var newEntity = await _repository.AddAsync(entity);
            return _mapper.Map<TDto>(newEntity);
        }

        public virtual async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public virtual async Task<IEnumerable<TDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<TDto>>(entities);
        }

        public virtual async Task<TDto?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                return null;
            return _mapper.Map<TDto>(entity);
        }

        public virtual async Task<bool> UpdateAsync(int id, TUpdateDto updateDto)
        {
            var existingEntity = await _repository.GetByIdAsync(id);
            if (existingEntity == null)
                return false;

            _mapper.Map(updateDto, existingEntity);
              await _repository.UpdateAsync(existingEntity);
            return true;
        }
        public virtual async Task<IEnumerable<TDto>> GetAllWithIncludeAsync(
    params Expression<Func<TEntity, object>>[] includes)
        {
            var entities = await _repository.GetAllWithIncludeAsync(includes);
            return _mapper.Map<IEnumerable<TDto>>(entities);
        }

    }
}
