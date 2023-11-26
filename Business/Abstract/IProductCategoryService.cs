using Core.Utilities.Results.Abstract;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IProductCategoryService
    {
        Task<IDataResult<List<GetProductCategoryDto>>> GetAllProductCategoriesAsync();
        Task<IDataResult<GetProductCategoryDto>> GetProductCategoryById(int productCategoryId);
        Task<IResult> CreateProductCategoryAsync(CreateProductCategoryDto model);
        Task<IResult> UpdateProductCategoryAsync(UpdateProductCategoryDto model);
    }
}
