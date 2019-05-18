using GWebsite.AbpZeroTemplate.Application.Share.Assets.Dto;
using GWebsite.AbpZeroTemplate.Core.Models;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.Assets
{
    public interface IAssetLineAppService
    {
        Task<AssetLineDto> GetAsyncForView(int id);
        Task<AssetLineDto> GetAsyncForView(string code);
        Task CreateOrEdit(AssetLineInput assetLineInput);
    }
}
