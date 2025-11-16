using BusinceLayer.EntitiesDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinceLayer.Interfaces
{
    public interface IBaseService<TEntity, TDto, TCreateDto, TUpdateDto> where TEntity : class
    {
        Task<IEnumerable<TDto>> GetAllAsync();
        Task<TDto?> GetByIdAsync(int id);
        Task<TDto> AddAsync(TCreateDto createDto);
        Task<bool> UpdateAsync(int id, TUpdateDto updateDto);
        Task<bool> DeleteAsync(int id);
       

    }
}
