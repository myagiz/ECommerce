using Core.Repository.Concrete;
using Core.Utilities.Current;
using DataAccess.Abstract;
using DataAccess.Contexts;
using Entities.DTOs;
using Entities.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EfCore
{
    public class EfCoreProductDal : EfEntityRepository<Product, ECommerceDbContext>, IProductDal
    {
        public async Task CreateProductAsync(CreateProductDto model)
        {
            using (var context = new ECommerceDbContext())
            {
                Product entity = new Product();
                entity.CategoryId = model.CategoryId;
                entity.UnitPrice = model.UnitPrice;
                entity.Name = model.Name;
                entity.Amount = model.Amount;
                entity.CreateDate = DateTime.Now;
                entity.CreateUserId = UserCurrents.UserId();
                entity.IsActive = true;
                context.Products.Add(entity);
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteProductAsync(int productId)
        {
            using (var context = new ECommerceDbContext())
            {
                var getData = await context.Products.FirstOrDefaultAsync(x => x.IsActive == true && x.Id == productId);
                if (getData != null) { getData.IsActive = false; getData.UpdateDate = DateTime.Now; getData.UpdateUserId = UserCurrents.UserId(); await context.SaveChangesAsync(); }
            }
        }

        public async Task<List<GetAllProductDto>> GetAllProductsAsync()
        {
            using (var context = new ECommerceDbContext())
            {
                var data = await (from a in context.Products.Where(x => x.IsActive == true)
                                  //join b in context.ProductCategories on a.CategoryId equals b.Id
                                  select new GetAllProductDto
                                  {
                                      Id = a.Id,
                                      //CategoryId = a.CategoryId,
                                      //CategoryName = b.Name,
                                      Name = a.Name,
                                      Amount = a.Amount,
                                      UnitPrice = a.UnitPrice,
                                      CreateDate = a.CreateDate

                                  }).OrderByDescending(x => x.CreateDate).ToListAsync();
                return data;
            }
        }

        public async Task<GetProductByIdDto> GetProductById(int productId)
        {
            using (var context = new ECommerceDbContext())
            {
                var data = await (from a in context.Products.Where(x => x.IsActive == true && x.Id == productId)
                                  //join b in context.ProductCategories on a.CategoryId equals b.Id
                                  select new GetProductByIdDto
                                  {
                                      Id = a.Id,
                                      //CategoryId = a.CategoryId,
                                      //CategoryName = b.Name,
                                      Name = a.Name,
                                      Amount = a.Amount,
                                      UnitPrice = a.UnitPrice,
                                  }).FirstOrDefaultAsync();
                if (data != null)
                {
                    return data;
                }
                return null;
            }
        }

        public async Task UpdateProductAsync(UpdateProductDto model)
        {
            using (var context = new ECommerceDbContext())
            {
                var getData = await context.Products.FirstOrDefaultAsync(x => x.IsActive == true && x.Id == model.Id);
                if (getData != null)
                {
                    getData.CategoryId = model.CategoryId;
                    getData.UnitPrice = model.UnitPrice;
                    getData.Name = model.Name;
                    getData.Amount = model.Amount;
                    getData.UpdateDate = DateTime.Now;
                    getData.UpdateUserId = UserCurrents.UserId();
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
