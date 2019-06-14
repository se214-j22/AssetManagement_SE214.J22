using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using AutoMapper.QueryableExtensions;
using GWebsite.AbpZeroTemplate.Application;
using GWebsite.AbpZeroTemplate.Application.Share.UseAssets;
using GWebsite.AbpZeroTemplate.Application.Share.UseAssets.Dto;
using GWebsite.AbpZeroTemplate.Core.Authorization;
using GWebsite.AbpZeroTemplate.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace GWebsite.AbpZeroTemplate.Web.Core.UseAssets
{
    [AbpAuthorize(GWebsitePermissions.Pages_Administration_UseAsset)]
    public class UseAssetAppService : GWebsiteAppServiceBase, IUseAssetAppService
    {
        private readonly IRepository<UseAsset> useAssetRepository;

        public UseAssetAppService(IRepository<UseAsset> useAssetRepository)
        {
            this.useAssetRepository = useAssetRepository;
        }

        #region Public Method

        public void CreateOrEditUseAsset(UseAssetInput useAssetInput)
        {
            if (useAssetInput.Id == 0)
            {
                Create(useAssetInput);
            }
            else
            {
                Update(useAssetInput);
            }
        }

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_UseAsset_Delete)]
        public void DeleteUseAsset(int id)
        {
            var useAssetEntity = useAssetRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (useAssetEntity != null)
            {
                useAssetEntity.IsDelete = true;
                useAssetRepository.Update(useAssetEntity);
                CurrentUnitOfWork.SaveChanges();
            }
        }

        public UseAssetInput GetUseAssetForEdit(int id)
        {
            var useAssetEntity = useAssetRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (useAssetEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<UseAssetInput>(useAssetEntity);
        }

        public UseAssetForViewDto GetUseAssetForView(int id)
        {
            var useAssetEntity = useAssetRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (useAssetEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<UseAssetForViewDto>(useAssetEntity);
        }

        public PagedResultDto<UseAssetDto> GetUseAssets(UseAssetFilter input)
        {
            var query = useAssetRepository.GetAll().Where(x => !x.IsDelete);

            // filter by value
            if (input.AssetId != null)
            {
                query = query.Where(x => x.AssetId.ToLower().Equals(input.AssetId));
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
            return new PagedResultDto<UseAssetDto>(
                totalCount,
                items.Select(item => ObjectMapper.Map<UseAssetDto>(item)).ToList());
        }

        public UseAssetDto GetUseAssetByAssetID(string id)
        {
            var useAssetEntity = useAssetRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.AssetId == id);
            if (useAssetEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<UseAssetDto>(useAssetEntity);
        }

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_UseAsset_Approve)]
        public void ApproveUseAsset(int id)
        {
            var useAssetEntity = useAssetRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (useAssetEntity != null)
            {
                useAssetEntity.StatusApproved = true;
                useAssetRepository.Update(useAssetEntity);
                CurrentUnitOfWork.SaveChanges();
            }
        }

        public List<UseAssetDto> GetListUsseAssetNoteApproved()
        {
            IQueryable<UseAsset> query = useAssetRepository.GetAll().Where(x => !x.IsDelete).Where(x => x.StatusApproved == false);
            IQueryable<UseAssetDto> assetGroupDtoQuery = query.ProjectTo<UseAssetDto>(query);
            return assetGroupDtoQuery.ToList();
        }

        #endregion

        #region Private Method

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_UseAsset_Create)]
        private void Create(UseAssetInput useAssetInput)
        {
            useAssetInput.StatusApproved = false;
            var useAssetEntity = ObjectMapper.Map<UseAsset>(useAssetInput);
            SetAuditInsert(useAssetEntity);
            useAssetRepository.Insert(useAssetEntity);
            CurrentUnitOfWork.SaveChanges();
        }

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_UseAsset_Edit)]
        private void Update(UseAssetInput useAssetInput)
        {
            var useAssetEntity = useAssetRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == useAssetInput.Id);
            if (useAssetEntity == null)
            {
            }
            ObjectMapper.Map(useAssetInput, useAssetEntity);
            SetAuditEdit(useAssetEntity);
            useAssetRepository.Update(useAssetEntity);
            CurrentUnitOfWork.SaveChanges();
        }

        #endregion
    }
}