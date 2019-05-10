using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Assets.Dto;

namespace GWebsite.AbpZeroTemplate.Application.Share.Assets
{
    public interface IAssetAppService
    {
        void CreateOrEditAsset(AssetInput assetInput);
        AssetInput GetAssetForEdit(int id);
        void DeleteAsset(int id);
        PagedResultDto<AssetDto> GetAssets(AssetFilter input);
        AssetForViewDto GetAssetForView(int id);
    }
}