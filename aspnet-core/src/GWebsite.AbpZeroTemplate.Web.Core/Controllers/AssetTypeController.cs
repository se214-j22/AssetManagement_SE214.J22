using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Customers;
using GWebsite.AbpZeroTemplate.Application.Share.Customers.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Assets;
using GWebsite.AbpZeroTemplate.Application.Share.Assets.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
        public async Task<AssetTypeDto> GetById(int id)
        {
            return await assetTypeAppService.GetAsyncForView(id);
        }

        [HttpGet]
        public async Task<AssetTypeDto> GetByCode(string code)
        {
            return await assetTypeAppService.GetAsyncForView(code);
        }
        [HttpPost]
        public async Task CreateOrEdit([FromBody] AssetTypeInput input)
        {
            await assetTypeAppService.CreateOrEdit(input);
        }
    }
}
