using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Assets.Dto;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.Assets
{
    public interface IAssetTypeAppService
    {
        Task<PagedResultDto<AssetTypeDto>> GetsForView(AssetTypeFilter filter);
        Task<AssetTypeDto> GetAsyncForView(int id);
        Task<AssetTypeDto> GetAsyncForView(string code);
        Task CreateOrEdit(AssetTypeInput assetTypeInput);
        Task<bool> HasAnyRecordsPointTo(int id);
        Task DeleteAsync(int id);

    }
}
