using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.MenuClients.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Product.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Products;
using GWebsite.AbpZeroTemplate.Application.Share.Products.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ProductsController : GWebsiteControllerBase
    {
        private readonly IProductAppService _ProductAppService;

        public ProductsController(IProductAppService ProductAppService)
        {
            _ProductAppService = ProductAppService;
        }

        [HttpGet]
        public async Task<ListResultDto<ProductDto>> GetProducts(GetProductInput input)
        {
            return await _ProductAppService.GetProductsAsync(input);
        }

        [HttpGet]
        public async Task<ProductDto> GetProduct(EntityDto<int> input)
        {
            return await _ProductAppService.GetProductAsync(input);
        }

        [HttpPut]
        public async Task<ProductDto> UpdateProductAsync([FromBody] ProductSavedDto productSavedDto)
        {
            return await this._ProductAppService.UpdateProductAsync(productSavedDto);
        }

        [HttpPut]
        public async Task<ProductDto> ActiveProductAsync(int id)
        {
            return await this._ProductAppService.ActiveProductAsync(id);
        }

        [HttpDelete("{id}")]
        public async Task DeleteProductAsync(int id)
        {
            await this._ProductAppService.DeleteProductAsync(id);
        }
    }
}
