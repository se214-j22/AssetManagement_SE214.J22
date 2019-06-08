using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.Organizations;
using GSoft.AbpZeroTemplate.Authorization.Users;
using GWebsite.AbpZeroTemplate.Application;
using GWebsite.AbpZeroTemplate.Application.Share.Assets;
using GWebsite.AbpZeroTemplate.Application.Share.Assets.Dto;
using GWebsite.AbpZeroTemplate.Core.Authorization;
using GWebsite.AbpZeroTemplate.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Web.Core.Assets
{
    [AbpAuthorize(GWebsitePermissions.Pages_Administration_Asset)]
    public class AssetAppService : GWebsiteAppServiceBase, IAssetAppService
    {
        private readonly IRepository<Asset> assetRepository;
        private readonly IRepository<AssetLine> assetLineRepository;
        private readonly IRepository<AssetOrganizationUnit> assetOrganizationUnitRepository;
        private readonly IRepository<OrganizationUnit, long> organizationUnitRepository;
        private readonly IRepository<UserOrganizationUnit, long> userOrganizationUnitRepository;
        private readonly IRepository<AppUserRole> userRoleRepository;
        private readonly UserManager<User> userManager;


        public AssetAppService(IRepository<Asset> assetRepository, IRepository<AssetLine> assetLineRepository,
            IRepository<AssetOrganizationUnit> assetOrganizationUnitRepository,
            IRepository<OrganizationUnit, long> organizationUnitRepository,
        IRepository<UserOrganizationUnit, long> userOrganizationUnitRepository,
        IRepository<AppUserRole> userRoleRepository,
        UserManager<User> userManager
        )
        {
            this.assetRepository = assetRepository;
            this.assetLineRepository = assetLineRepository;
            this.assetOrganizationUnitRepository = assetOrganizationUnitRepository;
            this.organizationUnitRepository = organizationUnitRepository;
            this.userOrganizationUnitRepository = userOrganizationUnitRepository;
            this.userRoleRepository = userRoleRepository;
            this.userManager = userManager;
        }
        public async Task<IQueryable<Asset>> GetAssetsOfCurrentUser()
        {


            IList<long> ouIds;


            var user = GetCurrentUser();
            var isInAdminRole= await userManager.IsInRoleAsync(user, "Admin");
            if(isInAdminRole)
            {
                ouIds = await (from o in organizationUnitRepository.GetAll()
                               select o.Id).ToListAsync();

            }
            else
            {
                var organizationUnitId = userOrganizationUnitRepository.FirstOrDefault(uo => uo.UserId == user.Id)?.OrganizationUnitId;
                if (organizationUnitId == null)
                    return null;

                ouIds = await (from o in organizationUnitRepository.GetAll()
                               where (o.ParentId == organizationUnitId || o.Id == organizationUnitId)
                               select o.Id).ToListAsync();
            }

            return (from po in assetOrganizationUnitRepository.GetAll()
                                          where ouIds.Contains(po.OrganizationUnitId)
                                          select po.Asset);
        }
        public async Task<PagedResultDto<AssetDto>> GetsForView(AssetFilter filter)
        {
            var query = (await GetAssetsOfCurrentUser()).Where(x => !x.IsDelete);
            if (filter.AssetLineId > 0)
            {
                query = query.Where(x => x.AssetLineId == filter.AssetLineId);
            }

            if (filter.Term != null)
            {
                query = query.Where(x => x.Code.ToLower().Contains(filter.Term));
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
           
            var items = await query.AsNoTracking().Include(i => i.AssetLine).ThenInclude(al => al.AssetType)
                .Include(i => i.AssetLine).ThenInclude(al => al.Manufacturer).ToListAsync();
            var items_convert = items.Select(item => ObjectMapper.Map<AssetDto>(item)).ToList();
            foreach(var item in items_convert)
            {
                item.OrganizationUnitId= (await assetOrganizationUnitRepository.GetAll().FirstOrDefaultAsync(ao => ao.AssetId == item.Id)).OrganizationUnitId;
            }
            return new PagedResultDto<AssetDto>(
                totalCount,
                items_convert);

        }


        public async Task<AssetDto> GetAsyncForView(int id)
        {
            var assetEntity = await assetRepository.GetAll().Where(x => !x.IsDelete).AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id);

            if (assetEntity == null)
            {
                return null;
            }
            assetEntity.AssetLine = this.assetLineRepository.GetAll().Where(x => !x.IsDelete).AsNoTracking()
               .Include(b => b.AssetType).Include(b => b.Manufacturer).SingleOrDefault(x => x.Id == assetEntity.AssetLineId);
            var result = ObjectMapper.Map<AssetDto>(assetEntity);
            result.OrganizationUnitId = (await assetOrganizationUnitRepository.GetAll().FirstOrDefaultAsync(ao => ao.AssetId == result.Id)).OrganizationUnitId;
            return result;
        }

        public async Task<AssetDto> GetAsyncForView(string code)
        {
            var assetEntity = await assetRepository.GetAll().Where(x => !x.IsDelete).AsNoTracking()
                 .SingleOrDefaultAsync(x => x.Code == code);
            if (assetEntity == null)
            {
                return null;
            }
            assetEntity.AssetLine = this.assetLineRepository.GetAll().Where(x => !x.IsDelete).AsNoTracking()
              .Include(b => b.AssetType).Include(b => b.Manufacturer).SingleOrDefault(x => x.Id == assetEntity.AssetLineId);

            var result = ObjectMapper.Map<AssetDto>(assetEntity);
            result.OrganizationUnitId = (await assetOrganizationUnitRepository.GetAll().FirstOrDefaultAsync(ao => ao.AssetId == result.Id)).OrganizationUnitId;
            return result;
        }
        public async Task<AssetInput> GetForEdit(int id)
        {
            var assetEntity = await assetRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefaultAsync(x => x.Id == id);

            if (assetEntity == null)
            {
                return null;
            }
            var result = ObjectMapper.Map<AssetInput>(assetEntity);
            result.OrganizationUnitId = (await assetOrganizationUnitRepository.GetAll().FirstOrDefaultAsync(ao => ao.AssetId == result.Id)).OrganizationUnitId;
            return result;
        }

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_Asset_Create_Edit)]
        public async Task CreateOrEdit(AssetInput assetInput)
        {
            if (assetInput.Id == 0)
            {
                await CreateAsync(assetInput);
            }
            else
            {
                await UpdateAsync(assetInput);
            }
        }

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_Asset_Create_Edit)]
        public async Task CreateAsync(AssetInput assetInput)
        {
            var assetEntity = ObjectMapper.Map<Asset>(assetInput);
            var assetLineEntity = this.assetLineRepository.GetAll().Where(x => !x.IsDelete).AsNoTracking()
               .Include(b => b.AssetType).Include(b => b.Manufacturer).SingleOrDefault(x => x.Id == assetInput.AssetLineID);
            if (assetLineEntity == null)
                throw new ArgumentException("AssetLine is not existed!");
            Random random = new Random();
            var randomNumber = random.Next(0, 999999).ToString("D6");
            //todo:check existed 
            assetEntity.Code = string.Concat(assetLineEntity.AssetType.Code, assetLineEntity.Manufacturer.Code, assetLineEntity.Code, randomNumber);
            SetAuditInsert(assetEntity);
            await assetRepository.InsertAsync(assetEntity);

            var user = GetCurrentUser();
            var organizationUnitId = userOrganizationUnitRepository.FirstOrDefault(uo => uo.UserId == user.Id)?.OrganizationUnitId;
            if (organizationUnitId != null)
            {
                var assetOrganizationUnit = new AssetOrganizationUnit() { AssetId = assetEntity.Id, OrganizationUnitId = (long)organizationUnitId };
                await this.assetOrganizationUnitRepository.InsertAsync(assetOrganizationUnit);
            }
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        private async Task UpdateAsync(AssetInput assetInput)
        {
            var assetEntity = assetRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == assetInput.Id);
            if (assetEntity == null)
            {
            }
            ObjectMapper.Map(assetInput, assetEntity);
            SetAuditEdit(assetEntity);
            await assetRepository.UpdateAsync(assetEntity);
            if(assetInput.OrganizationUnitId>0)
            {
                var isExisting= assetOrganizationUnitRepository.GetAll()
                    .Where(x => x.AssetId == assetInput.Id).Any();
                if(isExisting)
                {
                    await assetOrganizationUnitRepository.GetAll()
                    .Where(x => x.AssetId == assetInput.Id).ForEachAsync(p => p.OrganizationUnitId = assetInput.OrganizationUnitId);
                }
                else
                {
                    var newAssetOrganizationUnit = new AssetOrganizationUnit() { AssetId = assetEntity.Id, OrganizationUnitId = assetInput.OrganizationUnitId };
                    await assetOrganizationUnitRepository.InsertAsync(newAssetOrganizationUnit);
                }
            }
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_Asset_Delete)]
        public async Task DeleteAsync(int id)
        {
            var assetEntity = assetRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (assetEntity != null)
            {
                assetEntity.IsDelete = true;
                await assetRepository.UpdateAsync(assetEntity);
                await CurrentUnitOfWork.SaveChangesAsync();
            }
        }
    }
}