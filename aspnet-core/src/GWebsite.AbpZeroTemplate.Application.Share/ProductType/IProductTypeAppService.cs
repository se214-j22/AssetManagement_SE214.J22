using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.MenuClients.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Product.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.ProductType.Dto;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.ProductType
{
    public interface IProductTypeAppService
    {
        Task<PagedResultDto<ProductTypeDto>> GetProductTypesWithFilterAsync(ProductTypeListInputDto input);

        Task<ProductTypeDto> ToggleStatusProductCatalogAsync(int id);

        Task<ProductTypeDto> CreateProductCatalogAsync(ProductTypeSavedDto productTypeSavedDto);

        Task DeleteProductCatalogAsync(int id);

        Task<ProductTypeDto> UpdateProductCatalogAsync(ProductTypeSavedDto supplierTypeSavedDto);
    }
}
