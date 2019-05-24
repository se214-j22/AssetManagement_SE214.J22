using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.MenuClients.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Bidding.Dto;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.SupplierType
{
    public interface ISupplierTypeAppService
    {
        Task<PagedResultDto<SupplierTypeDto>> GetSupplierTypesAsync(GetMenuClientInput input);
    }
}
