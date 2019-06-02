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

namespace GWebsite.AbpZeroTemplate.Web.Core.AssetTypes
{
    [AbpAuthorize(GWebsitePermissions.Pages_Administration_AssetType)]
    public class AssetTypeAppService : GWebsiteAppServiceBase, IAssetTypeAppService
    {
        private readonly IRepository<AssetType> assetTypeRepository;
        private readonly IRepository<AssetLine> assetLineRepository;

        public AssetTypeAppService(IRepository<AssetType> assetTypeRepository, IRepository<AssetLine> assetLineRepository)
        {
            this.assetTypeRepository = assetTypeRepository;
            this.assetLineRepository = assetLineRepository;
        }

        public async Task<PagedResultDto<AssetTypeDto>> GetsForView(AssetTypeFilter filter)
        {
            var query = assetTypeRepository.GetAll().Where(x => !x.IsDelete).AsNoTracking();
           
            if (filter.Term != null)
            {
                query = query.Where(x => x.Name.ToLower().Contains(filter.Term));
            }

            var totalCount = query.Count();

            if (!string.IsNullOrWhiteSpace(filter.Sorting))
            {
                query = query.OrderBy(filter.Sorting);
            }

            var items = await query.PageBy(filter).ToListAsync();

            return new PagedResultDto<AssetTypeDto>(
                totalCount,
                items.Select(item => ObjectMapper.Map<AssetTypeDto>(item)).ToList());
        }

        public async Task<AssetTypeDto> GetAsyncForView(int id)
        {
                var assetTypeEntity = await assetTypeRepository.GetAll().Where(x => !x.IsDelete).AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
                if (assetTypeEntity == null)
                {
                    return null;
                }
            return ObjectMapper.Map<AssetTypeDto>(assetTypeEntity);
        }

        public async Task<AssetTypeDto> GetAsyncForView(string code)
        {
            var assetTypeEntity = await assetTypeRepository.GetAll().Where(x => !x.IsDelete).AsNoTracking().SingleOrDefaultAsync(x => x.Code == code);
            if (assetTypeEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<AssetTypeDto>(assetTypeEntity);
        }
        public async Task<AssetType> GetAsyncForEdit(string code)
        {
            var assetTypeEntity = await assetTypeRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefaultAsync(x => x.Code == code);
            if (assetTypeEntity == null)
            {
                return null;
            }
            return assetTypeEntity;
        }

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_AssetType_Create_Edit)]
        public async Task CreateOrEdit(AssetTypeInput assetTypeInput)
        {
            if (assetTypeInput.Id == 0)
            {
                await CreateAsync(assetTypeInput);
            }
            else
            {
                await UpdateAsync(assetTypeInput);
            }
        }
        private async Task CreateAsync(AssetTypeInput assetTypeInput)
        {
            var assetTypeEntity = ObjectMapper.Map<AssetType>(assetTypeInput);
            SetAuditInsert(assetTypeEntity);
            await assetTypeRepository.InsertAsync(assetTypeEntity);
            await CurrentUnitOfWork.SaveChangesAsync();
        }
        private async Task UpdateAsync(AssetTypeInput assetTypeInput)
        {
            var assetTypeEntity = assetTypeRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == assetTypeInput.Id);
            if (assetTypeEntity == null)
            {
            }
            ObjectMapper.Map(assetTypeInput, assetTypeEntity);
            SetAuditEdit(assetTypeEntity);
            await assetTypeRepository.UpdateAsync(assetTypeEntity);
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        public async Task<bool> HasAnyRecordsPointTo(int id)
        {
            var assetTypeEntity = assetTypeRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (assetTypeEntity != null)
            {
                return await assetLineRepository.GetAll().AnyAsync(x => x.AssetTypeID == id);
            }
            return false;
        }

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_AssetType_Delete)]
        public async Task DeleteAsync(int id)
        {
            var assetTypeEntity = assetTypeRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (assetTypeEntity != null)
            {
                assetTypeEntity.IsDelete = true;
                await assetTypeRepository.UpdateAsync(assetTypeEntity);
                await CurrentUnitOfWork.SaveChangesAsync();
            }
        }
    }
}