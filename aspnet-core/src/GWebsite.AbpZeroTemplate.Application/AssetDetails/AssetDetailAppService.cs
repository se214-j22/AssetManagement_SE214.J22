using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using GWebsite.AbpZeroTemplate.Application;
using GWebsite.AbpZeroTemplate.Application.Share.AssetDetails;
using GWebsite.AbpZeroTemplate.Application.Share.AssetDetails.Dto;
using GWebsite.AbpZeroTemplate.Core.Authorization;
using GWebsite.AbpZeroTemplate.Core.Models;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace GWebsite.AbpZeroTemplate.Web.Core.AssetDetails
{
    [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient)]
    public class AssetDetailAppService : GWebsiteAppServiceBase, IAssetDetailAppService
    {
        private readonly IRepository<AssetDetail> assetDetailRepository;

        public AssetDetailAppService(IRepository<AssetDetail> assetDetailRepository)
        {
            this.assetDetailRepository = assetDetailRepository;
        }

        #region Public Method

        public void CreateOrEditAssetDetail(AssetDetailInput assetDetailInput)
        {
            if (assetDetailInput.Id == 0)
            {
                Create(assetDetailInput);
            }
            else
            {
                Update(assetDetailInput);
            }
        }

        public void DeleteAssetDetail(int id)
        {
            var assetDetailEntity = assetDetailRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (assetDetailEntity != null)
            {
                assetDetailEntity.IsDelete = true;
                assetDetailRepository.Update(assetDetailEntity);
                CurrentUnitOfWork.SaveChanges();
            }
        }

        public AssetDetailInput GetAssetDetailForEdit(int id)
        {
            var assetDetailEntity = assetDetailRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (assetDetailEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<AssetDetailInput>(assetDetailEntity);
        }

        public AssetDetailForViewDto GetAssetDetailForView(int id)
        {
            var assetDetailEntity = assetDetailRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (assetDetailEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<AssetDetailForViewDto>(assetDetailEntity);
        }

        public PagedResultDto<AssetDetailDto> GetAssetDetails(AssetDetailFilter input)
        {
            var query = assetDetailRepository.GetAll().Where(x => !x.IsDelete);

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
            return new PagedResultDto<AssetDetailDto>(
                totalCount,
                items.Select(item => ObjectMapper.Map<AssetDetailDto>(item)).ToList());
        }

        #endregion

        #region Private Method

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient_Create)]
        private void Create(AssetDetailInput assetDetailInput)
        {
            var assetDetailEntity = ObjectMapper.Map<AssetDetail>(assetDetailInput);
            SetAuditInsert(assetDetailEntity);
            assetDetailRepository.Insert(assetDetailEntity);
            CurrentUnitOfWork.SaveChanges();
        }

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient_Edit)]
        private void Update(AssetDetailInput assetDetailInput)
        {
            var assetDetailEntity = assetDetailRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == assetDetailInput.Id);
            if (assetDetailEntity == null)
            {
            }
            ObjectMapper.Map(assetDetailInput, assetDetailEntity);
            SetAuditEdit(assetDetailEntity);
            assetDetailRepository.Update(assetDetailEntity);
            CurrentUnitOfWork.SaveChanges();
        }

        #endregion
    }
}