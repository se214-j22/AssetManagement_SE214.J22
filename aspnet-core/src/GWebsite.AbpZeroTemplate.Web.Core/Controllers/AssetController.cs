using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Assets;
using GWebsite.AbpZeroTemplate.Application.Share.Assets.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
        public PagedResultDto<AssetDto> GetAssetsByFilter(AssetFilter assetFilter)
        {
            return assetAppService.GetAssets(assetFilter);
        }

        [HttpGet]
        public AssetInput GetAssetForEdit(int id)
        {
            return assetAppService.GetAssetForEdit(id);
        }

        [HttpPost]
        public void CreateOrEditAsset([FromBody] AssetInput input)
        {
            assetAppService.CreateOrEditAsset(input);
        }

        [HttpDelete("{id}")]
        public void DeleteAsset(int id)
        {
            assetAppService.DeleteAsset(id);
        }

        [HttpGet]
        public AssetForViewDto GetAssetForView(int id)
        {
            return assetAppService.GetAssetForView(id);
        }

        [HttpGet]
        public string GetAssetNameByID(string assetId)
        {
            return assetAppService.GetAssetNameByAssetID(assetId);
        }

        [HttpGet("{id}")]
        public void ApproveAsset(int id)
        {
            assetAppService.ApproveAsset(id);
        }

        [HttpGet]
        public AssetForViewDto GetAssetByAssetID(string assetId)
        {
            return assetAppService.GetAssetByAssetID(assetId);
        }

        [HttpGet]
        public int GetTotalAsset()
        {
            return assetAppService.GetTotalAsset();
        }

        [HttpGet]
        public List<AssetForViewDto> GetListAssetsInStock()
        {
            return assetAppService.GetListAssetsInStock();
        }

        [HttpGet]
        public List<AssetForViewDto> GetListAssetsInUse()
        {
            return assetAppService.GetListAssetsInUse();
        }

        [HttpGet]
        public void updateAssetStatusInStock(string assetID)
        {
            assetAppService.updateAssetStatusInStock(assetID);
        }

        [HttpGet]
        public void updateAssetStatusUsing(string assetID)
        {
            assetAppService.updateAssetStatusUsing(assetID);
        }

        [HttpGet]
        public void updateAssetStatusReparing(string assetID)
        {
            assetAppService.updateAssetStatusReparing(assetID);
        }

        [HttpGet]
        public void updateAssetStatusLiquidated(string assetID)
        {
            assetAppService.updateAssetStatusLiquidated(assetID);
        }
    }
}