using Core.Utilities.Results.Abstract;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IProductService
    {
        Task<IDataResult<List<GetAllProductDto>>> GetAllProductsAsync();
        Task<IDataResult<GetProductByIdDto>> GetProductById(int productId);
        Task<IResult> CreateProductAsync(CreateProductDto model);
        Task<IResult> UpdateProductAsync(UpdateProductDto model);
        Task<IResult> DeleteProductAsync(int productId);
    }
}
