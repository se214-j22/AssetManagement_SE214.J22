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
        public async Task<ListResultDto<ProductTypeDto>> GetProductTypes(ProductTypeListInputDto input)
        {
            return await this.productTypeAppService.GetProductTypesAsync(input);
        }

        [HttpPost("status/{id}")]
        public async Task<ProductTypeDto> SetStatusProductTypeAsync(int id)
        {
            return await this.productTypeAppService.SetStatusProductTypeAsync(id);
        }

        [HttpPost]
        public async Task<ProductTypeDto> CreateProductTypeDto(ProductTypeDto dto)
        {
            return await this.productTypeAppService.CreateProductTypeAsync(dto);
        }

        [HttpDelete("{id}")]
        public async Task DeleteProductTypeAsync(int id)
        {
            await this.productTypeAppService.DeleteProductTypeAsync(id);
        }

        [HttpPost("edit")]
        public async Task<ProductTypeDto> EditNameProductTypeAsync(ProductTypeDto dto)
        {
            return await this.productTypeAppService.EditNameProductTypeAsync(dto.Id, dto.Name, dto.Note);
        }
    }
}
