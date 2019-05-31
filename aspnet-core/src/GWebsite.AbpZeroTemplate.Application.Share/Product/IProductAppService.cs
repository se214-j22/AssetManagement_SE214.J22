using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.MenuClients.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Products.Dto;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.Products
{
    public interface IProductAppService
    {
        Task<PagedResultDto<ProductDto>> GetProductsAsync(GetMenuClientInput input);
        Task<ProductDto> GetProductAsync(EntityDto<int> input);
    }
}
