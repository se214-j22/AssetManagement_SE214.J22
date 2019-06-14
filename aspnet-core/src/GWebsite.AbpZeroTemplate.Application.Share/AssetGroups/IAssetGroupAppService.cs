using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.AssetGroups.Dto;
using System.Collections.Generic;

namespace GWebsite.AbpZeroTemplate.Application.Share.AssetGroups
{
    public interface IAssetGroupAppService
    {
        void CreateOrEditAssetGroup(AssetGroupInput assetgroupInput);
        AssetGroupInput GetAssetGroupForEdit(int id);
        void DeleteAssetGroup(int id);
        PagedResultDto<AssetGroupDto> GetAssetGroups(AssetGroupFilter input);
        AssetGroupForViewDto GetAssetGroupForView(int id);
        List<AssetGroupDto> GetListAssetGroups(string assetGrouptId);
        string GetAssetGroupNameByAssetID(string assetGrouptId);
        List<AssetGroupDto> GetListAssetGroupsByAssetType(int assetType);
        AssetGroupForViewDto GetAssetGroupByAssetID(string assetGrouptId);

    }
}

