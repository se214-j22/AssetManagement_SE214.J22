using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using AutoMapper.QueryableExtensions;
using GWebsite.AbpZeroTemplate.Application;
using GWebsite.AbpZeroTemplate.Application.Share.Assets;
using GWebsite.AbpZeroTemplate.Application.Share.Assets.Dto;
using GWebsite.AbpZeroTemplate.Core.Authorization;
using GWebsite.AbpZeroTemplate.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace GWebsite.AbpZeroTemplate.Web.Core.Assets
{
    [AbpAuthorize(GWebsitePermissions.Pages_Administration_Asset)]
    public class AssetAppService : GWebsiteAppServiceBase, IAssetAppService
    {
        private readonly IRepository<Asset> assetRepository;

        public AssetAppService(IRepository<Asset> assetRepository)
        {
            this.assetRepository = assetRepository;
        }

        #region Public Method

        public void CreateOrEditAsset(AssetInput assetInput)
        {
            if (assetInput.Id == 0)
            {
                Create(assetInput);
            }
            else
            {
                Update(assetInput);
            }
        }

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_Asset_Delete)]
        public void DeleteAsset(int id)
        {
            var assetEntity = assetRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (assetEntity != null)
            {
                assetEntity.IsDelete = true;
                assetRepository.Update(assetEntity);
                CurrentUnitOfWork.SaveChanges();
            }
        }

        public AssetInput GetAssetForEdit(int id)
        {
            var assetEntity = assetRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (assetEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<AssetInput>(assetEntity);
        }

        public AssetForViewDto GetAssetForView(int id)
        {
            var assetEntity = assetRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (assetEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<AssetForViewDto>(assetEntity);
        }

        public PagedResultDto<AssetDto> GetAssets(AssetFilter input)
        {
            var query = assetRepository.GetAll().Where(x => !x.IsDelete);

            // filter by value
            if (input.AssetName != null)
            {
                query = query.Where(x => x.AssetName.ToLower().Contains(input.AssetName.ToLower()));
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
            return new PagedResultDto<AssetDto>(
                totalCount,
                items.Select(item => ObjectMapper.Map<AssetDto>(item)).ToList());
        }

        public string GetAssetNameByAssetID(string assetId)
        {
            var assetEntity = assetRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.AssetId.ToLower().Equals(assetId.ToLower()));
            if (assetEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<AssetDto>(assetEntity).AssetName;
        }

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_Asset_Approve)]
        public void ApproveAsset(int id)
        {
            var assetEntity = assetRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (assetEntity != null)
            {
                assetEntity.StatusApproved = true;
                assetRepository.Update(assetEntity);
                CurrentUnitOfWork.SaveChanges();
            }
        }

        public AssetForViewDto GetAssetByAssetID(string assetId)
        {
            var assetEntity = assetRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.AssetId.ToLower().Equals(assetId.ToLower()));
            if (assetEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<AssetForViewDto>(assetEntity);
        }

        public int GetTotalAsset()
        {
            var query = assetRepository.GetAll();
            if (query == null)
            {
                return 0;
            }
            return query.ToList().Count();
        }

        public List<AssetForViewDto> GetListAssetsInStock()
        {
            IQueryable<Asset> query = assetRepository.GetAll().Where(x => !x.IsDelete)
                .Where(x => x.Status == (int)Const.Const.AssetStatus.IN_STOCK).Where(x => x.StatusApproved == true);
            IQueryable<AssetForViewDto> assetDtoQuery = query.ProjectTo<AssetForViewDto>(query);
            return assetDtoQuery.ToList();
        }

        public List<AssetForViewDto> GetListAssetsInUse()
        {
            IQueryable<Asset> query = assetRepository.GetAll().Where(x => !x.IsDelete)
                .Where(x => x.Status == (int)Const.Const.AssetStatus.USING).Where(x => x.StatusApproved == true);
            IQueryable<AssetForViewDto> assetDtoQuery = query.ProjectTo<AssetForViewDto>(query);
            return assetDtoQuery.ToList();
        }

        public void updateAssetStatusInStock(string assetID)
        {
            var assetEntity = assetRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.AssetId.ToLower().Equals(assetID.ToLower()));
            if (assetEntity != null)
            {
                assetEntity.Status = (int)Const.Const.AssetStatus.IN_STOCK;
                assetRepository.Update(assetEntity);
                CurrentUnitOfWork.SaveChanges();
            }
        }

        public void updateAssetStatusUsing(string assetID)
        {
            var assetEntity = assetRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.AssetId.ToLower().Equals(assetID.ToLower()));
            if (assetEntity != null)
            {
                assetEntity.Status = (int)Const.Const.AssetStatus.USING;
                assetRepository.Update(assetEntity);
                CurrentUnitOfWork.SaveChanges();
            }
        }

        public void updateAssetStatusReparing(string assetID)
        {
            var assetEntity = assetRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.AssetId.ToLower().Equals(assetID.ToLower()));
            if (assetEntity != null)
            {
                assetEntity.Status = (int)Const.Const.AssetStatus.REPARING;
                assetRepository.Update(assetEntity);
                CurrentUnitOfWork.SaveChanges();
            }
        }

        public void updateAssetStatusLiquidated(string assetID)
        {
            var assetEntity = assetRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.AssetId.ToLower().Equals(assetID.ToLower()));
            if (assetEntity != null)
            {
                assetEntity.Status = (int)Const.Const.AssetStatus.LIQUIDATED;
                assetRepository.Update(assetEntity);
                CurrentUnitOfWork.SaveChanges();
            }
        }

        #endregion

        #region Private Method

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_Asset_Create)]
        private void Create(AssetInput assetInput)
        {
            assetInput.StatusApproved = false;
            assetInput.Status = (int)Const.Const.AssetStatus.IN_STOCK;
            var assetEntity = ObjectMapper.Map<Asset>(assetInput);
            SetAuditInsert(assetEntity);
            assetRepository.Insert(assetEntity);
            CurrentUnitOfWork.SaveChanges();
        }

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_Asset_Edit)]
        private void Update(AssetInput assetInput)
        {
            var assetEntity = assetRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == assetInput.Id);
            if (assetEntity == null)
            {
            }
            ObjectMapper.Map(assetInput, assetEntity);
            SetAuditEdit(assetEntity);
            assetRepository.Update(assetEntity);
            CurrentUnitOfWork.SaveChanges();
        }

        #endregion
    }
}