using AutoMapper;
using NTierApp.Core.DTOs;
using NTierApp.Core.Entities;
using NTierApp.Core.Repositories;
using NTierApp.Core.ResultPattern;
using NTierApp.Core.Services;
using NTierApp.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierApp.Service.Services
{
    public class ProductService : Service<Product>, IProductService
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IProductRepository repository,IUnitOfWork unitOfWork, IMapper mapper): base(repository, unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = repository;
        }
        public async Task<CustomResponseDto<List<ProductWithCategoriesDto>>> GetProductsWithCategory()
        {
            var products = await _repository.GetProductsWithCategory();
            var productWithCategoriesDto = _mapper.Map<List<ProductWithCategoriesDto>>(products);
            return CustomResponseDto<List<ProductWithCategoriesDto>>.Success(200, productWithCategoriesDto);
        }
    }
}
