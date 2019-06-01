using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using GWebsite.AbpZeroTemplate.Application;
using GWebsite.AbpZeroTemplate.Application.Share.MenuClients.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Product.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.ProductType;
using GWebsite.AbpZeroTemplate.Application.Share.ProductType.Dto;
using GWebsite.AbpZeroTemplate.Core.Authorization;
using GWebsite.AbpZeroTemplate.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Web.Core.ProductCategory
{
    //[AbpAuthorize(GWebsitePermissions.Pages_Catalog_ProductCatalog)]
    public class ProductTypeAppService : GWebsiteAppServiceBase, IProductTypeAppService
    {
        private readonly IRepository<ProductType, int> productTypeRepository;

        public ProductTypeAppService(IRepository<ProductType, int> productTypeRepository)
        {
            this.productTypeRepository = productTypeRepository;
        }

        public async Task<PagedResultDto<ProductTypeDto>> GetProductTypesAsync(ProductTypeListInputDto input)
        {
            IQueryable<ProductType> query = this.productTypeRepository
                .GetAll()
                .Where(predicate: st => input.Status == 3 || (input.Status == st.Status));

            if (!string.IsNullOrEmpty(input.Name))
            {
                query.Where(st => st.Name.Contains(input.Name)).OrderBy(st => st.Name);
            }

            if (!string.IsNullOrEmpty(input.Code))
            {
                query.Where(st => st.Code.Contains(input.Code)).OrderBy(st => st.Code);
            }

            int totalCount = await query.CountAsync();
            List<ProductType> items = await query.PageBy(input.CountSkip, input.PageSize).ToListAsync();
            return new PagedResultDto<ProductTypeDto>(
             totalCount,
             items.Select(model => this.ObjectMapper.Map<ProductTypeDto>(model)).ToList());
        }

        public async Task<ProductTypeDto> EditNameProductTypeAsync(int id, string name, string note)
        {
            ProductType current = await this.productTypeRepository
                .GetAll()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (current == null)
            {
                return null;
            }

            current.Name = name;
            current.Note = note;

            current = await this.productTypeRepository.UpdateAsync(current);
            await this.CurrentUnitOfWork.SaveChangesAsync();
            return this.ObjectMapper.Map<ProductTypeDto>(current);
        }

        public async Task<ProductTypeDto> SetStatusProductTypeAsync(int id)
        {
            ProductType current = await this.productTypeRepository
                .GetAll()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (current == null)
            {
                return null;
            }

            if (current.Status == 2)
            {
                current.Status = 1;
            }

            if (current.Status == 1)
            {
                current.Status = 2;
            }

            current = await this.productTypeRepository.UpdateAsync(current);
            await this.CurrentUnitOfWork.SaveChangesAsync();
            return this.ObjectMapper.Map<ProductTypeDto>(current);
        }

        public async Task DeleteProductTypeAsync(int id)
        {
            await this.productTypeRepository.DeleteAsync(id);
        }

        public async Task<ProductTypeDto> CreateProductTypeAsync(ProductTypeDto dto)
        {
            ProductType current = await this.productTypeRepository
                   .GetAll()
                   .FirstOrDefaultAsync(x => x.Code == dto.Code);

            if (current != null)
            {
                return null;
            }
            current = this.ObjectMapper.Map<ProductType>(dto);
            current = await this.productTypeRepository.InsertAsync(current);
            await this.CurrentUnitOfWork.SaveChangesAsync();
            return this.ObjectMapper.Map<ProductTypeDto>(current);
        }
    }
}
