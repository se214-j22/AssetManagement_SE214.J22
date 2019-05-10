using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using GWebsite.AbpZeroTemplate.Application;
using GWebsite.AbpZeroTemplate.Application.Share.TaiSans;
using GWebsite.AbpZeroTemplate.Application.Share.TaiSans.Dto;
using GWebsite.AbpZeroTemplate.Core.Authorization;
using GWebsite.AbpZeroTemplate.Core.Models;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace GWebsite.AbpZeroTemplate.Web.Core.TaiSans
{
    [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient)]
    public class TaiSanAppService : GWebsiteAppServiceBase, ITaiSanAppService
    {
        private readonly IRepository<TaiSan> taisanrepository;

        public TaiSanAppService(IRepository<TaiSan> taisanrepository)
        {
            this.taisanrepository = taisanrepository;
        }

        #region public method

        public void CreateOrEditTaiSan(TaiSanInput taiSanInput)
        {
            if (taiSanInput.Id == 0)
            {
                Create(taiSanInput);
            }
            else
            {
                Update(taiSanInput);
            }
        }

        public void DeleteTaiSan(int id)
        {
            var taisanEntity = taisanrepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (taisanEntity != null)
            {
                taisanEntity.IsDelete = true;
                taisanrepository.Update(taisanEntity);
                CurrentUnitOfWork.SaveChanges();
            }
        }

        public TaiSanInput GetTaiSanForEdit(int id)
        {
            var taisanEntity = taisanrepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (taisanEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<TaiSanInput>(taisanEntity);
        }

        public TaiSanForViewDto GetTaiSanForView(int id)
        {
            var taisanEntity = taisanrepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (taisanEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<TaiSanForViewDto>(taisanEntity);
        }

        public PagedResultDto<TaiSanDto> GetTaiSans(TaiSanFilter input)
        {
            var query = taisanrepository.GetAll().Where(x => !x.IsDelete);

            // filter by value
            if (input.TenTaiSan != null)
            {
                query = query.Where(x => x.TenTaiSan.ToLower().Equals(input.TenTaiSan));
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
            return new PagedResultDto<TaiSanDto>(
                totalCount,
                items.Select(item => ObjectMapper.Map<TaiSanDto>(item)).ToList());
        }

        #endregion

        #region Private Method

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient_Create)]
        private void Create(TaiSanInput taiSanInput)
        {
            var taisanEnity = ObjectMapper.Map<TaiSan>(taiSanInput);
            SetAuditInsert(taisanEnity);
            taisanrepository.Insert(taisanEnity);
            CurrentUnitOfWork.SaveChanges();
        }

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient_Edit)]
        private void Update(TaiSanInput taiSanInput)
        {
            var taisanEnity = taisanrepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == taiSanInput.Id);
            if (taisanEnity == null)
            {
            }
            ObjectMapper.Map(taiSanInput, taisanEnity);
            SetAuditEdit(taisanEnity);
            taisanrepository.Update(taisanEnity);
            CurrentUnitOfWork.SaveChanges();
        }

        #endregion
    }
}
