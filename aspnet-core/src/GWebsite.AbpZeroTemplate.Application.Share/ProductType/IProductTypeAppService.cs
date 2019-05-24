using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.MenuClients.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Product.Dto;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.ProductType
{
    public interface IProductTypeAppService
    {
        Task<PagedResultDto<ProductTypeDto>> GetProductTypesAsync(GetMenuClientInput input);
    }
}
