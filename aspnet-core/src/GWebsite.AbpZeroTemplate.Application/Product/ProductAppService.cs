using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using GWebsite.AbpZeroTemplate.Application;
using GWebsite.AbpZeroTemplate.Application.Share.MenuClients;
using GWebsite.AbpZeroTemplate.Application.Share.MenuClients.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Product.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Products;
using GWebsite.AbpZeroTemplate.Application.Share.Products.Dto;
using GWebsite.AbpZeroTemplate.Core.Authorization;
using GWebsite.AbpZeroTemplate.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Web.Core
{
    [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient)]
  public  class ProductAppService : GWebsiteAppServiceBase, IProductAppService
    {
        private readonly IRepository<Product, int> _productRepository;
        private readonly IRepository<ProductType, int> _productTypeRepository;
        //, IRepository<ProductType, int> productTypeRepository
        public ProductAppService(IRepository<Product, int> productRepository, IRepository<ProductType, int> productTypeRepository)
        {
            _productRepository = productRepository;
            _productTypeRepository = productTypeRepository;


        }

        public async Task<PagedResultDto<ProductDto>> GetProductsAsync(GetMenuClientInput input)
        {
            var query =  _productRepository.GetAllIncluding().Include(p=>p.Image).Include(p=>p.Biddings).ThenInclude(p=>p.Supplier);
            var totalCount = await query.CountAsync();
            System.Collections.Generic.List<Product> items = await query.OrderBy(input.Sorting).PageBy(input).ToListAsync();
            return new PagedResultDto<ProductDto>(
             totalCount,
             items.Select(item => ObjectMapper.Map<ProductDto>(item)).ToList());
        }

        public async Task<ProductDto> GetProductAsync(EntityDto<int> input)
        {
            var entity = await _productRepository.GetAllIncluding().Include(p => p.Image).Include(p => p.Biddings).ThenInclude(p => p.Supplier).FirstOrDefaultAsync(x => x.Id == input.Id);
            return ObjectMapper.Map<ProductDto>(entity);
        }



        public async Task<IEnumerable<ProductTypeDto>> GetProductTypesAsync()
        {
            var items = await _productTypeRepository.GetAllListAsync();
            return items.Select(model => ObjectMapper.Map<ProductTypeDto>(model));
        }
    }
}
