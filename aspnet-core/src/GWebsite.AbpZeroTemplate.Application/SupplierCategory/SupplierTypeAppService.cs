using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using GWebsite.AbpZeroTemplate.Application;
using GWebsite.AbpZeroTemplate.Application.Share.Bidding.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.MenuClients.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.SupplierType;
using GWebsite.AbpZeroTemplate.Application.Share.SupplierType.Dto;
using GWebsite.AbpZeroTemplate.Core.Authorization;
using GWebsite.AbpZeroTemplate.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Web.Core.SupplierCategory
{
    //[AbpAuthorize(GWebsitePermissions.Pages_Catalog_SupplierCatalog)]
    public class SupplierTypeAppService : GWebsiteAppServiceBase, ISupplierTypeAppService
    {
        private readonly IRepository<SupplierType, int> supplierTypeRepository;

        public SupplierTypeAppService(IRepository<SupplierType, int> supplierTypeRepository)
        {
            this.supplierTypeRepository = supplierTypeRepository;
        }

        public async Task<PagedResultDto<SupplierTypeDto>> GetSupplierTypesAsync(SupplierTypeListInputDto input)
        {
            IQueryable<SupplierType> query = this.supplierTypeRepository
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
            List<SupplierType> items = await query.PageBy(input).ToListAsync();
            return new PagedResultDto<SupplierTypeDto>(
             totalCount,
             items.Select(model => this.ObjectMapper.Map<SupplierTypeDto>(model)).ToList());
        }

        public async Task<SupplierTypeDto> EditNameSupplierTypeAsync(int id, string name, string note)
        {
            SupplierType current = await this.supplierTypeRepository
                .GetAll()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (current == null)
            {
                return null;
            }

            current.Name = name;
            current.Note = note;

            current = await this.supplierTypeRepository.UpdateAsync(current);
            await this.CurrentUnitOfWork.SaveChangesAsync();
            return this.ObjectMapper.Map<SupplierTypeDto>(current);
        }

        public async Task<SupplierTypeDto> SetStatusSupplierTypeAsync(int id)
        {
            SupplierType current = await this.supplierTypeRepository
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
            else if (current.Status == 1)
            {
                current.Status = 2;
            }

            current = await this.supplierTypeRepository.UpdateAsync(current);
            await this.CurrentUnitOfWork.SaveChangesAsync();
            return this.ObjectMapper.Map<SupplierTypeDto>(current);
        }

        public async Task DeleteSupplierTypeAsync(int id)
        {
            await this.supplierTypeRepository.DeleteAsync(id);
        }

        public async Task<SupplierTypeDto> CreateSupplierTypeAsync(SupplierTypeDto dto)
        {
            SupplierType current = await this.supplierTypeRepository
                   .GetAll()
                   .FirstOrDefaultAsync(x => x.Code == dto.Code);

            if (current != null)
            {
                return null;
            }
            current = this.ObjectMapper.Map<SupplierType>(dto);
            current = await this.supplierTypeRepository.InsertAsync(current);
            await this.CurrentUnitOfWork.SaveChangesAsync();
            return this.ObjectMapper.Map<SupplierTypeDto>(current);
        }
    }
}
