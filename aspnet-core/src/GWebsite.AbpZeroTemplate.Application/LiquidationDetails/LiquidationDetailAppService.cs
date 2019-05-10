using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using GWebsite.AbpZeroTemplate.Application;
using GWebsite.AbpZeroTemplate.Application.Share.LiquidationDetails;
using GWebsite.AbpZeroTemplate.Application.Share.LiquidationDetails.Dto;
using GWebsite.AbpZeroTemplate.Core.Authorization;
using GWebsite.AbpZeroTemplate.Core.Models;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace GWebsite.AbpZeroTemplate.Web.Core.LiquidationDetails
{
    [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient)]
    public class LiquidationDetailAppService : GWebsiteAppServiceBase, ILiquidationDetailAppService
    {
        private readonly IRepository<LiquidationDetail> liquidationDetailRepository;

        public LiquidationDetailAppService(IRepository<LiquidationDetail> liquidationDetailRepository)
        {
            this.liquidationDetailRepository = liquidationDetailRepository;
        }

        #region Public Method

        public void CreateOrEditLiquidationDetail(LiquidationDetailInput liquidationDetailInput)
        {
            if (liquidationDetailInput.Id == 0)
            {
                Create(liquidationDetailInput);
            }
            else
            {
                Update(liquidationDetailInput);
            }
        }

        public void DeleteLiquidationDetail(int id)
        {
            var liquidationDetailEntity = liquidationDetailRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (liquidationDetailEntity != null)
            {
                liquidationDetailEntity.IsDelete = true;
                liquidationDetailRepository.Update(liquidationDetailEntity);
                CurrentUnitOfWork.SaveChanges();
            }
        }

        public LiquidationDetailInput GetLiquidationDetailForEdit(int id)
        {
            var liquidationDetailEntity = liquidationDetailRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (liquidationDetailEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<LiquidationDetailInput>(liquidationDetailEntity);
        }

        public LiquidationDetailForViewDto GetLiquidationDetailForView(int id)
        {
            var liquidationDetailEntity = liquidationDetailRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (liquidationDetailEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<LiquidationDetailForViewDto>(liquidationDetailEntity);
        }

        public PagedResultDto<LiquidationDetailDto> GetLiquidationDetails(LiquidationDetailFilter input)
        {
            var query = liquidationDetailRepository.GetAll().Where(x => !x.IsDelete);

            // filter by value
            if (input.LiquidationID != null)
            {
                query = query.Where(x => x.LiquidationID.ToLower().Equals(input.LiquidationID));
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
            return new PagedResultDto<LiquidationDetailDto>(
                totalCount,
                items.Select(item => ObjectMapper.Map<LiquidationDetailDto>(item)).ToList());
        }

        #endregion

        #region Private Method

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient_Create)]
        private void Create(LiquidationDetailInput liquidationDetailInput)
        {
            var liquidationDetailEntity = ObjectMapper.Map<LiquidationDetail>(liquidationDetailInput);
            SetAuditInsert(liquidationDetailEntity);
            liquidationDetailRepository.Insert(liquidationDetailEntity);
            CurrentUnitOfWork.SaveChanges();
        }

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient_Edit)]
        private void Update(LiquidationDetailInput liquidationDetailInput)
        {
            var liquidationDetailEntity = liquidationDetailRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == liquidationDetailInput.Id);
            if (liquidationDetailEntity == null)
            {
            }
            ObjectMapper.Map(liquidationDetailInput, liquidationDetailEntity);
            SetAuditEdit(liquidationDetailEntity);
            liquidationDetailRepository.Update(liquidationDetailEntity);
            CurrentUnitOfWork.SaveChanges();
        }

        #endregion
    }
}