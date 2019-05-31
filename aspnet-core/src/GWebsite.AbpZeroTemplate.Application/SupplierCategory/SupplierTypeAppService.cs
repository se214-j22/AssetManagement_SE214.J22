using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using GWebsite.AbpZeroTemplate.Application;
using GWebsite.AbpZeroTemplate.Application.Share.Bidding.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.MenuClients.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.SupplierType;
using GWebsite.AbpZeroTemplate.Core.Authorization;
using GWebsite.AbpZeroTemplate.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Web.Core.SupplierCategory
{
    [AbpAuthorize(GWebsitePermissions.Pages_Catalog_SupplierCatalog)]
    class SupplierTypeAppService : GWebsiteAppServiceBase, ISupplierTypeAppService
    {
        private readonly IRepository<SupplierType, int> supplierTypeRepository;

        public SupplierTypeAppService(IRepository<SupplierType, int> supplierTypeRepository)
        {
            this.supplierTypeRepository = supplierTypeRepository;
        }

        public async Task<PagedResultDto<SupplierTypeDto>> GetSupplierTypesAsync(GetMenuClientInput input)
        {
            IQueryable<SupplierType> query = this.supplierTypeRepository.GetAll();
            int totalCount = await query.CountAsync();
            List<SupplierType> items = await query.OrderBy(input.Sorting).PageBy(input).ToListAsync();
            return new PagedResultDto<SupplierTypeDto>(
             totalCount,
             items.Select(model => this.ObjectMapper.Map<SupplierTypeDto>(model)).ToList());
        }
    }
}
