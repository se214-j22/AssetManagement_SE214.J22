using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using GWebsite.AbpZeroTemplate.Application;
using GWebsite.AbpZeroTemplate.Application.Share.LoTaiSans;
using GWebsite.AbpZeroTemplate.Application.Share.LoTaiSans.Dto;
using GWebsite.AbpZeroTemplate.Core.Authorization;
using GWebsite.AbpZeroTemplate.Core.Models;
using System.Linq;
using System.Linq.Dynamic.Core;
namespace GWebsite.AbpZeroTemplate.Web.Core.LoTaiSans
{
    [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient)]
    public class LoTaiSanAppService : GWebsiteAppServiceBase, ILoTaiSanAppService
    {
        private readonly IRepository<LoTaiSan> loTaiSanRepository;
        public LoTaiSanAppService(IRepository<LoTaiSan> loTaiSanRepository)
        {
            this.loTaiSanRepository = loTaiSanRepository;
        }
        public void CreateOrEditLoTaiSan(LoTaiSanInput loTaiSanInput)
        {
            if (loTaiSanInput.Id == 0)
            {
                Create(loTaiSanInput);
            }
            else
            {
                Update(loTaiSanInput);
            }
        }

        public void DeleteLoTaiSan(int id)
        {
            var loTaiSanEnity = loTaiSanRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (loTaiSanEnity != null)
            {
                loTaiSanEnity.IsDelete = true;
                loTaiSanRepository.Update(loTaiSanEnity);
                CurrentUnitOfWork.SaveChanges();
            }
        }

        public LoTaiSanInput GetLoTaiSanForEdit(int id)
        {
            var loTaiSanEnity = loTaiSanRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (loTaiSanEnity == null)
            {
                return null;
            }
            return ObjectMapper.Map<LoTaiSanInput>(loTaiSanEnity);
        }

        public LoTaiSanForViewDto GetLoTaiSanForView(int id)
        {
            var loTaiSanEnity = loTaiSanRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (loTaiSanEnity == null)
            {
                return null;
            }
            return ObjectMapper.Map<LoTaiSanForViewDto>(loTaiSanEnity);
        }

        public PagedResultDto<LoTaiSanDto> GetLoTaiSans(LoTaiSanFilter input)
        {
            var query = loTaiSanRepository.GetAll().Where(x => !x.IsDelete);

            // filter by value
            query = query.Where(x => x.SoLuong.Equals(input.SoLuong));

            var totalCount = query.Count();

            // sorting
            if (!string.IsNullOrWhiteSpace(input.Sorting))
            {
                query = query.OrderBy(input.Sorting);
            }

            // paging
            var items = query.PageBy(input).ToList();

            // result
            return new PagedResultDto<LoTaiSanDto>(
                totalCount,
                items.Select(item => ObjectMapper.Map<LoTaiSanDto>(item)).ToList());
        }
        #region Private Method

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient_Create)]
        private void Create(LoTaiSanInput loTaiSanInput)
        {
            var loTaiSanEnity = ObjectMapper.Map<LoTaiSan>(loTaiSanInput);
            SetAuditInsert(loTaiSanEnity);
            loTaiSanRepository.Insert(loTaiSanEnity);
            CurrentUnitOfWork.SaveChanges();
        }

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient_Edit)]
        private void Update(LoTaiSanInput loTaiSanInput)
        {
            var loTaiSanEnity = loTaiSanRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == loTaiSanInput.Id);
            if (loTaiSanEnity == null)
            {
            }
            ObjectMapper.Map(loTaiSanInput, loTaiSanEnity);
            SetAuditEdit(loTaiSanEnity);
            loTaiSanRepository.Update(loTaiSanEnity);
            CurrentUnitOfWork.SaveChanges();
        }

        #endregion
    }
}
