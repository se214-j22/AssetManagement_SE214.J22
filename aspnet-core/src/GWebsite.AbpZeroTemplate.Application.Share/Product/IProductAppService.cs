using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.MenuClients.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Product.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Products.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.Products
{
    public interface IProductAppService
    {
        Task<PagedResultDto<ProductDto>> GetProductsAsync(GetMenuClientInput input);
        Task<ProductDto> GetProductAsync(int id);
        Task<IEnumerable<ProductTypeDto>> GetProductTypesAsync();
    }
}
