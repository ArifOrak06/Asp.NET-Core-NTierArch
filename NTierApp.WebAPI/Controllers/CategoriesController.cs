using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NTierApp.Core.Services;

namespace NTierApp.WebAPI.Controllers
{
    public class CategoriesController : CustomBaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetSingleCategoryByIdWithProducts(int id)
        {
            return CreateActionResult(await _categoryService.GetSingleCategoryByIdWidthProductsAsync(id));
        }
    }
}
