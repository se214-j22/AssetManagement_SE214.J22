using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using GWebsite.AbpZeroTemplate.Application;
using GWebsite.AbpZeroTemplate.Application.Share.MenuClients.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Product.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.ProductType;
using GWebsite.AbpZeroTemplate.Core.Authorization;
using GWebsite.AbpZeroTemplate.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Web.Core.ProductCategory
{
    [AbpAuthorize(GWebsitePermissions.Pages_Catalog_ProductCatalog)]
    public class ProductTypeAppService : GWebsiteAppServiceBase, IProductTypeAppService
    {
        private readonly IRepository<ProductType, int> _productTypeRepository;

        public ProductTypeAppService(IRepository<ProductType, int> productTypeRepository)
        {
            this._productTypeRepository = productTypeRepository;
        }

        public async Task<PagedResultDto<ProductTypeDto>> GetProductTypesAsync(GetMenuClientInput input)
        {
            IQueryable<ProductType> query = this._productTypeRepository.GetAll();
            int totalCount = await query.CountAsync();
            List<ProductType> items = await query.OrderBy(input.Sorting).PageBy(input).ToListAsync();
            return new PagedResultDto<ProductTypeDto>(
             totalCount,
             items.Select(model => this.ObjectMapper.Map<ProductTypeDto>(model)).ToList());
        }
    }
}
