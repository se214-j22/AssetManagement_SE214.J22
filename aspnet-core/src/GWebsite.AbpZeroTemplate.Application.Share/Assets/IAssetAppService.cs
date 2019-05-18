using GWebsite.AbpZeroTemplate.Application.Share.Assets.Dto;
using GWebsite.AbpZeroTemplate.Core.Models;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.Assets
{
    public interface IAssetAppService
    {
        Task<AssetDto> GetAsyncForView(int id);
        Task<AssetDto> GetAsyncForView(string code);
        Task CreateOrEdit(AssetInput assetInput);
        Task CreateAsync(AssetInput assetInput);
    }
}
