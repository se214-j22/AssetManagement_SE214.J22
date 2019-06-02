using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Assets.Dto;
using GWebsite.AbpZeroTemplate.Core.Models;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.Assets
{
    public interface IAssetAppService
    {
        Task<PagedResultDto<AssetDto>> GetsForView(AssetFilter filter);
        Task<AssetDto> GetAsyncForView(int id);
        Task<AssetDto> GetAsyncForView(string code);
        Task<AssetInput> GetForEdit(int id);
        Task CreateOrEdit(AssetInput assetInput);
        Task CreateAsync(AssetInput assetInput);
        Task DeleteAsync(int id);
    }
}
