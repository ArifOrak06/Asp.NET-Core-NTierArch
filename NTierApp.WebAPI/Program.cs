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
using NTierApp.WebAPI.ValidationFilters;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(opt => 
{
    opt.Filters.Add(new ValidateFilterAttribute()); // filter global olarak b�t�n controller class'lar�na eklendi.Ayr� ayr� tan�mlanmas�na gerek yok !
    
}).AddFluentValidation(x=> x.RegisterValidatorsFromAssemblyContaining<ProductDtoValidator>());

builder.Services.Configure<ApiBehaviorOptions>(opt =>
{
    opt.SuppressModelStateInvalidFilter = true; //API'nin fluentValidation error modelini pasif hale getirdik kendi filter�m�z�n �al��mas� i�in.
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));
builder.Services.AddAutoMapper(typeof(MapProfile));

//CustomService
builder.Services.AddScoped<IProductRepository,ProductRepository>();
builder.Services.AddScoped<IProductService,ProductService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
// context

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), options =>
    {
        // options.MigrationsAssembly("NLayer.Repository") // Tip G�vensiz
        // Context class�m�z�n bulundu�u Assembly'i (NLayer.Repository) tip g�venli olarak bildirdik.
        // AppDbContext'in bulundu�u ASsembly'nin Name'i  = NLayer.Repository
        options.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
