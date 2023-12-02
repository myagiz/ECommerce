using Business.Abstract;
using Business.Concrete;
using Core.Utilities.Configs.Abstract;
using Core.Utilities.Configs.Concrete;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete.EfCore;
using DataAccess.Contexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<ECommerceDbContext>(options => {
    options.UseSqlServer(configuration.GetConnectionString("ECommerceDbConnection"));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IUserService, UserManager>();
builder.Services.AddTransient<IUserDal, EfCoreUserDal>();

builder.Services.AddTransient<IProductService, ProductManager>();
builder.Services.AddTransient<IProductDal, EfCoreProductDal>();

builder.Services.AddTransient<IProductCategoryService, ProductCategoryManager>();
builder.Services.AddTransient<IProductCategoryDal, EfCoreProductCategoryDal>();

builder.Services.AddTransient<IAuthService, AuthManager>();
builder.Services.AddTransient<IAuthDal, EfCoreAuthDal>();

builder.Services.AddTransient<IOrderService, OrderManager>();
builder.Services.AddTransient<IOrderDal, EfCoreOrderDal>();

builder.Services.AddTransient<ITokenService, TokenService>();
builder.Services.AddSingleton<IECommerceConfigService, ECommerceConfigManager>();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(option =>
{
    option.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = configuration["Token:Issuer"],
        ValidAudience = configuration["Token:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:SecurityKey"])),
        ClockSkew = TimeSpan.Zero
    };

});

builder.Services.AddSwaggerGen(c =>
{
    var securityScheme = new OpenApiSecurityScheme()
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT" // Optional
    };
    var securityRequirement = new OpenApiSecurityRequirement
                {
                   {
                       new OpenApiSecurityScheme
                       {
                       Reference = new OpenApiReference
                       {
                         Type = ReferenceType.SecurityScheme,
                         Id = JwtAuthenticationDefaults.AuthenticationScheme
                       }
                },
                new string[] {}
                }};
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ECommerce API", Version = "v1" });
    c.AddSecurityDefinition("bearerAuth", securityScheme);
    c.AddSecurityRequirement(securityRequirement);

});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ECommerce API");
    });

}

app.UseCors(options =>
  options.WithOrigins("http://localhost:4200")
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
