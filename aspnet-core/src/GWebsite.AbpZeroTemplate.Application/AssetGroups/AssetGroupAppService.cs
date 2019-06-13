using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using AutoMapper.QueryableExtensions;
using GWebsite.AbpZeroTemplate.Application;
using GWebsite.AbpZeroTemplate.Application.Share.AssetGroups;
using GWebsite.AbpZeroTemplate.Application.Share.AssetGroups.Dto;
using GWebsite.AbpZeroTemplate.Core.Authorization;
using GWebsite.AbpZeroTemplate.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace GWebsite.AbpZeroTemplate.Web.Core.AssetGroups
{
    [AbpAuthorize(GWebsitePermissions.Pages_Administration_AssetGroup)]
    public class AssetGroupAppService : GWebsiteAppServiceBase, IAssetGroupAppService
    {
        private readonly IRepository<AssetGroup> assetGroupRepository;

        public AssetGroupAppService(IRepository<AssetGroup> assetGroupRepository)
        {
            this.assetGroupRepository = assetGroupRepository;
        }

        #region Public Method

        public void CreateOrEditAssetGroup(AssetGroupInput assetGroupInput)
        {
            if (assetGroupInput.Id == 0)
            {
                Create(assetGroupInput);
            }
            else
            {
                Update(assetGroupInput);
            }
        }
        [AbpAuthorize(GWebsitePermissions.Pages_Administration_AssetGroup_Delete)]
        public void DeleteAssetGroup(int id)
        {
            var assetGroupEntity = assetGroupRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (assetGroupEntity != null)
            {
                assetGroupEntity.IsDelete = true;
                assetGroupRepository.Update(assetGroupEntity);
                CurrentUnitOfWork.SaveChanges();
            }
        }

        public AssetGroupInput GetAssetGroupForEdit(int id)
        {
            var assetGroupEntity = assetGroupRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (assetGroupEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<AssetGroupInput>(assetGroupEntity);
        }

        public AssetGroupForViewDto GetAssetGroupForView(int id)
        {
            var assetGroupEntity = assetGroupRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (assetGroupEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<AssetGroupForViewDto>(assetGroupEntity);
        }

        public PagedResultDto<AssetGroupDto> GetAssetGroups(AssetGroupFilter input)
        {
            var query = assetGroupRepository.GetAll().Where(x => !x.IsDelete);

            // filter by value
            if (input.AssetGroupName != null)
            {
                query = query.Where(x => x.AssetGroupName.ToLower().Equals(input.AssetGroupName));
            }

            var totalCount = query.Count();

            // sorting
            if (!string.IsNullOrWhiteSpace(input.Sorting))
            {
                query = query.OrderBy(input.Sorting);
            }

            // paging
            var items = query.PageBy(input).ToList();

            // result
            return new PagedResultDto<AssetGroupDto>(
                totalCount,
                items.Select(item => ObjectMapper.Map<AssetGroupDto>(item)).ToList());
        }

        public List<AssetGroupDto> GetListAssetGroups(string assetGrouptId)
        {
            IQueryable<AssetGroup> query = assetGroupRepository.GetAll().Where(x => !x.IsDelete).Where(x => x.AssetGrouptId != assetGrouptId);
            IQueryable<AssetGroupDto> assetGroupDtoQuery = query.ProjectTo<AssetGroupDto>(query);
            return assetGroupDtoQuery.ToList();
        }

        public string GetAssetGroupNameByAssetID(string assetGrouptId)
        {
            var assetGroupEntity = assetGroupRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.AssetGrouptId.ToLower().Equals(assetGrouptId.ToLower()));
            if (assetGroupEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<AssetGroupDto>(assetGroupEntity).AssetGroupName;
        }

        public List<AssetGroupDto> GetListAssetGroupsByAssetType(int assetType)
        {
            IQueryable<AssetGroup> query = assetGroupRepository.GetAll().Where(x => !x.IsDelete).Where(x => x.AssetType == assetType);
            IQueryable<AssetGroupDto> assetGroupDtoQuery = query.ProjectTo<AssetGroupDto>(query);
            return assetGroupDtoQuery.ToList();
        }

        public AssetGroupForViewDto GetAssetGroupByAssetID(string assetGrouptId)
        {
            var assetGroupEntity = assetGroupRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.AssetGrouptId.ToLower().Equals(assetGrouptId.ToLower()));
            if (assetGroupEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<AssetGroupForViewDto>(assetGroupEntity);
        }

        #endregion

        #region Private Method

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_AssetGroup_Create)]
        private void Create(AssetGroupInput assetGroupInput)
        {
            var assetGroupEntity = ObjectMapper.Map<AssetGroup>(assetGroupInput);
            SetAuditInsert(assetGroupEntity);
            assetGroupRepository.Insert(assetGroupEntity);
            CurrentUnitOfWork.SaveChanges();
        }

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_AssetGroup_Edit)]
        private void Update(AssetGroupInput assetGroupInput)
        {
            var assetGroupEntity = assetGroupRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == assetGroupInput.Id);
            if (assetGroupEntity == null)
            {
            }
            ObjectMapper.Map(assetGroupInput, assetGroupEntity);
            SetAuditEdit(assetGroupEntity);
            assetGroupRepository.Update(assetGroupEntity);
            CurrentUnitOfWork.SaveChanges();
        }

        #endregion
    }
}