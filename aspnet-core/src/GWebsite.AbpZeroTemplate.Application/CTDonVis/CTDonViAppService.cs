using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using GWebsite.AbpZeroTemplate.Application;
using GWebsite.AbpZeroTemplate.Application.Share.CTDonVis;
using GWebsite.AbpZeroTemplate.Application.Share.CTDonVis.Dto;
using GWebsite.AbpZeroTemplate.Core.Authorization;
using GWebsite.AbpZeroTemplate.Core.Models;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace GWebsite.AbpZeroTemplate.Web.Core.CTDonVis
{
    [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient)]
    public class CTDonViAppService : GWebsiteAppServiceBase, ICTDonViAppService
    {
        private readonly IRepository<CTDonVi> cTDonViRepository;

        public CTDonViAppService(IRepository<CTDonVi> cTDonViRepository)
        {
            this.cTDonViRepository = cTDonViRepository;
        }

        #region Public Method

        public void CreateOrEditCTDonVi(CTDonViInput cTDonViInput)
        {
            if (cTDonViInput.Id == 0)
            {
                Create(cTDonViInput);
            }
            else
            {
                Update(cTDonViInput);
            }
        }

        public void DeleteCTDonVi(int id)
        {
            var cTDonViEntity = cTDonViRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (cTDonViEntity != null)
            {
                cTDonViEntity.IsDelete = true;
                cTDonViRepository.Update(cTDonViEntity);
                CurrentUnitOfWork.SaveChanges();
            }
        }

        public CTDonViInput GetCTDonViForEdit(int id)
        {
            var cTDonViEntity = cTDonViRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (cTDonViEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<CTDonViInput>(cTDonViEntity);
        }

        public CTDonViForViewDto GetCTDonViForView(int id)
        {
            var cTDonViEntity = cTDonViRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (cTDonViEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<CTDonViForViewDto>(cTDonViEntity);
        }

        public PagedResultDto<CTDonViDto> GetCTDonVis(CTDonViFilter input)
        {
            var query = cTDonViRepository.GetAll().Where(x => !x.IsDelete);

            // filter by value
            if (input.TenDonVi!= null)
            {
                query = query.Where(x => x.TenDonVi.ToLower().Equals(input.TenDonVi));
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
            return new PagedResultDto<CTDonViDto>(
                totalCount,
                items.Select(item => ObjectMapper.Map<CTDonViDto>(item)).ToList());
        }

        #endregion

        #region Private Method

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient_Create)]
        private void Create(CTDonViInput cTDonViInput)
        {
            var cTDonViEntity = ObjectMapper.Map<CTDonVi>(cTDonViInput);
            SetAuditInsert(cTDonViEntity);
            cTDonViRepository.Insert(cTDonViEntity);
            CurrentUnitOfWork.SaveChanges();
        }

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient_Edit)]
        private void Update(CTDonViInput cTDonViInput)
        {
            var cTDonViEntity = cTDonViRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == cTDonViInput.Id);
            if (cTDonViEntity == null)
            {
            }
            ObjectMapper.Map(cTDonViInput, cTDonViEntity);
            SetAuditEdit(cTDonViEntity);
            cTDonViRepository.Update(cTDonViEntity);
            CurrentUnitOfWork.SaveChanges();
        }

        #endregion
    }
}
