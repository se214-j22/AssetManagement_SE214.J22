using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.MenuClients.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Product.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.ProductType.Dto;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.ProductType
{
    public interface IProductTypeAppService
    {
        Task<PagedResultDto<ProductTypeDto>> GetProductTypesAsync(ProductTypeListInputDto input);

        Task<ProductTypeDto> SetStatusProductTypeAsync(int id);

        Task<ProductTypeDto> CreateProductTypeAsync(ProductTypeDto dto);

        Task DeleteProductTypeAsync(int id);

        Task<ProductTypeDto> EditNameProductTypeAsync(int id, string name, string note);
    }
}
