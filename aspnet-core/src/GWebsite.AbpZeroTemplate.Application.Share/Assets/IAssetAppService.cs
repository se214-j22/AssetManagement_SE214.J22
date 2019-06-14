using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Assets.Dto;
using System.Collections.Generic;

namespace GWebsite.AbpZeroTemplate.Application.Share.Assets
{
    public interface IAssetAppService
    {
        void CreateOrEditAsset(AssetInput assetInput);
        AssetInput GetAssetForEdit(int id);
        void DeleteAsset(int id);
        PagedResultDto<AssetDto> GetAssets(AssetFilter input);
        AssetForViewDto GetAssetForView(int id);
        string GetAssetNameByAssetID(string assetId);
        void ApproveAsset(int id);
        int GetTotalAsset();
        AssetForViewDto GetAssetByAssetID(string assetId);
        List<AssetForViewDto> GetListAssetsInStock();
        List<AssetForViewDto> GetListAssetsInUse();
        void updateAssetStatusInStock(string assetID);
        void updateAssetStatusUsing(string assetID);
        void updateAssetStatusReparing(string assetID);
        void updateAssetStatusLiquidated(string assetID);
    }
}
