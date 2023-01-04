using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierApp.Core.UnitOfWorks
{
    public interface IUnitOfWork
    {

        Task CommitAsync(); // Database'deki data konusundan nası aldıracağız.
        void Commit();
    }
}
