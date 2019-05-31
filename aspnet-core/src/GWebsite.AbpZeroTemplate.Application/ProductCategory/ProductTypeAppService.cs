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

      
     

        /// <summary>
        /// filter
        /// </summary>
        /// <param name="input"></param>
        /// <param name="code"></param>
        /// <param name="name"></param>
        /// <param name="intput"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<ProductTypeDto>> GetSupplierTypesWithFilterAsync(GetMenuClientInput input, string code, string name, int status)
        {
            var query = _productTypeRepository.GetAllIncluding(p => p.Products).Where(p => p.Name.Contains(name) || p.Code.Contains(code) || p.Status.Equals(status));
            var totalCount = await query.CountAsync();
            if (totalCount == 0)
            {
                query = _productTypeRepository.GetAllIncluding(p => p.Products);
            }
            var items = await query.OrderBy(input.Sorting).PageBy(input).ToListAsync();
            return new PagedResultDto<ProductTypeDto>(
            totalCount,
            items.Select(item =>
            {
                var data = this.ObjectMapper.Map<ProductTypeDto>(item);
                data.IsInCludeSupplier = item.Products.Count > 0;
                return data;
            }).ToList());
        }
        /// <summary>
        /// toggle status
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ProductTypeDto> ToggleStatusProductCatalogAsync(EntityDto<int> input)
        {
            var query = await _productTypeRepository.GetAllIncluding(p => p.Products).FirstOrDefaultAsync(item => item.Id == input.Id);
            query.Status = query.Status == 1 ? 2 : 1;
            query = await _productTypeRepository.UpdateAsync(query);
            await CurrentUnitOfWork.SaveChangesAsync();
            return ObjectMapper.Map<ProductTypeDto>(query);
        }

       

        /// <summary>
        /// delete supplier category
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task DeleteSupplierCatalogAsync(EntityDto<int> input)
        {
            var query = await this._productTypeRepository.FirstOrDefaultAsync(item => item.Id == input.Id);
            await this._productTypeRepository.DeleteAsync(query);
        }
        /// <summary>
        /// update suppliere category
        /// </summary>
        /// <param name="supplierTypeSavedDto"></param>
        /// <returns></returns>
        //public async Task<ProductTypeDto> UpdateSupplierCatalogAsync(SupplierTypeSavedDto supplierTypeSavedDto)
        //{
        //    var entity = await this._productTypeRepository.GetAllIncluding(p => p.Products).FirstOrDefaultAsync(item => item.Id == supplierTypeSavedDto.Id);
        //    this.ObjectMapper.Map(supplierTypeSavedDto, entity);
        //    entity = await this._productTypeRepository.UpdateAsync(entity);
        //    await this.CurrentUnitOfWork.SaveChangesAsync();
        //    return this.ObjectMapper.Map<ProductTypeDto>(entity);
        //}


        //public async Task<SupplierTypeDto> CreateSupplierCatalogAsync(SupplierTypeSavedDto supplierTypeSavedDto)
        //{
        //    var supplier = ObjectMapper.Map<SupplierType>(supplierTypeSavedDto);
        //    await supplierTypeRepository.InsertAndGetIdAsync(supplier);
        //    await CurrentUnitOfWork.SaveChangesAsync();
        //    return ObjectMapper.Map<SupplierTypeDto>(supplier);
        //}
    }
}
