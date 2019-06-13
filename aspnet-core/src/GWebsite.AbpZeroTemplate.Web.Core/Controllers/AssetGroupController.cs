using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.AssetGroups;
using GWebsite.AbpZeroTemplate.Application.Share.AssetGroups.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace GWebsite.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AssetGroupController : GWebsiteControllerBase
    {
        private readonly IAssetGroupAppService assetGroupAppService;

        public AssetGroupController(IAssetGroupAppService assetGroupAppService)
        {
            this.assetGroupAppService = assetGroupAppService;
        }

        [HttpGet]
        public PagedResultDto<AssetGroupDto> GetAssetGroupsByFilter(AssetGroupFilter assetGroupFilter)
        {
            return assetGroupAppService.GetAssetGroups(assetGroupFilter);
        }

        [HttpGet]
        public AssetGroupInput GetAssetGroupForEdit(int id)
        {
            return assetGroupAppService.GetAssetGroupForEdit(id);
        }

        [HttpPost]
        public void CreateOrEditAssetGroup([FromBody] AssetGroupInput input)
        {
            assetGroupAppService.CreateOrEditAssetGroup(input);
        }

        [HttpDelete("{id}")]
        public void DeleteAssetGroup(int id)
        {
            assetGroupAppService.DeleteAssetGroup(id);
        }

        [HttpGet]
        public AssetGroupForViewDto GetAssetGroupForView(int id)
        {
            return assetGroupAppService.GetAssetGroupForView(id);
        }

        [HttpGet]
        public List<AssetGroupDto> GetListAssetGroups(string assetGrouptId)
        {
            return assetGroupAppService.GetListAssetGroups(assetGrouptId);
        }

        [HttpGet]
        public string GetAssetGroupNameByAssetID(string assetGrouptId)
        {
            return assetGroupAppService.GetAssetGroupNameByAssetID(assetGrouptId);
        }

        [HttpGet]
        public List<AssetGroupDto> GetListAssetGroupsByAssetType(int assetType)
        {
            return assetGroupAppService.GetListAssetGroupsByAssetType(assetType);
        }

        [HttpGet]
        public AssetGroupForViewDto GetAssetGroupByAssetID(string assetGrouptId)
        {
            return assetGroupAppService.GetAssetGroupByAssetID(assetGrouptId);
        }
    }
}