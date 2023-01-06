using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NTierApp.Core.Services
{
    public interface IService<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        IQueryable<T> Where(Expression<Func<T, bool>> expression); // filtreleme operasyonları
        Task<IEnumerable<T>> GetAllAsync();
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> AddRangeAsnyc(IEnumerable<T> dtos);
        Task<T> AddAsync(T dto);
        Task UpdateAsync(T dto);
        Task DeleteAsync(T dto);
        Task DeleteRangeAsync(IEnumerable<T> dtos);
    }
}
