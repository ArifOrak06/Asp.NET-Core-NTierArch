using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NTierApp.Core.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        IQueryable<T> Where(Expression<Func<T, bool>> expression); // filtreleme operasyonları
        IQueryable<T> GetAll(Expression<Func<T, bool>> expression);
        Task<bool> AnyAsync(Expression<Func<T, bool>> exporession);
        Task AddRangeAsnyc(IEnumerable<T> entities);
        Task AddAsync(T Entity);
        void Update(T Entity);
        void Delete(T Entity);
        void DeleteRange(IEnumerable<T> entities);



    }
}
