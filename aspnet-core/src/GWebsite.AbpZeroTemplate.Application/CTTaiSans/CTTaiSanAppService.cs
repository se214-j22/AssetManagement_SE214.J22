using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using GWebsite.AbpZeroTemplate.Application;
using GWebsite.AbpZeroTemplate.Application.Share.CTTaiSans;
using GWebsite.AbpZeroTemplate.Application.Share.CTTaiSans.Dto;
using GWebsite.AbpZeroTemplate.Core.Authorization;
using GWebsite.AbpZeroTemplate.Core.Models;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace GWebsite.AbpZeroTemplate.Web.Core.CTTaiSans
{
    [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient)]
    public class CTTaiSanAppService:GWebsiteAppServiceBase,ICTTaiSanAppService
    {

        private readonly IRepository<CTTaiSan> cttsrepository;
        public CTTaiSanAppService(IRepository<CTTaiSan> cttsrepository)
        {
            this.cttsrepository = cttsrepository;
        }
        public void CreateOrEditCTTaiSan(CTTaiSanInput cTTaiSanInput)
        {
            if (cTTaiSanInput.Id == 0)
            {
                Create(cTTaiSanInput);
            }
            else
            {
                Update(cTTaiSanInput);
            }
        }

        public void DeleteCTTaiSan(int id)
        {
            var taisanEntity = cttsrepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (taisanEntity != null)
            {
                taisanEntity.IsDelete = true;
                cttsrepository.Update(taisanEntity);
                CurrentUnitOfWork.SaveChanges();
            }
        }

        public CTTaiSanInput GetCTTaiSanForEdit(int id)
        {
            var taisanEntity = cttsrepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (taisanEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<CTTaiSanInput>(taisanEntity);
        }

        public CTTaiSanForViewDto GetCTTaiSanForView(int id)
        {
            var taisanEntity = cttsrepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (taisanEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<CTTaiSanForViewDto>(taisanEntity);
        }

        public PagedResultDto<CTTaiSanDto> GetCTTaiSans(CTTaiSanFilter input)
        {
            var query = cttsrepository.GetAll().Where(x => !x.IsDelete);

            // filter by value
            if (input.Soseri != null)
            {
                query = query.Where(x => x.SoSeri.ToLower().Equals(input.Soseri));
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
            return new PagedResultDto<CTTaiSanDto>(
                totalCount,
                items.Select(item => ObjectMapper.Map<CTTaiSanDto>(item)).ToList());
        }
        #region Private Method

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient_Create)]
        private void Create(CTTaiSanInput cTTaiSanInput)
        {
            var taisanEnity = ObjectMapper.Map<CTTaiSan>(cTTaiSanInput);
            SetAuditInsert(taisanEnity);
            cttsrepository.Insert(taisanEnity);
            CurrentUnitOfWork.SaveChanges();
        }

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient_Edit)]
        private void Update(CTTaiSanInput cTTaiSanInput)
        {
            var taisanEnity = cttsrepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == cTTaiSanInput.Id);
            if (taisanEnity == null)
            {
            }
            ObjectMapper.Map(cTTaiSanInput, taisanEnity);
            SetAuditEdit(taisanEnity);
            cttsrepository.Update(taisanEnity);
            CurrentUnitOfWork.SaveChanges();
        }

        #endregion
    }
}
