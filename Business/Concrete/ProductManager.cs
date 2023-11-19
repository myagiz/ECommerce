using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.ErrorResults;
using Core.Utilities.Results.Concrete.SuccessResults;
using DataAccess.Abstract;
using Entities.DTOs;
using Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public async Task<IResult> CreateProductAsync(CreateProductDto model)
        {
            await _productDal.CreateProductAsync(model);
            return new SuccessResult(Messages.Added);
        }

        public async Task<IResult> DeleteProductAsync(int productId)
        {
            var control = _productDal.Get(x => x.IsActive == true && x.Id == productId);
            if (control != null)
            {
                await _productDal.DeleteProductAsync(productId);
                return new SuccessResult(Messages.Deleted);
            }
            return new ErrorResult(Messages.NotFoundData);
        }

        public async Task<IDataResult<List<GetAllProductDto>>> GetAllProductsAsync()
        {
            var result = await _productDal.GetAllProductsAsync();
            return new SuccessDataResult<List<GetAllProductDto>>(result, Messages.Listed);
        }

        public async Task<IDataResult<GetProductByIdDto>> GetProductById(int productId)
        {
            var result = await _productDal.GetProductById(productId);
            if (result != null)
            {
                return new SuccessDataResult<GetProductByIdDto>(result, Messages.Succeed);
            }
            return new ErrorDataResult<GetProductByIdDto>(Messages.NotFoundData);
        }

        public async Task<IResult> UpdateProductAsync(UpdateProductDto model)
        {
            var control = _productDal.Get(x => x.IsActive == true && x.Id == model.Id);
            if (control != null)
            {
                await _productDal.UpdateProductAsync(model);
                return new SuccessResult(Messages.Updated);
            }
            return new ErrorResult(Messages.NotFoundData);
        }
    }
}
