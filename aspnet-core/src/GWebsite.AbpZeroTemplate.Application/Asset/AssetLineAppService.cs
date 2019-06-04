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
    [AbpAuthorize(GWebsitePermissions.Pages_Administration_AssetLine)]
    public class AssetLineAppService : GWebsiteAppServiceBase, IAssetLineAppService
    {
        private readonly IRepository<AssetLine> assetLineRepository;
        private readonly IRepository<Asset> assetRepository;

        public AssetLineAppService(IRepository<AssetLine> assetLineRepository, IRepository<Asset> assetRepository)
        {
            this.assetLineRepository = assetLineRepository;
            this.assetRepository = assetRepository;
        }

        public async Task<PagedResultDto<AssetLineDto>> GetsForView(AssetLineFilter filter)
        {
            var query = assetLineRepository.GetAll().Where(x => !x.IsDelete).AsNoTracking();
            if (filter.AssetTypeId>0)
            {
                query = query.Where(x => x.AssetTypeID == filter.AssetTypeId);
            }
            if (filter.ManufacturerId > 0)
            {
                query = query.Where(x => x.ManufacturerID == filter.ManufacturerId);
            }
            if (filter.Term != null)
            {
                query = query.Where(x => x.Name.ToLower().Contains(filter.Term));
            }

            var totalCount = query.Count();

            if (!string.IsNullOrWhiteSpace(filter.Sorting))
            {
                query = query.OrderBy(filter.Sorting);
            }
            if (filter.MaxResultCount>0)
            {
                query = query.PageBy(filter);
            }
            var items = await query.Include(b => b.AssetType).Include(b => b.Manufacturer).ToListAsync();
            return new PagedResultDto<AssetLineDto>(
                totalCount,
                items.Select(item => ObjectMapper.Map<AssetLineDto>(item)).ToList());
        }

        public async Task<AssetLineDto> GetAsyncForView(int id)
        {
            var assetLineEntity = await assetLineRepository.GetAll().Where(x => !x.IsDelete).AsNoTracking()
                .Include(b => b.AssetType).Include(b => b.Manufacturer).SingleOrDefaultAsync(x => x.Id == id);
            if (assetLineEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<AssetLineDto>(assetLineEntity);
        }

        public async Task<AssetLineDto> GetAsyncForView(string code)
        {
            var assetLineEntity = await assetLineRepository.GetAll().Where(x => !x.IsDelete).AsNoTracking().SingleOrDefaultAsync(x => x.Code == code);
            if (assetLineEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<AssetLineDto>(assetLineEntity);
        }
        public async Task<AssetLine> GetAsyncForEdit(string code)
        {
            var assetLineEntity = await assetLineRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefaultAsync(x => x.Code == code);
            if (assetLineEntity == null)
            {
                return null;
            }
            return assetLineEntity;
        }

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_AssetLine_Create_Edit)]
        public async Task CreateOrEdit(AssetLineInput assetLineInput)
        {
            if (assetLineInput.Id == 0)
            {
                await CreateAsync(assetLineInput);
            }
            else
            {
                await UpdateAsync(assetLineInput);
            }
        }
        private async Task CreateAsync(AssetLineInput assetLineInput)
        {
            var assetLineEntity = ObjectMapper.Map<AssetLine>(assetLineInput);
            SetAuditInsert(assetLineEntity);
            await assetLineRepository.InsertAsync(assetLineEntity);
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        private async Task UpdateAsync(AssetLineInput assetLineInput)
        {
            var assetLineEntity = assetLineRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == assetLineInput.Id);
            if (assetLineEntity == null)
            {
            }
            ObjectMapper.Map(assetLineInput, assetLineEntity);
            SetAuditEdit(assetLineEntity);
            await assetLineRepository.UpdateAsync(assetLineEntity);
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        public async Task<bool> HasAnyRecordsPointTo(int id)
        {
            var assetLineEntity = assetLineRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (assetLineEntity != null)
            {
                return await assetRepository.GetAll().AnyAsync(x => x.AssetLineId == id);
            }
            return false;
        }

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_AssetLine_Delete)]
        public async Task DeleteAsync(int id)
        {
            var assetLineEntity = assetLineRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (assetLineEntity != null)
            {
                assetLineEntity.IsDelete = true;
                await assetLineRepository.UpdateAsync(assetLineEntity);
                await CurrentUnitOfWork.SaveChangesAsync();
            }
        }
    }
}