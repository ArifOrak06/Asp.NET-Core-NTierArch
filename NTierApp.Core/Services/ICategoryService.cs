using NTierApp.Core.DTOs;
using NTierApp.Core.Entities;
using NTierApp.Core.ResultPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierApp.Core.Services
{
    public interface ICategoryService : IService<Category>
    {
        Task<CustomResponseDto<CategoryWithProductsDto>> GetSingleCategoryByIdWidthProductsAsync(int categoryId);
    }
}
