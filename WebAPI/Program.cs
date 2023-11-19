using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EfCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IUserService, UserManager>();
builder.Services.AddTransient<IUserDal, EfCoreUserDal>();

builder.Services.AddTransient<IProductService, ProductManager>();
builder.Services.AddTransient<IProductDal, EfCoreProductDal>();

builder.Services.AddTransient<IProductCategoryService, ProductCategoryManager>();
builder.Services.AddTransient<IProductCategoryDal, EfCoreProductCategoryDal>();

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
