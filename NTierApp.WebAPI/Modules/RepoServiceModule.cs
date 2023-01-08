

using Autofac;
using NTierApp.Caching.Services.ProductCache;
using NTierApp.Core.Repositories;
using NTierApp.Core.Services;
using NTierApp.Core.UnitOfWorks;
using NTierApp.Repository.Contexts;
using NTierApp.Repository.Repositories;
using NTierApp.Repository.UnitOfWorks;
using NTierApp.Service.Mappings.AutoMapper;
using NTierApp.Service.Services;
using System.Reflection;
using Module = Autofac.Module;
namespace NTierApp.WebAPI.Modules
{
    public class RepoServiceModule  : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            // Generics register (Repository ve Service)
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(Service<>)).As(typeof(IService<>)).InstancePerLifetimeScope();

            //UnitOfWork Pattern
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();



            var apiAssembly = Assembly.GetExecutingAssembly();
            var repoAssembly = Assembly.GetAssembly(typeof(AppDbContext));
            var serviceAssembly = Assembly.GetAssembly(typeof(MapProfile));


            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly).Where(x => x.Name.EndsWith("Repository"))
                                                                                     .AsImplementedInterfaces()
                                                                                     .InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly).Where(x=> x.Name.EndsWith("Service"))
                                                                                     .AsImplementedInterfaces()
                                                                                     .InstancePerLifetimeScope();

            // IProductService istenildiğinde ProductServiceWithCaching çalışması gerekmektedir.

            builder.RegisterType<ProductServiceWithCaching>().As<IProductService>();
        }

    }
}
