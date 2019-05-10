using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.AssetDetails.Dto;

namespace GWebsite.AbpZeroTemplate.Application.Share.AssetDetails
{
    public interface IAssetDetailAppService
    {
        void CreateOrEditAssetDetail(AssetDetailInput customerInput);
        AssetDetailInput GetAssetDetailForEdit(int id);
        void DeleteAssetDetail(int id);
        PagedResultDto<AssetDetailDto> GetAssetDetails(AssetDetailFilter input);
        AssetDetailForViewDto GetAssetDetailForView(int id);
    }
}
