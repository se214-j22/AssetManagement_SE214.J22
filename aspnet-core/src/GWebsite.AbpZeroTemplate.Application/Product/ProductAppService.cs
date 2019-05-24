using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using GWebsite.AbpZeroTemplate.Application;
using GWebsite.AbpZeroTemplate.Application.Share.MenuClients.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Products;
using GWebsite.AbpZeroTemplate.Application.Share.Products.Dto;
using GWebsite.AbpZeroTemplate.Core.Authorization;
using GWebsite.AbpZeroTemplate.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Web.Core
{
    [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient)]
    public class ProductAppService : GWebsiteAppServiceBase, IProductAppService
    {
        private readonly IRepository<Product, int> productRepository;

        public ProductAppService(IRepository<Product, int> productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<PagedResultDto<ProductDto>> GetProductsAsync(GetMenuClientInput input)
        {
            var query = this.productRepository.GetAllIncluding().Include(p => p.Image).Include(p => p.Biddings).ThenInclude(p => p.Supplier);
            var totalCount = await query.CountAsync();
            List<Product> items = await query.OrderBy(input.Sorting).PageBy(input).ToListAsync();
            return new PagedResultDto<ProductDto>(
             totalCount,
             items.Select(item => this.ObjectMapper.Map<ProductDto>(item)).ToList());
        }

        public async Task<ProductDto> GetProductAsync(EntityDto<int> input)
        {
            var entity = await this.productRepository.GetAllIncluding().Include(p => p.Image).Include(p => p.Biddings).ThenInclude(p => p.Supplier).FirstOrDefaultAsync(x => x.Id == input.Id);
            return this.ObjectMapper.Map<ProductDto>(entity);
        }
    }
}
