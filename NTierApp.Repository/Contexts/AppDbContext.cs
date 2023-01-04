using Microsoft.EntityFrameworkCore;
using NTierApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NTierApp.Repository.Contexts
{
    public class AppDbContext : DbContext
    {
        // Database source program.cs'de tanımlanabilmesi için
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductFeature> ProductFeatures { get; set; }
        
        protected  override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Configuration classları Assembly olarak tanımlandı.
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }

    }
}
