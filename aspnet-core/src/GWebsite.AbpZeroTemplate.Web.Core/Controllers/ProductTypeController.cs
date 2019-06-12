using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.MenuClients.Dto;
using Microsoft.AspNetCore.Mvc;
using GWebsite.AbpZeroTemplate.Application.Share.ProductType;
using GWebsite.AbpZeroTemplate.Application.Share.Product.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.ProductType.Dto;

namespace GWebsite.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ProductTypeController : GWebsiteControllerBase
    {
        private readonly IProductTypeAppService productTypeAppService;

        public ProductTypeController(IProductTypeAppService productTypeAppService)
        {
            this.productTypeAppService = productTypeAppService;
        }

        [HttpGet]
        public async Task<PagedResultDto<ProductTypeDto>> GetProductTypes(ProductTypeListInputDto input)
        {
            return await this.productTypeAppService.GetProductTypesWithFilterAsync(input);
        }

        [HttpPut("status/{id}")]
        public async Task<ProductTypeDto> ToggleStatusProductCatalogAsync(int id)
        {
            return await this.productTypeAppService.ToggleStatusProductCatalogAsync(id);
        }

        [HttpPost]
        public async Task<ProductTypeDto> CreateProductCatalogAsync([FromBody] ProductTypeSavedDto dto)
        {
            return await this.productTypeAppService.CreateProductCatalogAsync(dto);
        }

        [HttpDelete("{id}")]
        public async Task DeleteProductCatalogAsync(int id)
        {
            await this.productTypeAppService.DeleteProductCatalogAsync(id);
        }

        [HttpPut("edit")]
        public async Task<ProductTypeDto> UpdateProductCatalogAsync([FromBody] ProductTypeSavedDto dto)
        {
            return await this.productTypeAppService.UpdateProductCatalogAsync(dto);
        }
    }
}
