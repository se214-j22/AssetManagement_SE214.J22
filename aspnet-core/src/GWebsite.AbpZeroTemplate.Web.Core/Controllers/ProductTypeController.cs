using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.MenuClients.Dto;
using Microsoft.AspNetCore.Mvc;
using GWebsite.AbpZeroTemplate.Application.Share.ProductType;
using GWebsite.AbpZeroTemplate.Application.Share.Product.Dto;

namespace GWebsite.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    class ProductTypeController : GWebsiteControllerBase
    {
        private readonly IProductTypeAppService productTypeAppService;

        public ProductTypeController(IProductTypeAppService productTypeAppService)
        {
            this.productTypeAppService = productTypeAppService;
        }

        [HttpGet]
        public async Task<ListResultDto<ProductTypeDto>> GetProductTypes(GetMenuClientInput input)
        {
            return await this.productTypeAppService.GetProductTypesAsync(input);
        }
    }
}
