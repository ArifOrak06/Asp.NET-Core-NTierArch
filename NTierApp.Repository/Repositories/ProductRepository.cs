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
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly AppDbContext _context;
       
        public ProductRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        // Product'ın bağlı olduğu categoriler ile birlikte listelenmesi
        public async Task<List<Product>> GetProductsWithCategory()
        {
            // Eager Loading. (Data çekilirken Category datasınıda almasını istedik.)
            return await _context.Products.Include(x => x.Category).ToListAsync();
        }
    }
}
