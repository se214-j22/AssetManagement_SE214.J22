using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.MenuClients.Dto;
using Microsoft.AspNetCore.Mvc;
using GWebsite.AbpZeroTemplate.Application.Share.SupplierType;
using GWebsite.AbpZeroTemplate.Application.Share.Bidding.Dto;

namespace GWebsite.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    class SupplierTypeController : GWebsiteControllerBase
    {
        private readonly ISupplierTypeAppService  supplierTypeAppService;

        public SupplierTypeController(ISupplierTypeAppService supplierTypeAppService)
        {
            this.supplierTypeAppService = supplierTypeAppService;
        }

        [HttpGet]
        public async Task<ListResultDto<SupplierTypeDto>> GetSupplierTypes(GetMenuClientInput input)
        {
            return await this.supplierTypeAppService.GetSupplierTypesAsync(input);
        }
    }
}
