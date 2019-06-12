﻿using Abp.Application.Services.Dto;
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
    [AbpAuthorize(GWebsitePermissions.Pages_Administration_ProductCatalog)]
    public class ProductTypeAppService : GWebsiteAppServiceBase, IProductTypeAppService
    {
        private readonly IRepository<ProductType, int> productTypeRepository;

        public ProductTypeAppService(IRepository<ProductType, int> productTypeRepository)
        {
            this.productTypeRepository = productTypeRepository;
        }

        public async Task<PagedResultDto<ProductTypeDto>> GetProductTypesWithFilterAsync(ProductTypeListInputDto input)
        {
            IQueryable<ProductType> query = productTypeRepository.GetAllIncluding(p => p.Products);
            if (input.Name != null)
            {
                query = query.Where(p => p.Name.Contains(input.Name));
            }
            if (input.Code != null)
            {
                query = query.Where(p => p.Code.Contains(input.Code));
            }
            if (input.Status == 1 || input.Status == 2)
            {
                query = query.Where(p => p.Status == input.Status);
            }
            int totalCount = await query.CountAsync();
            //if (totalCount == 0)
            //{
            //    query = productTypeRepository.GetAllIncluding(p => p.Products);
            //    totalCount = await query.CountAsync();
            //}
            List<ProductType> items = await query.OrderBy(input.Sorting).PageBy(input).ToListAsync();
            return new PagedResultDto<ProductTypeDto>(
            totalCount,
            items.Select(item =>
            {
                ProductTypeDto data = this.ObjectMapper.Map<ProductTypeDto>(item);
                data.IsInCludeSupplier = item.Products.Count > 0;
                return data;
            }).ToList());
        }

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_ProductCatalog_Edit)]
        public async Task<ProductTypeDto> ToggleStatusProductCatalogAsync(int id)
        {
            ProductType query = await productTypeRepository.GetAllIncluding(p => p.Products).FirstOrDefaultAsync(item => item.Id == id);
            query.Status = query.Status == 1 ? 2 : 1;
            query = await productTypeRepository.UpdateAsync(query);
            await CurrentUnitOfWork.SaveChangesAsync();
            return ObjectMapper.Map<ProductTypeDto>(query);
        }
        [AbpAuthorize(GWebsitePermissions.Pages_Administration_ProductCatalog_Delete)]
        public async Task DeleteProductCatalogAsync(int id)
        {
            ProductType query = await this.productTypeRepository.FirstOrDefaultAsync(item => item.Id == id);
            await this.productTypeRepository.DeleteAsync(query);
        }

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_ProductCatalog_Edit)]
        public async Task<ProductTypeDto> UpdateProductCatalogAsync(ProductTypeSavedDto productTypeSavedDto)
        {
            ProductType entity = await this.productTypeRepository.GetAllIncluding(p => p.Products).FirstOrDefaultAsync(item => item.Id == productTypeSavedDto.Id);
            this.ObjectMapper.Map(productTypeSavedDto, entity);
            entity = await this.productTypeRepository.UpdateAsync(entity);
            await this.CurrentUnitOfWork.SaveChangesAsync();
            return this.ObjectMapper.Map<ProductTypeDto>(entity);
        }

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_ProductCatalog_Create)]
        public async Task<ProductTypeDto> CreateProductCatalogAsync(ProductTypeSavedDto productTypeSavedDto)
        {
            ProductType productType = ObjectMapper.Map<ProductType>(productTypeSavedDto);
            await productTypeRepository.InsertAndGetIdAsync(productType);
            await CurrentUnitOfWork.SaveChangesAsync();
            return ObjectMapper.Map<ProductTypeDto>(productType);
        }
    }
}
