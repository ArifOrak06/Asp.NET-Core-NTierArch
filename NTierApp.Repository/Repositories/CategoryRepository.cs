using Microsoft.EntityFrameworkCore;
using NTierApp.Core.Entities;
using NTierApp.Core.Repositories;
using NTierApp.Repository.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierApp.Repository.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Category> GetSingleCategoryByIdWidthProductsAsync(int categoryId)
        {
            // categorilere ait productları dahil edip, akabinde ıd'si parametre olarak gelen categoryId'ye eşit olan categorinin
            // productlarını dön.
            return await _context.Categories.Include(x => x.Products).Where(x => x.Id == categoryId).SingleOrDefaultAsync();
            
        }
    }
}
