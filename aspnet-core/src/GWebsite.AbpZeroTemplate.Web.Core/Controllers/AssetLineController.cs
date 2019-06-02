using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Customers;
using GWebsite.AbpZeroTemplate.Application.Share.Customers.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Assets;
using GWebsite.AbpZeroTemplate.Application.Share.Assets.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using GWebsite.AbpZeroTemplate.Core.Models;

namespace GWebsite.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AssetLineController : GWebsiteControllerBase
    {
        private readonly IAssetLineAppService assetLineAppService;

        public AssetLineController(IAssetLineAppService assetLineAppService)
        {
            this.assetLineAppService = assetLineAppService;
        }

        [HttpGet]
        public async Task<PagedResultDto<AssetLineDto>> GetByFilter(AssetLineFilter filter)
        {
            return await assetLineAppService.GetsForView(filter);
        }

        [HttpGet]
        public async Task<AssetLineDto> GetById(int id)
        {
            return await assetLineAppService.GetAsyncForView(id);
        }

        [HttpGet]
        public async Task<AssetLineDto> GetByCode(string code)
        {
            return await assetLineAppService.GetAsyncForView(code);
        }
        [HttpPost]
        public async Task CreateOrEdit([FromBody] AssetLineInput input)
        {
            await assetLineAppService.CreateOrEdit(input);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (await assetLineAppService.HasAnyRecordsPointTo(id))
                return BadRequest("This record has anything point to!");
            await assetLineAppService.DeleteAsync(id);
            return Ok();
        }
    }
}
