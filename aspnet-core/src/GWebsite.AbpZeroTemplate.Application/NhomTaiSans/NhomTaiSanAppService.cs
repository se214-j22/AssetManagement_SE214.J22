using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using GWebsite.AbpZeroTemplate.Application;
using GWebsite.AbpZeroTemplate.Application.Share.NhomTaiSans;
using GWebsite.AbpZeroTemplate.Application.Share.NhomTaiSans.Dto;
using GWebsite.AbpZeroTemplate.Core.Authorization;
using GWebsite.AbpZeroTemplate.Core.Models;
using System.Linq;
using System.Linq.Dynamic.Core;
namespace GWebsite.AbpZeroTemplate.Web.Core.NhomTaiSans
{
    [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient)]
    public class NhomTaiSanAppService : GWebsiteAppServiceBase, INhomTaiSanAppService
    {
        private readonly IRepository<NhomTaiSan> nhomTaiSanRepository;
        public NhomTaiSanAppService(IRepository<NhomTaiSan> nhomTaiSanRepository)
        {
            this.nhomTaiSanRepository = nhomTaiSanRepository;
        }
        public void CreateOrEditNhomTaiSan(NhomTaiSanInput nhomTaiSanInput)
        {
            if (nhomTaiSanInput.Id == 0)
            {
                Create(nhomTaiSanInput);
            }
            else
            {
                Update(nhomTaiSanInput);
            }
        }

        public void DeleteNhomTaiSan(int id)
        {
            var nhomTaiSanEnity = nhomTaiSanRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (nhomTaiSanEnity != null)
            {
                nhomTaiSanEnity.IsDelete = true;
                nhomTaiSanRepository.Update(nhomTaiSanEnity);
                CurrentUnitOfWork.SaveChanges();
            }
        }

        public NhomTaiSanInput GetNhomTaiSanForEdit(int id)
        {
            var nhomTaiSanEnity = nhomTaiSanRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (nhomTaiSanEnity == null)
            {
                return null;
            }
            return ObjectMapper.Map<NhomTaiSanInput>(nhomTaiSanEnity);
        }

        public NhomTaiSanForViewDto GetNhomTaiSanForView(int id)
        {
            var nhomTaiSanEnity = nhomTaiSanRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (nhomTaiSanEnity == null)
            {
                return null;
            }
            return ObjectMapper.Map<NhomTaiSanForViewDto>(nhomTaiSanEnity);
        }

        public PagedResultDto<NhomTaiSanDto> GetNhomTaiSans(NhomTaiSanFilter input)
        {
            var query = nhomTaiSanRepository.GetAll().Where(x => !x.IsDelete);

            // filter by value
            if (input.tenNhomTaiSan != null)
            {
                query = query.Where(x => x.tenNhomTaiSan.ToLower().Contains(input.tenNhomTaiSan));
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
            return new PagedResultDto<NhomTaiSanDto>(
                totalCount,
                items.Select(item => ObjectMapper.Map<NhomTaiSanDto>(item)).ToList());
        }
        #region Private Method

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient_Create)]
        private void Create(NhomTaiSanInput nhomTaiSanInput)
        {
            var nhomTaiSanEnity = ObjectMapper.Map<NhomTaiSan>(nhomTaiSanInput);
            SetAuditInsert(nhomTaiSanEnity);
            nhomTaiSanRepository.Insert(nhomTaiSanEnity);
            CurrentUnitOfWork.SaveChanges();
        }

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient_Edit)]
        private void Update(NhomTaiSanInput nhomTaiSanInput)
        {
            var nhomTaiSanEnity = nhomTaiSanRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == nhomTaiSanInput.Id);
            if (nhomTaiSanEnity == null)
            {
            }
            ObjectMapper.Map(nhomTaiSanInput, nhomTaiSanEnity);
            SetAuditEdit(nhomTaiSanEnity);
            nhomTaiSanRepository.Update(nhomTaiSanEnity);
            CurrentUnitOfWork.SaveChanges();
        }

        #endregion
    }
}
