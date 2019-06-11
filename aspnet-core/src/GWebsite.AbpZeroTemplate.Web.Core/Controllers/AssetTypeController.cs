using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Customers;
using GWebsite.AbpZeroTemplate.Application.Share.Customers.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Assets;
using GWebsite.AbpZeroTemplate.Application.Share.Assets.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Abp.UI;

namespace GWebsite.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AssetTypeController : GWebsiteControllerBase
    {
        private readonly IAssetTypeAppService assetTypeAppService;

        public AssetTypeController(IAssetTypeAppService assetTypeAppService)
        {
            this.assetTypeAppService = assetTypeAppService;
        }

        [HttpGet]
        public async Task<PagedResultDto<AssetTypeDto>> GetByFilter(AssetTypeFilter filter)
        {
            return await assetTypeAppService.GetsForView(filter);
        }

        [HttpGet]
        public async Task<AssetTypeDto> GetById(int id)
        {
            return await assetTypeAppService.GetAsyncForView(id);
        }

        [HttpGet]
        public async Task<AssetTypeDto> GetByCode(string code)
        {
            return await assetTypeAppService.GetAsyncForView(code);
        }

        [HttpGet]
        public async Task<AssetTypeInput> GetForEdit(int id)
        {
            return await assetTypeAppService.GetAsyncForEdit(id);
        }


        [HttpPost]
        public async Task CreateOrEdit([FromBody] AssetTypeInput input)
        {
            await assetTypeAppService.CreateOrEdit(input);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            if (await assetTypeAppService.HasAnyRecordsPointTo(id))
                throw new UserFriendlyException("Ooppps! Your request was bad!", "This record has any asset lines to! Please destruct them!");
            await assetTypeAppService.DeleteAsync(id);
        }
    }
}
