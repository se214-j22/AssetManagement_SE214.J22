using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using GWebsite.AbpZeroTemplate.Application;
using GWebsite.AbpZeroTemplate.Application.Share.Products;
using GWebsite.AbpZeroTemplate.Application.Share.Products.Dto;
using GWebsite.AbpZeroTemplate.Core.Authorization;
using GWebsite.AbpZeroTemplate.Core.Models;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Web.Core.Products
{
    [AbpAuthorize(GWebsitePermissions.Pages_Administration_Product)]
    public class ProductAppService : GWebsiteAppServiceBase, IProductAppService
    {
        private readonly IRepository<Product> productRepository;

        public ProductAppService(IRepository<Product> productRepository)
        {
            this.productRepository = productRepository;
        }

        //[AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient_Create)]
        //public void Create(ProductInput productInput)
        //{
        //    var productEntity = ObjectMapper.Map<Product>(productInput);
        //    //SetAuditInsert(productEntity);
        //    productRepository.Insert(productEntity);
        //    CurrentUnitOfWork.SaveChanges();
        //}

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_Product_Create_Edit)]
        public async Task CreateOrEdit(ProductInput productInput)
        {
            if (productInput.Id == 0)
            {
                await CreateAsync(productInput);
            }
            else
            {
                await UpdateAsync(productInput);
            }
        }
        private async Task CreateAsync(ProductInput productInput)
        {
            var productEntity = ObjectMapper.Map<Product>(productInput);
            SetAuditInsert(productEntity);
            await productRepository.InsertAsync(productEntity);
            await CurrentUnitOfWork.SaveChangesAsync();
        }
        private async Task UpdateAsync(ProductInput productInput)
        {
            var productEntity = productRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == productInput.Id);
            if (productEntity == null)
            {
            }
            ObjectMapper.Map(productInput, productEntity);
            SetAuditEdit(productEntity);
            await productRepository.UpdateAsync(productEntity);
            await CurrentUnitOfWork.SaveChangesAsync();
        }
    }
}