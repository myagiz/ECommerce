using Core.Repository.Abstract;
using Entities.DTOs;
using Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IProductCategoryDal : IEntityRepository<ProductCategory>
    {
        Task<List<GetProductCategoryDto>> GetAllProductCategoriesAsync();
        Task<GetProductCategoryDto> GetProductCategoryById(int productCategoryId);
        Task CreateProductCategoryAsync(CreateProductCategoryDto model);
        Task UpdateProductCategoryAsync(UpdateProductCategoryDto model);
    }
}
