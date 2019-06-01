using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.MenuClients.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Bidding.Dto;
using System.Threading.Tasks;
using GWebsite.AbpZeroTemplate.Application.Share.SupplierType.Dto;

namespace GWebsite.AbpZeroTemplate.Application.Share.SupplierType
{
    public interface ISupplierTypeAppService
    {
        Task<PagedResultDto<SupplierTypeDto>> GetSupplierTypesAsync(SupplierTypeListInputDto input);

        Task<SupplierTypeDto> SetStatusSupplierTypeAsync(int id);

        Task<SupplierTypeDto> CreateSupplierTypeAsync(SupplierTypeDto dto);

        Task DeleteSupplierTypeAsync(int id);

        Task<SupplierTypeDto> EditNameSupplierTypeAsync(int id, string name, string note);
    }
}
