using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Customers;
using GWebsite.AbpZeroTemplate.Application.Share.Customers.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Assets;
using GWebsite.AbpZeroTemplate.Application.Share.Assets.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using GWebsite.AbpZeroTemplate.Core.Models;
using System;

namespace GWebsite.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AssetController : GWebsiteControllerBase
    {
        private readonly IAssetAppService assetAppService;

        public AssetController(IAssetAppService assetAppService)
        {
            this.assetAppService = assetAppService;
        }

        [HttpGet]
        public async Task<AssetDto> GetById(int id)
        {
            return await assetAppService.GetAsyncForView(id);
        }

        [HttpGet]
        public async Task<AssetDto> GetByCode(string code)
        {
            return await assetAppService.GetAsyncForView(code);
        }
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] AssetInput input)
        {
            try
            {
                if (input.Number == 0)
                {
                    await assetAppService.CreateOrEdit(input);
                }
                else
                {
                    for (int i = 0; i < input.Number; i++)
                        await assetAppService.CreateAsync(input);
                }
                return Ok();
            }
            catch (ArgumentException e)
            {
                return BadRequest(e);
            }
        }
    }
}
