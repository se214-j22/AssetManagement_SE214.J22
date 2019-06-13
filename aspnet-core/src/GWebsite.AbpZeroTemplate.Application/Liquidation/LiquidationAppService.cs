using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using AutoMapper.QueryableExtensions;
using GWebsite.AbpZeroTemplate.Application;
using GWebsite.AbpZeroTemplate.Application.Share.Liquidations;
using GWebsite.AbpZeroTemplate.Application.Share.Liquidations.Dto;
using GWebsite.AbpZeroTemplate.Core.Authorization;
using GWebsite.AbpZeroTemplate.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace GWebsite.AbpZeroTemplate.Web.Core.Liquidations
{
    [AbpAuthorize(GWebsitePermissions.Pages_Administration_Liquidation)]
    public class LiquidationAppService : GWebsiteAppServiceBase, ILiquidationAppService
    {
        private readonly IRepository<Liquidation> liquidationRepository;

        public LiquidationAppService(IRepository<Liquidation> liquidationRepository)
        {
            this.liquidationRepository = liquidationRepository;
        }

        #region Public Method

        public void CreateOrEditLiquidation(LiquidationInput liquidationInput)
        {
            if (liquidationInput.Id == 0)
            {
                Create(liquidationInput);
            }
            else
            {
                Update(liquidationInput);
            }
        }

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_Liquidation_Delete)]
        public void DeleteLiquidation(int id)
        {
            var liquidationEntity = liquidationRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (liquidationEntity != null)
            {
                liquidationEntity.IsDelete = true;
                liquidationRepository.Update(liquidationEntity);
                CurrentUnitOfWork.SaveChanges();
            }
        }

        public LiquidationInput GetLiquidationForEdit(int id)
        {
            var liquidationEntity = liquidationRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (liquidationEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<LiquidationInput>(liquidationEntity);
        }

        public LiquidationForViewDto GetLiquidationForView(int id)
        {
            var liquidationEntity = liquidationRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (liquidationEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<LiquidationForViewDto>(liquidationEntity);
        }

        public PagedResultDto<LiquidationDto> GetLiquidations(LiquidationFilter input)
        {
            var query = liquidationRepository.GetAll().Where(x => !x.IsDelete);

            // filter by value
            if (input.AssetID != null)
            {
                query = query.Where(x => x.AssetID.ToLower().Equals(input.AssetID));
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
            return new PagedResultDto<LiquidationDto>(
                totalCount,
                items.Select(item => ObjectMapper.Map<LiquidationDto>(item)).ToList());
        }

        public LiquidationForViewDto GetLiquidationByAssetID(string assetId)
        {
            var liquidationEntity = liquidationRepository.GetAll().Where(x => !x.IsDelete).Where(x => x.StatusApproved == true)
                .SingleOrDefault(x => x.AssetID.ToLower().Equals(assetId.ToLower()));
            if (liquidationEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<LiquidationForViewDto>(liquidationEntity);
        }

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_Liquidation_Approve)]
        public void ApproveLiquidation(int id)
        {
            var liquidationEntity = liquidationRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (liquidationEntity != null)
            {
                liquidationEntity.StatusApproved = true;
                liquidationRepository.Update(liquidationEntity);
                CurrentUnitOfWork.SaveChanges();
            }
        }

        public List<LiquidationDto> GetListLiquidation()
        {
            IQueryable<Liquidation> query = liquidationRepository.GetAll().Where(x => !x.IsDelete).Where(x => x.StatusApproved == false);
            IQueryable<LiquidationDto> assetGroupDtoQuery = query.ProjectTo<LiquidationDto>(query);
            return assetGroupDtoQuery.ToList();
        }

        #endregion

        #region Private Method

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_Liquidation_Create)]
        private void Create(LiquidationInput liquidationInput)
        {
            liquidationInput.StatusApproved = false;
            var liquidationEntity = ObjectMapper.Map<Liquidation>(liquidationInput);
            SetAuditInsert(liquidationEntity);
            liquidationRepository.Insert(liquidationEntity);
            CurrentUnitOfWork.SaveChanges();
        }

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_Liquidation_Edit)]
        private void Update(LiquidationInput liquidationInput)
        {
            var liquidationEntity = liquidationRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == liquidationInput.Id);
            if (liquidationEntity == null)
            {
            }
            ObjectMapper.Map(liquidationInput, liquidationEntity);
            SetAuditEdit(liquidationEntity);
            liquidationRepository.Update(liquidationEntity);
            CurrentUnitOfWork.SaveChanges();
        }

        #endregion
    }
}