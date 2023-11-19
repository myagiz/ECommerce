using Core.Repository.Concrete;
using DataAccess.Abstract;
using Entities.DTOs;
using Entities.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EfCore
{
    public class EfCoreProductCategoryDal : EfEntityRepository<ProductCategory, ECommerceDbContext>, IProductCategoryDal
    {
        public async Task CreateProductCategoryAsync(CreateProductCategoryDto model)
        {
            using (var context = new ECommerceDbContext())
            {
                ProductCategory entity = new ProductCategory();
                entity.Name = model.Name;
                entity.CreateDate = DateTime.Now;
                entity.CreateUserId = 1;
                entity.IsActive = true;
                context.ProductCategories.Add(entity);
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<GetProductCategoryDto>> GetAllProductCategoriesAsync()
        {
            using (var context = new ECommerceDbContext())
            {
                var data = await (from a in context.ProductCategories.Where(x => x.IsActive == true)
                                  select new GetProductCategoryDto
                                  {
                                      Id = a.Id,
                                      Name = a.Name,
                                  }).OrderBy(x => x.Name).ToListAsync();
                return data;
            }
        }

        public async Task<GetProductCategoryDto> GetProductCategoryById(int productCategoryId)
        {
            using (var context = new ECommerceDbContext())
            {
                var data = await (from a in context.ProductCategories.Where(x => x.IsActive == true && x.Id == productCategoryId)
                                  select new GetProductCategoryDto
                                  {
                                      Id = a.Id,
                                      Name = a.Name,
                                  }).FirstOrDefaultAsync();
                if (data != null)
                {
                    return data;
                }
                return null;
            }
        }

        public async Task UpdateProductCategoryAsync(UpdateProductCategoryDto model)
        {
            using (var context = new ECommerceDbContext())
            {
                var getData = await context.ProductCategories.FirstOrDefaultAsync(x => x.Id == model.Id && x.IsActive == true);
                if (getData != null)
                {
                    getData.Name = model.Name;
                    getData.UpdateDate = DateTime.Now;
                    getData.UpdateUserId = 1;
                    await context.SaveChangesAsync();
                }

            }
        }
    }
}
