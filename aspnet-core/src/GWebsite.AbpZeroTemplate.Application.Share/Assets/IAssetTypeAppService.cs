using GWebsite.AbpZeroTemplate.Application.Share.Assets.Dto;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.Assets
{
    public interface IAssetTypeAppService
    {
        Task<AssetTypeDto> GetAsyncForView(int id);
        Task<AssetTypeDto> GetAsyncForView(string code);
        Task CreateOrEdit(AssetTypeInput assetTypeInput);
    }
}
