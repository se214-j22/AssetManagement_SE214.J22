using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.MenuClients.Dto;
using Microsoft.AspNetCore.Mvc;
using GWebsite.AbpZeroTemplate.Application.Share.SupplierType;
using GWebsite.AbpZeroTemplate.Application.Share.Bidding.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.SupplierType.Dto;

namespace GWebsite.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class SupplierTypeController : GWebsiteControllerBase
    {
        private readonly ISupplierTypeAppService supplierTypeAppService;

        public SupplierTypeController(ISupplierTypeAppService supplierTypeAppService)
        {
            this.supplierTypeAppService = supplierTypeAppService;
        }

        [HttpGet]
        public async Task<PagedResultDto<SupplierTypeDto>> GetSupplierTypes(SupplierTypeListInputDto input)
        {
            return await this.supplierTypeAppService.GetSupplierTypesAsync(input);
        }

        [HttpPost]
        public async Task<SupplierTypeDto> CreateSupplierTypeDto([FromBody] SupplierTypeDto dto)
        {
            return await this.supplierTypeAppService.CreateSupplierTypeAsync(dto);
        }

        [HttpPut]
        public async Task<SupplierTypeDto> SetStatusSupplierTypeAsync(int id)
        {
            return await this.supplierTypeAppService.SetStatusSupplierTypeAsync(id);
        }

        [HttpDelete]
        public async Task DeleteSupplierTypeAsync(int id)
        {
            await this.supplierTypeAppService.DeleteSupplierTypeAsync(id);
        }

        [HttpPut]
        public async Task<SupplierTypeDto> EditNameSupplierTypeAsync([FromBody] SupplierTypeDto dto)
        {
            return await this.supplierTypeAppService.EditNameSupplierTypeAsync(dto.Id, dto.Name, dto.Note);
        }
    }
}
