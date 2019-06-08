using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.Organizations;
using GSoft.AbpZeroTemplate.Authorization;
using GSoft.AbpZeroTemplate.Organizations.Dto;
using System.Linq.Dynamic.Core;
using Abp.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;
using Abp.Collections.Extensions;
using GWebsite.AbpZeroTemplate.Core.Models;

namespace GSoft.AbpZeroTemplate.Organizations
{
    [AbpAuthorize(AppPermissions.Pages_Administration_OrganizationUnits)]
    public class OrganizationUnitAppService : AbpZeroTemplateAppServiceBase, IOrganizationUnitAppService
    {
        private readonly OrganizationUnitManager _organizationUnitManager;
        private readonly IRepository<OrganizationUnit, long> _organizationUnitRepository;
        private readonly IRepository<UserOrganizationUnit, long> _userOrganizationUnitRepository;
        private readonly IRepository<AssetOrganizationUnit, int> _assetOrganizationUnitRepository;

        public OrganizationUnitAppService(
            OrganizationUnitManager organizationUnitManager,
            IRepository<OrganizationUnit, long> organizationUnitRepository,
            IRepository<UserOrganizationUnit, long> userOrganizationUnitRepository,
            IRepository<AssetOrganizationUnit, int> assetOrganizationUnitRepository)
        {
            _organizationUnitManager = organizationUnitManager;
            _organizationUnitRepository = organizationUnitRepository;
            _userOrganizationUnitRepository = userOrganizationUnitRepository;
            _assetOrganizationUnitRepository = assetOrganizationUnitRepository;
        }
        public async Task<OrganizationUnitDto> GetOrganizationUnit()
        {
            var user = GetCurrentUser();
            var organizationUnitId = _userOrganizationUnitRepository.FirstOrDefault(uo => uo.UserId == user.Id)?.OrganizationUnitId;
            if (organizationUnitId == null)
                return null;
            var ou = await _organizationUnitRepository.GetAll().FirstOrDefaultAsync(o=>o.Id == organizationUnitId);
            if (ou == null)
                return null;

            return ObjectMapper.Map<OrganizationUnitDto>(ou);
        }

        public async Task<ListResultDto<OrganizationUnitDto>> GetOrganizationUnits()
        {
            var user = GetCurrentUser();
            if (true) //not admin
            {
                var organizationUnitId = _userOrganizationUnitRepository.FirstOrDefault(uo => uo.UserId == user.Id)?.OrganizationUnitId;
                if (organizationUnitId == null)
                    return null;

                var ouIds = await (from o in _organizationUnitRepository.GetAll()
                                           where (o.Id == organizationUnitId || o.ParentId == organizationUnitId)
                                           select o.Id).ToListAsync();

                ouIds.Add((long)organizationUnitId);
                var query =
                    from ou in _organizationUnitRepository.GetAll() where ouIds.Contains(ou.Id)
                    join uou in _userOrganizationUnitRepository.GetAll() on ou.Id equals uou.OrganizationUnitId into g

                    select new { ou, memberCount = g.Count() };
                var items = await query.ToListAsync();
                return new ListResultDto<OrganizationUnitDto>(
             items.Select(item =>
             {
                 var dto = ObjectMapper.Map<OrganizationUnitDto>(item.ou);
                 dto.MemberCount = item.memberCount;
                 return dto;
             }).ToList());
            }
            else //admin
            {
                var query =
           from ou in _organizationUnitRepository.GetAll()
           join uou in _userOrganizationUnitRepository.GetAll() on ou.Id equals uou.OrganizationUnitId into g

           select new { ou, memberCount = g.Count() };
                var items = await query.ToListAsync();
                return new ListResultDto<OrganizationUnitDto>(
             items.Select(item =>
             {
                 var dto = ObjectMapper.Map<OrganizationUnitDto>(item.ou);
                 dto.MemberCount = item.memberCount;
                 return dto;
             }).ToList());
            }
        }

        public async Task<PagedResultDto<OrganizationUnitUserListDto>> GetOrganizationUnitUsers(GetOrganizationUnitUsersInput input)
        {
            var query = from uou in _userOrganizationUnitRepository.GetAll()
                        join ou in _organizationUnitRepository.GetAll() on uou.OrganizationUnitId equals ou.Id
                        join user in UserManager.Users on uou.UserId equals user.Id
                        where uou.OrganizationUnitId == input.Id
                        select new { uou, user };
            var totalCount = await query.CountAsync();

            var items = await query.OrderBy(input.Sorting).PageBy(input).ToListAsync();

            return new PagedResultDto<OrganizationUnitUserListDto>(
                totalCount,
                items.Select(item =>
                {
                    var dto = ObjectMapper.Map<OrganizationUnitUserListDto>(item.user);
                    dto.AddedTime = item.uou.CreationTime;
                    return dto;
                }).ToList());
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_OrganizationUnits_ManageOrganizationTree)]
        public async Task<OrganizationUnitDto> CreateOrganizationUnit(CreateOrganizationUnitInput input)
        {
            var organizationUnit = new OrganizationUnit(AbpSession.TenantId, input.DisplayName, input.ParentId);

            await _organizationUnitManager.CreateAsync(organizationUnit);
            await CurrentUnitOfWork.SaveChangesAsync();

            return ObjectMapper.Map<OrganizationUnitDto>(organizationUnit);
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_OrganizationUnits_ManageOrganizationTree)]
        public async Task<OrganizationUnitDto> UpdateOrganizationUnit(UpdateOrganizationUnitInput input)
        {
            var organizationUnit = await _organizationUnitRepository.GetAsync(input.Id);

            organizationUnit.DisplayName = input.DisplayName;

            await _organizationUnitManager.UpdateAsync(organizationUnit);

            return await CreateOrganizationUnitDto(organizationUnit);
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_OrganizationUnits_ManageOrganizationTree)]
        public async Task<OrganizationUnitDto> MoveOrganizationUnit(MoveOrganizationUnitInput input)
        {
            await _organizationUnitManager.MoveAsync(input.Id, input.NewParentId);

            return await CreateOrganizationUnitDto(
                await _organizationUnitRepository.GetAsync(input.Id)
                );
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_OrganizationUnits_ManageOrganizationTree)]
        public async Task DeleteOrganizationUnit(EntityDto<long> input)
        {
            await _organizationUnitManager.DeleteAsync(input.Id);
        }


        [AbpAuthorize(AppPermissions.Pages_Administration_OrganizationUnits_ManageMembers)]
        public async Task RemoveUserFromOrganizationUnit(UserToOrganizationUnitInput input)
        {
            await UserManager.RemoveFromOrganizationUnitAsync(input.UserId, input.OrganizationUnitId);
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_OrganizationUnits_ManageMembers)]
        public async Task AddUsersToOrganizationUnit(UsersToOrganizationUnitInput input)
        {
            foreach (var userId in input.UserIds)
            {
                await UserManager.AddToOrganizationUnitAsync(userId, input.OrganizationUnitId);
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_OrganizationUnits_ManageMembers)]
        public async Task<PagedResultDto<NameValueDto>> FindUsers(FindOrganizationUnitUsersInput input)
        {
            var userIdsInOrganizationUnit = _userOrganizationUnitRepository.GetAll()
                .Where(uou => uou.OrganizationUnitId == input.OrganizationUnitId)
                .Select(uou => uou.UserId);

            var query = UserManager.Users
                .Where(u => !userIdsInOrganizationUnit.Contains(u.Id))
                .WhereIf(
                    !input.Filter.IsNullOrWhiteSpace(),
                    u =>
                        u.Name.Contains(input.Filter) ||
                        u.Surname.Contains(input.Filter) ||
                        u.UserName.Contains(input.Filter) ||
                        u.EmailAddress.Contains(input.Filter)
                );

            var userCount = await query.CountAsync();
            var users = await query
                .OrderBy(u => u.Name)
                .ThenBy(u => u.Surname)
                .PageBy(input)
                .ToListAsync();

            return new PagedResultDto<NameValueDto>(
                userCount,
                users.Select(u =>
                    new NameValueDto(
                        u.FullName + " (" + u.EmailAddress + ")",
                        u.Id.ToString()
                    )
                ).ToList()
            );
        }

        private async Task<OrganizationUnitDto> CreateOrganizationUnitDto(OrganizationUnit organizationUnit)
        {
            var dto = ObjectMapper.Map<OrganizationUnitDto>(organizationUnit);
            dto.MemberCount = await _userOrganizationUnitRepository.CountAsync(uou => uou.OrganizationUnitId == organizationUnit.Id);
            return dto;
        }

        public async Task<PagedResultDto<OrganizationUnitAssetListDto>> GetAssets(GetOrganizationUnitAssetsInput input)
        {
            // validation: check whether organization belongs to current user

            var assetsInWarehouseQuery = (from po in _assetOrganizationUnitRepository.GetAll()
                                          where po.OrganizationUnitId == input.Id && po.Asset.IsDelete==false
                                          select po.Asset).AsNoTracking();

            var items = await assetsInWarehouseQuery.OrderBy(input.Sorting).PageBy(input).ToListAsync();

            return new PagedResultDto<OrganizationUnitAssetListDto>(
                await assetsInWarehouseQuery.CountAsync(),
                items.Select(item =>
                {
                    var dto = ObjectMapper.Map<OrganizationUnitAssetListDto>(item);
                    return dto;
                }).ToList());
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_OrganizationUnits_ManageWarehouse)]
        public async Task<PagedResultDto<NameValueDto>> FindAssets(FindOrganizationUnitAssetsInput input)
        {
            var assetIdsInOrganizationUnit = _assetOrganizationUnitRepository.GetAll()
                .Where(aou => aou.OrganizationUnitId == input.OrganizationUnitId)
                .Select(aou => aou.AssetId);

            #region assetsInWarehouseQuery
            var user = GetCurrentUser();
            var organizationUnitId = _userOrganizationUnitRepository.FirstOrDefault(uo => uo.UserId == user.Id)?.OrganizationUnitId;
            if (organizationUnitId == null)
                return null;

            IList<long> ouIds;
            //if (containsChildOrganizationUnit)
            //{
                ouIds = await (from o in _organizationUnitRepository.GetAll()
                               where (o.Id == organizationUnitId || o.ParentId == organizationUnitId)
                               select o.Id).ToListAsync();
            //}
            //else
            //{
            //    ouIds = await (from o in _organizationUnitRepository.GetAll()
            //                   where (o.Id == organizationUnitId)
            //                   select o.Id).ToListAsync();
            //}

            var assetsInWarehouseQuery = (from po in _assetOrganizationUnitRepository.GetAll()
                    where ouIds.Contains(po.OrganizationUnitId)
                    select po.Asset).AsNoTracking();
            #endregion
            var query = assetsInWarehouseQuery
                .Where(a => !assetIdsInOrganizationUnit.Contains(a.Id))
                .WhereIf(
                    !input.Filter.IsNullOrWhiteSpace(),
                    u =>
                        u.Code.Contains(input.Filter)
                );

            var assetCount = await query.CountAsync();
            var assets = await query
                .OrderBy(u => u.Id)
                .PageBy(input)
                .ToListAsync();

            return new PagedResultDto<NameValueDto>(
                assetCount,
                assets.Select(u =>
                    new NameValueDto(
                        u.Code,
                        u.Id.ToString()
                    )
                ).ToList()
            );
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_OrganizationUnits_ManageWarehouse)]
        public async Task PlaceAssetsToOrganizationUnit(AssetsToOrganizationUnitInput input)
        {
            var existedAssetIds = await _assetOrganizationUnitRepository.GetAll()
                .Where(x => input.AssetIds.Contains(x.AssetId))
                .Select(p => p.AssetId).ToListAsync();
            var notExistedAssetIds = input.AssetIds.Except(existedAssetIds).ToList();

            await _assetOrganizationUnitRepository.GetAll()
                .Where(x => existedAssetIds.Contains(x.AssetId))
                .ForEachAsync(p => p.OrganizationUnitId = input.OrganizationUnitId);
            foreach (int productId in notExistedAssetIds)
            {
                var newProduct = new AssetOrganizationUnit() { AssetId = productId, OrganizationUnitId = input.OrganizationUnitId };
                await _assetOrganizationUnitRepository.InsertAsync(newProduct);
            }
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_OrganizationUnits_ManageWarehouse)]
        public async Task<WarehouseStatus> GetWarehouseStatus()
        {
            var user = GetCurrentUser();
            var organizationUnitId = (await _userOrganizationUnitRepository.FirstOrDefaultAsync(uo => uo.UserId == user.Id))?.OrganizationUnitId;
            if(organizationUnitId == null)  //this case occurs when current user granted organization unit permission don't have organization unit.
                return new WarehouseStatus { AllNumber = 0, RestingNumber = 0, DamagedNumber = 0, UsingNumber = 0 };
            var childrendOrgIds = from o in _organizationUnitRepository.GetAll() where (o.ParentId == organizationUnitId) select o.Id;
            var usingNumber = (from po in _assetOrganizationUnitRepository.GetAll()
                                   where childrendOrgIds.Contains(po.OrganizationUnitId) && po.Asset.IsDamaged == false && po.Asset.IsDelete == false
                               select po).Count();
            var allNumber = (from po in _assetOrganizationUnitRepository.GetAll()
                                where (organizationUnitId == po.OrganizationUnitId || childrendOrgIds.Contains(po.OrganizationUnitId)) && po.Asset.IsDelete == false
                                 select po).Count();
            var damagedNumber = (from po in _assetOrganizationUnitRepository.GetAll()
                                 where (organizationUnitId == po.OrganizationUnitId|| childrendOrgIds.Contains(po.OrganizationUnitId)) && po.Asset.IsDamaged == true && po.Asset.IsDelete == false
                                 select po.Asset).Count();
            return new WarehouseStatus { AllNumber = allNumber, RestingNumber = allNumber - usingNumber - damagedNumber, DamagedNumber = damagedNumber, UsingNumber = usingNumber };
        }

    }
}