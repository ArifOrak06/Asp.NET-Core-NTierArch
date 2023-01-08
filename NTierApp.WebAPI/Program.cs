using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NTierApp.Core.Repositories;
using NTierApp.Core.Services;
using NTierApp.Core.UnitOfWorks;
using NTierApp.Repository.Contexts;
using NTierApp.Repository.Repositories;
using NTierApp.Repository.UnitOfWorks;
using NTierApp.Service.Mappings.AutoMapper;
using NTierApp.Service.Services;
using NTierApp.Service.Validations;
using NTierApp.WebAPI.Filters;
using NTierApp.WebAPI.Middlewares;
using NTierApp.WebAPI.ValidationFilters;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(opt => 
{
    opt.Filters.Add(new ValidateFilterAttribute()); // filter global olarak bütün controller class'larýna eklendi.Ayrý ayrý tanýmlanmasýna gerek yok !
    
}).AddFluentValidation(x=> x.RegisterValidatorsFromAssemblyContaining<ProductDtoValidator>());

builder.Services.Configure<ApiBehaviorOptions>(opt =>
{
    opt.SuppressModelStateInvalidFilter = true; //API'nin fluentValidation error modelini pasif hale getirdik kendi filterýmýzýn çalýþmasý için.
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddScoped(typeof(NotFoundFilter<>));
builder.Services.AddAutoMapper(typeof(MapProfile));
 
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), options =>
    {
        // options.MigrationsAssembly("NLayer.Repository") // Tip Güvensiz
        // Context classýmýzýn bulunduðu Assembly'i (NLayer.Repository) tip güvenli olarak bildirdik.
        // AppDbContext'in bulunduðu ASsembly'nin Name'i  = NLayer.Repository
        options.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
    });
});

// autoFac 
builder.Host.UseServiceProviderFactory
    (new AutofacServiceProviderFactory());
// RepoModule'ün dahil edilmesi.
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterModule(new NTierApp.WebAPI.Modules.RepoServiceModule()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCustomException();

app.UseAuthorization();

app.MapControllers();

app.Run();
