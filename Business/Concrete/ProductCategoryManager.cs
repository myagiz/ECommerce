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
    public class ProductCategoryManager : IProductCategoryService
    {
        private readonly IProductCategoryDal _productCategoryDal;

        public ProductCategoryManager(IProductCategoryDal productCategoryDal)
        {
            _productCategoryDal = productCategoryDal;
        }

        public async Task<IResult> CreateProductCategoryAsync(CreateProductCategoryDto model)
        {
            await _productCategoryDal.CreateProductCategoryAsync(model);
            return new SuccessResult(Messages.Added);
        }

        public async Task<IDataResult<List<GetProductCategoryDto>>> GetAllProductCategoriesAsync()
        {
            var result = await _productCategoryDal.GetAllProductCategoriesAsync();
            return new SuccessDataResult<List<GetProductCategoryDto>>(result, Messages.Listed);
        }

        public async Task<IDataResult<GetProductCategoryDto>> GetProductCategoryById(int productCategoryId)
        {
            var result = await _productCategoryDal.GetProductCategoryById(productCategoryId);
            if (result != null)
            {
                return new SuccessDataResult<GetProductCategoryDto>(result, Messages.Succeed);
            }
            return new ErrorDataResult<GetProductCategoryDto>(Messages.NotFoundData);
        }

        public async Task<IResult> UpdateProductCategoryAsync(UpdateProductCategoryDto model)
        {
            var result = _productCategoryDal.Get(x => x.Id == model.Id && x.IsActive == true);
            if (result != null)
            {
                await _productCategoryDal.UpdateProductCategoryAsync(model);
                return new SuccessResult(Messages.Updated);
            }
            return new ErrorResult(Messages.NotFoundData);
        }
    }
}
