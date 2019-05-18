using GWebsite.AbpZeroTemplate.Application.Share.Assets.Dto;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.Assets
{
    public interface IManufacturerAppService
    {
        Task<ManufacturerDto> GetAsyncForView(int id);
        Task<ManufacturerDto> GetAsyncForView(string code);
        Task CreateOrEdit(ManufacturerInput manufacturerInput);
    }
}
