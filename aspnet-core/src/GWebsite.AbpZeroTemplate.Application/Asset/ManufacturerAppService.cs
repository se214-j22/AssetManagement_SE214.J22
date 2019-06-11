using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using GWebsite.AbpZeroTemplate.Application;
using GWebsite.AbpZeroTemplate.Application.Share.Assets;
using GWebsite.AbpZeroTemplate.Application.Share.Assets.Dto;
using GWebsite.AbpZeroTemplate.Core.Authorization;
using GWebsite.AbpZeroTemplate.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Web.Core.Assets
{
    [AbpAuthorize(GWebsitePermissions.Pages_Administration_Manufacturer)]
    public class ManufacturerAppService : GWebsiteAppServiceBase, IManufacturerAppService
    {
        private readonly IRepository<Manufacturer> manufacturerRepository;
        private readonly IRepository<AssetLine> assetLineRepository;

        public ManufacturerAppService(IRepository<Manufacturer> manufacturerRepository, IRepository<AssetLine> assetLineRepository)
        {
            this.manufacturerRepository = manufacturerRepository;
            this.assetLineRepository = assetLineRepository;
        }

        public async Task<PagedResultDto<ManufacturerDto>> GetsForView(ManufacturerFilter filter)
        {
            var query = manufacturerRepository.GetAll().Where(x => !x.IsDelete);

            if (filter.Term != null)
            {
                query = query.Where(x => x.Name.ToLower().Contains(filter.Term));
            }

            var totalCount = query.Count();

            if (!string.IsNullOrWhiteSpace(filter.Sorting))
            {
                query = query.OrderBy(filter.Sorting);
            }
            if (filter.MaxResultCount > 0)
            {
                query = query.PageBy(filter);
            }

            var items = await query.AsNoTracking().ToListAsync();

            return new PagedResultDto<ManufacturerDto>(
                totalCount,
                items.Select(item => ObjectMapper.Map<ManufacturerDto>(item)).ToList());
        }

        public async Task<ManufacturerDto> GetAsyncForView(int id)
        {
                var manufacturerEntity = await manufacturerRepository.GetAll().Where(x => !x.IsDelete).AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
                if (manufacturerEntity == null)
                {
                    return null;
                }
            return ObjectMapper.Map<ManufacturerDto>(manufacturerEntity);
        }

        public async Task<ManufacturerDto> GetAsyncForView(string code)
        {
            var manufacturerEntity = await manufacturerRepository.GetAll().Where(x => !x.IsDelete).AsNoTracking().SingleOrDefaultAsync(x => x.Code == code);
            if (manufacturerEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<ManufacturerDto>(manufacturerEntity);
        }
        public async Task<ManufacturerInput> GetAsyncForEdit(int id)
        {
            var manufacturerEntity = await manufacturerRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefaultAsync(x => x.Id == id);
            if (manufacturerEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<ManufacturerInput>(manufacturerEntity);
        }

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_Manufacturer_Create_Edit)]
        public async Task CreateOrEdit(ManufacturerInput manufacturerInput)
        {
            if (manufacturerInput.Id == 0)
            {
                await CreateAsync(manufacturerInput);
            }
            else
            {
                await UpdateAsync(manufacturerInput);
            }
        }

        private async Task CreateAsync(ManufacturerInput manufacturerInput)
        {
            var manufacturerEntity = ObjectMapper.Map<Manufacturer>(manufacturerInput);
            SetAuditInsert(manufacturerEntity);
            await manufacturerRepository.InsertAsync(manufacturerEntity);
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        private async Task UpdateAsync(ManufacturerInput manufacturerInput)
        {
            var manufacturerEntity = manufacturerRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == manufacturerInput.Id);
            if (manufacturerEntity == null)
            {
            }
            ObjectMapper.Map(manufacturerInput, manufacturerEntity);
            SetAuditEdit(manufacturerEntity);
            await manufacturerRepository.UpdateAsync(manufacturerEntity);
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        public async Task<bool> HasAnyRecordsPointTo(int id)
        {
            var manufacturerEntity = manufacturerRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (manufacturerEntity != null)
            {
                return await assetLineRepository.GetAll().AnyAsync(x => x.ManufacturerID == id);
            }
            return false;
        }

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_Manufacturer_Delete)]
        public async Task DeleteAsync(int id)
        {
            var manufacturerEntity = manufacturerRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (manufacturerEntity != null)
            {
                manufacturerEntity.IsDelete = true;
                await manufacturerRepository.UpdateAsync(manufacturerEntity);
                await CurrentUnitOfWork.SaveChangesAsync();
            }
        }
    }
}