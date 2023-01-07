
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
    public class CategoryService : Service<Category>, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, IMapper mapper) : base(categoryRepository, unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }



        public async Task<CustomResponseDto<CategoryWithProductsDto>> GetSingleCategoryByIdWidthProductsAsync(int categoryId)
        {
            var categories = _categoryRepository.GetSingleCategoryByIdWidthProductsAsync(categoryId);
            var dtos = _mapper.Map<CategoryWithProductsDto>(categories);
            return CustomResponseDto<CategoryWithProductsDto>.Success(200, dtos);
        }
    }
}
