using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.AssetDetails;
using GWebsite.AbpZeroTemplate.Application.Share.AssetDetails.Dto;
using Microsoft.AspNetCore.Mvc;

namespace GWebsite.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AssetDetailController : GWebsiteControllerBase
    {
        private readonly IAssetDetailAppService customerAppService;

        public AssetDetailController(IAssetDetailAppService customerAppService)
        {
            this.customerAppService = customerAppService;
        }

        [HttpGet]
        public PagedResultDto<AssetDetailDto> GetAssetDetailsByFilter(AssetDetailFilter customerFilter)
        {
            return customerAppService.GetAssetDetails(customerFilter);
        }

        [HttpGet]
        public AssetDetailInput GetAssetDetailForEdit(int id)
        {
            return customerAppService.GetAssetDetailForEdit(id);
        }

        [HttpPost]
        public void CreateOrEditAssetDetail([FromBody] AssetDetailInput input)
        {
            customerAppService.CreateOrEditAssetDetail(input);
        }

        [HttpDelete("{id}")]
        public void DeleteAssetDetail(int id)
        {
            customerAppService.DeleteAssetDetail(id);
        }

        [HttpGet]
        public AssetDetailForViewDto GetAssetDetailForView(int id)
        {
            return customerAppService.GetAssetDetailForView(id);
        }
    }
}
