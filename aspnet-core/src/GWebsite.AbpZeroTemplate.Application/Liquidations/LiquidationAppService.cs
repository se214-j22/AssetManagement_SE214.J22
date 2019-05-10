using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using GWebsite.AbpZeroTemplate.Application;
using GWebsite.AbpZeroTemplate.Application.Share.Liquidations;
using GWebsite.AbpZeroTemplate.Application.Share.Liquidations.Dto;
using GWebsite.AbpZeroTemplate.Core.Authorization;
using GWebsite.AbpZeroTemplate.Core.Models;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace GWebsite.AbpZeroTemplate.Web.Core.Liquidations
{
    [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient)]
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
            if (input.LiquidatorName != null)
            {
                query = query.Where(x => x.LiquidatorName.ToLower().Equals(input.LiquidatorName));
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

        #endregion

        #region Private Method

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient_Create)]
        private void Create(LiquidationInput liquidationInput)
        {
            var liquidationEntity = ObjectMapper.Map<Liquidation>(liquidationInput);
            SetAuditInsert(liquidationEntity);
            liquidationRepository.Insert(liquidationEntity);
            CurrentUnitOfWork.SaveChanges();
        }

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient_Edit)]
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