using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.MenuClients.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Product.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Products.Dto;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.Products
{
    public interface IProductAppService
    {
        Task<PagedResultDto<ProductDto>> GetProductsAsync(GetProductInput input);
        Task<ProductDto> GetProductAsync(EntityDto<int> input);
        Task<ProductDto> UpdateProductAsync(ProductSavedDto productSavedDto);
        Task<ProductDto> ActiveProductAsync(int id);
        Task DeleteProductAsync(int id);
        Task<ProductDto> CreateProductAsync(ProductSavedCreate productSavedCreate);
    }
}
