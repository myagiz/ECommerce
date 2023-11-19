using Business.Abstract;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoriesController : ControllerBase
    {
        private readonly IProductCategoryService _productCategoryService;

        public ProductCategoriesController(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }


        [HttpGet("GetAllProductCategoriesAsync")]
        public async Task<IActionResult> GetAllProductCategoriesAsync()
        {
            var result = await _productCategoryService.GetAllProductCategoriesAsync();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetProductCategoryById{productCategoryId}")]
        public async Task<IActionResult> GetProductCategoryById(int productCategoryId)
        {
            var result = await _productCategoryService.GetProductCategoryById(productCategoryId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("CreateProductCategoryAsync")]
        public async Task<IActionResult> CreateProductCategoryAsync(CreateProductCategoryDto model)
        {
            var result = await _productCategoryService.CreateProductCategoryAsync(model);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        [HttpPut("UpdateProductCategoryAsync")]
        public async Task<IActionResult> UpdateProductCategoryAsync(UpdateProductCategoryDto model)
        {
            var result = await _productCategoryService.UpdateProductCategoryAsync(model);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
