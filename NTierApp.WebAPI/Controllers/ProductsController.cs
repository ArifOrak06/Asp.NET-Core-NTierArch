using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NTierApp.Core.DTOs;
using NTierApp.Core.Entities;
using NTierApp.Core.ResultPattern;
using NTierApp.Core.Services;
using NTierApp.WebAPI.Filters;

namespace NTierApp.WebAPI.Controllers
{
 
    public class ProductsController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IProductService _service;

        public ProductsController(IMapper mapper, IProductService productService)
        {
            _mapper = mapper;

            _service = productService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetProductsWithCategories()
        {
            var dtos = await _service.GetProductsWithCategory();
            return CreateActionResult(dtos);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _service.GetAllAsync();
            var productsDtos = _mapper.Map<List<ProductDto>>(products.ToList());
            return CreateActionResult(CustomResponseDto<List<ProductDto>>.Success(200, productsDtos));
        }
        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _service.GetByIdAsync(id);
            var productDto = _mapper.Map<ProductDto>(product);
            return CreateActionResult(CustomResponseDto<ProductDto>.Success(200, productDto));
        }
        [HttpPost]
        public async Task<IActionResult> Add(ProductDto dto)
        {
            var product = _mapper.Map<Product>(dto);
            await _service.AddAsync(product);
            return CreateActionResult(CustomResponseDto<Product>.Success(201, product));
        }
        [HttpPut]
        public async Task<IActionResult> Update(ProductUpdateDto dto)
        {
            var product = _mapper.Map<Product>(dto);
            await _service.UpdateAsync(product);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
  
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var product = await _service.GetByIdAsync(id);
            await _service.DeleteAsync(product);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));

        }
    }
}
