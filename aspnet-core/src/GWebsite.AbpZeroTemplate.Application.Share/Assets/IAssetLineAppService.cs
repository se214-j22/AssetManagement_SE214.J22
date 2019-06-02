using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Assets.Dto;
using GWebsite.AbpZeroTemplate.Core.Models;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.Assets
{
    public interface IAssetLineAppService
    {
        Task<PagedResultDto<AssetLineDto>> GetsForView(AssetLineFilter filter);
        Task<AssetLineDto> GetAsyncForView(int id);
        Task<AssetLineDto> GetAsyncForView(string code);
        Task CreateOrEdit(AssetLineInput assetLineInput);
        Task<bool> HasAnyRecordsPointTo(int id);
        Task DeleteAsync(int id);
    }
}
