using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using NTierApp.Core.DTOs;
using NTierApp.Core.Entities;
using NTierApp.Core.Repositories;
using NTierApp.Core.ResultPattern;
using NTierApp.Core.Services;
using NTierApp.Core.UnitOfWorks;
using NTierApp.Service.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NTierApp.Caching.Services.ProductCache
{
    public class ProductServiceWithCaching : IProductService
    {
        private const string CacheProductKey = "productsCache";
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductServiceWithCaching(IMapper mapper, IMemoryCache memoryCache, IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _memoryCache = memoryCache;
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;

            if (!_memoryCache.TryGetValue(CacheProductKey, out _))
            {
                //memory'de data yoksa datanın uygulama ayağa kaldırıldığında eklenmesi gerekmektedir.
                _memoryCache.Set(CacheProductKey, _productRepository.GetProductsWithCategory().Result);
            }

        }

        public async Task<Product> AddAsync(Product dto)
        {
            await _productRepository.AddAsync(dto);
            await _unitOfWork.CommitAsync();
            await CacheAllProductsAsync();
            return dto;

        }

        public async Task<IEnumerable<Product>> AddRangeAsnyc(IEnumerable<Product> dtos)
        {
            await _productRepository.AddRangeAsnyc(dtos);
            await _unitOfWork.CommitAsync();
            await CacheAllProductsAsync();
            return dtos;
        }

        public async Task<bool> AnyAsync(Expression<Func<Product, bool>> expression)
        {
            return await _productRepository.AnyAsync(expression);
        }

        public async Task DeleteAsync(Product dto)
        {
            _productRepository.Delete(dto);
            await _unitOfWork.CommitAsync();
            await CacheAllProductsAsync();
            
        }

        public async Task DeleteRangeAsync(IEnumerable<Product> dtos)
        {
            _productRepository.DeleteRange(dtos);
            await _unitOfWork.CommitAsync();
            await CacheAllProductsAsync();

        }

        public Task<IEnumerable<Product>> GetAllAsync()
        {
            return Task.FromResult(_memoryCache.Get<IEnumerable<Product>>(CacheProductKey));
        }

        public Task<Product> GetByIdAsync(int id)
        {
            var product = _memoryCache.Get<List<Product>>(CacheProductKey).FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                throw new NotFoundException($"{typeof(Product).Name}({id}) bulunamadı");
            }
            return Task.FromResult(product);
        }

        public Task<CustomResponseDto<List<ProductWithCategoriesDto>>> GetProductsWithCategory()
        {
            var result = _memoryCache.Get <IEnumerable<Product>>(CacheProductKey);
            var productsWithCategoryDto = _mapper.Map<List<ProductWithCategoriesDto>>(result);
            return Task.FromResult(CustomResponseDto<List<ProductWithCategoriesDto>>.Success(200, productsWithCategoryDto));
        }

        public async Task UpdateAsync(Product dto)
        {
            _productRepository.Update(dto);
            await _unitOfWork.CommitAsync();
            await CacheAllProductsAsync();

        }

        public IQueryable<Product> Where(Expression<Func<Product, bool>> expression)
        {
            return _memoryCache.Get<List<Product>>(CacheProductKey).Where(expression.Compile()).AsQueryable();
        }
        public async Task CacheAllProductsAsync()
        {
            _memoryCache.Set(CacheProductKey, await _productRepository.GetAll().ToListAsync());
        }
    }
}
