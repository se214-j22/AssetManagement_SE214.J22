using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using GWebsite.AbpZeroTemplate.Application;
using GWebsite.AbpZeroTemplate.Application.Share.Assets;
using GWebsite.AbpZeroTemplate.Application.Share.Assets.Dto;
using GWebsite.AbpZeroTemplate.Core.Authorization;
using GWebsite.AbpZeroTemplate.Core.Models;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace GWebsite.AbpZeroTemplate.Web.Core.Assets
{
    [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient)]
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
            if (input.Name != null)
            {
                query = query.Where(x => x.Name.ToLower().Equals(input.Name));
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

        #endregion

        #region Private Method

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient_Create)]
        private void Create(AssetInput assetInput)
        {
            var assetEntity = ObjectMapper.Map<Asset>(assetInput);
            SetAuditInsert(assetEntity);
            assetRepository.Insert(assetEntity);
            CurrentUnitOfWork.SaveChanges();
        }

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient_Edit)]
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