using Microsoft.EntityFrameworkCore;
using NTierApp.Core.Repositories;
using NTierApp.Core.Services;
using NTierApp.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NTierApp.Service.Services
{
    public class Service<T> : IService<T> where T : class
    {
        private readonly IRepository<T> _repository;
        private readonly IUnitOfWork _unitOfWork;
        public Service(IRepository<T> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        public async Task<T> AddAsync(T dto)
        {
            // API katmanında ActionFilters kullanıldığı için validasyon kontrolü yapılmadı
            await _repository.AddAsync(dto);
            await _unitOfWork.CommitAsync(); 
            return dto;
        }

        public async Task<IEnumerable<T>> AddRangeAsnyc(IEnumerable<T> dtos)
        {
            await _repository.AddRangeAsnyc(dtos);
            await _unitOfWork.CommitAsync();
            return dtos;
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return await _repository.AnyAsync(expression);
        }

        public async Task DeleteAsync(T dto)
        {
            _repository.Delete(dto);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteRangeAsync(IEnumerable<T> dtos)
        {
            _repository.DeleteRange(dtos);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _repository.GetAll().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(T dto)
        {
            _repository.Update(dto);
            await _unitOfWork.CommitAsync();
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return _repository.Where(expression);
        }
    }
}
