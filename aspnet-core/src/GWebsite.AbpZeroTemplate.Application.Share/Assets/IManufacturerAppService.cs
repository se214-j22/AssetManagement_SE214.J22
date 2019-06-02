using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Assets.Dto;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.Assets
{
    public interface IManufacturerAppService
    {
        Task<PagedResultDto<ManufacturerDto>> GetsForView(ManufacturerFilter filter);
        Task<ManufacturerDto> GetAsyncForView(int id);
        Task<ManufacturerDto> GetAsyncForView(string code);
        Task CreateOrEdit(ManufacturerInput manufacturerInput);
        Task<bool> HasAnyRecordsPointTo(int id);
        Task DeleteAsync(int id);
    }
}
