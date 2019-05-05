using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.MenuClients.Dto;
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
        public async Task<ListResultDto<ProductDto>> GetProducts(GetMenuClientInput input)
        {
            return await _ProductAppService.GetProductsAsync(input);
        }

        [HttpGet]
        public async Task<ProductDto> GetProduct(int Id)
        {
            return await _ProductAppService.GetProductAsync(Id);
        }
    }
}
