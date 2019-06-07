using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using GWebsite.AbpZeroTemplate.Application;
using GWebsite.AbpZeroTemplate.Application.Share.MenuClients.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Product.Dto;
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
        private readonly IRepository<ProductType, int> productTypeRepository;

        public ProductAppService(IRepository<Product, int> productRepository, IRepository<ProductType, int> productTypeRepository)
        {
            this.productRepository = productRepository;
            this.productTypeRepository = productTypeRepository;
        }

        public async Task<PagedResultDto<ProductDto>> GetProductsAsync(GetProductInput input)
        {
            IQueryable<Product> query = this.productRepository.GetAllIncluding().Include(p => p.ProductType).Include(p => p.Supplier);
            if (input.Status == 1 || input.Status == 2)
            {
                query = query.Where(p => p.Status == input.Status);
            }
            if (input.Name!=null)
            {
                query = query.Where(p => p.Name.Contains(input.Name));
            }
            if (input.Code!=null)
            {
                query = query.Where(p => p.Code.Contains(input.Code));
            }
            var totalCount = await query.CountAsync();
            //if (totalCount == 0)
            //{
            //    query = this.productRepository.GetAllIncluding().Include(p => p.ProductType).Include(p => p.Supplier);
            //    totalCount = await query.CountAsync();
            //}
            List<Product> items = await query.OrderBy(input.Sorting).PageBy(input).ToListAsync();
            return new PagedResultDto<ProductDto>(
             totalCount,
             items.Select(item => this.ObjectMapper.Map<ProductDto>(item)).ToList());
        }

        public async Task<ProductDto> UpdateProductAsync(ProductSavedDto productSavedDto)
        {
            Product entity = await this.productRepository.GetAllIncluding().Include(p => p.ProductType).Include(p => p.Supplier).FirstOrDefaultAsync(item => item.Id == productSavedDto.Id);
            this.ObjectMapper.Map(productSavedDto, entity);
            entity = await this.productRepository.UpdateAsync(entity);
            await this.CurrentUnitOfWork.SaveChangesAsync();
            return this.ObjectMapper.Map<ProductDto>(entity);
        }

        public async Task<ProductDto> GetProductAsync(EntityDto<int> input)
        {
            var entity = await this.productRepository.GetAllIncluding().Include(p => p.ProductType).Include(p => p.Supplier).FirstOrDefaultAsync(x => x.Id == input.Id);
            return this.ObjectMapper.Map<ProductDto>(entity);
        }
        public async Task<ProductDto> ActiveProductAsync(int id)
        {
            Product entity = await this.productRepository.GetAllIncluding().Include(p => p.ProductType).Include(p => p.Supplier).FirstOrDefaultAsync(item => item.Id == id);
            entity.Status = 1;
            entity = await this.productRepository.UpdateAsync(entity);
            await this.CurrentUnitOfWork.SaveChangesAsync();
            return this.ObjectMapper.Map<ProductDto>(entity);
        }

        public async Task DeleteProductAsync(int id)
        {
            await this.productRepository.DeleteAsync(id);
        }
        public async Task<ProductDto> CreateProductAsync(ProductSavedCreate productSavedCreate)
        {
            Product product = ObjectMapper.Map<Product>(productSavedCreate);
            await productRepository.InsertAndGetIdAsync(product);
            await CurrentUnitOfWork.SaveChangesAsync();
            return ObjectMapper.Map<ProductDto>(product);
        }
    }
}
