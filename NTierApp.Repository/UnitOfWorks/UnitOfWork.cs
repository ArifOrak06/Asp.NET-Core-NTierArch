using NTierApp.Core.UnitOfWorks;
using NTierApp.Repository.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierApp.Repository.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges(); 
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
