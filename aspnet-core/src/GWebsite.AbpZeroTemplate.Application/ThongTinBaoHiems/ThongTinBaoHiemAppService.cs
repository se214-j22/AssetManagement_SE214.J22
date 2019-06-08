using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using GWebsite.AbpZeroTemplate.Application;
using GWebsite.AbpZeroTemplate.Application.Share.ThongTinBaoHiems;
using GWebsite.AbpZeroTemplate.Application.Share.ThongTinBaoHiems.Dto;
using GWebsite.AbpZeroTemplate.Core.Authorization;
using GWebsite.AbpZeroTemplate.Core.Models;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace GWebsite.AbpZeroTemplate.Web.Core.ThongTinBaoHiems
{
    [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient)]
    public class ThongTinBaoHiemAppService : GWebsiteAppServiceBase,IThongTinBaoHiemAppService
    {
        private readonly IRepository<ThongTinBaoHiem> thongTinBaoHiemRepository;

        public ThongTinBaoHiemAppService(IRepository<ThongTinBaoHiem> thongTinBaoHiemRepository)
        {
            this.thongTinBaoHiemRepository = thongTinBaoHiemRepository;
        }

        #region Public Method

        public void CreateOrEditThongTinBaoHiem(ThongTinBaoHiemInput thongTinBaoHiemInput)
        {
            if (thongTinBaoHiemInput.Id == 0)
            {
                Create(thongTinBaoHiemInput);
            }
            else
            {
                Update(thongTinBaoHiemInput);
            }
        }

        public void DeleteThongTinBaoHiem(int id)
        {
            var thongTinBaoHiemEntity = thongTinBaoHiemRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (thongTinBaoHiemEntity != null)
            {
                thongTinBaoHiemEntity.IsDelete = true;
                thongTinBaoHiemRepository.Update(thongTinBaoHiemEntity);
                CurrentUnitOfWork.SaveChanges();
            }
        }

        public ThongTinBaoHiemInput GetThongTinBaoHiemForEdit(int id)
        {
            var thongTinBaoHiemEntity = thongTinBaoHiemRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (thongTinBaoHiemEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<ThongTinBaoHiemInput>(thongTinBaoHiemEntity);
        }

        public ThongTinBaoHiemForViewDto GetThongTinBaoHiemForView(int id)
        {
            var thongTinBaoHiemEntity = thongTinBaoHiemRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id );
            if (thongTinBaoHiemEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<ThongTinBaoHiemForViewDto>(thongTinBaoHiemEntity);
        }

        public PagedResultDto<ThongTinBaoHiemDto> GetThongTinBaoHiems(ThongTinBaoHiemFilter input)
        {
            var query = thongTinBaoHiemRepository.GetAll().Where(x => !x.IsDelete);

            // filter by value
            if (input.soXe != null)
            {
                query = query.Where(x => x.soXe.ToLower().Equals(input.soXe));
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
            return new PagedResultDto<ThongTinBaoHiemDto>(
                totalCount,
                items.Select(item => ObjectMapper.Map<ThongTinBaoHiemDto>(item)).ToList());
        }

        #endregion

        #region Private Method

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient_Create)]
        private void Create(ThongTinBaoHiemInput thongTinBaoHiemInput)
        {
            var thongTinBaoHiemEntity = ObjectMapper.Map<ThongTinBaoHiem>(thongTinBaoHiemInput);
            SetAuditInsert(thongTinBaoHiemEntity);
            thongTinBaoHiemRepository.Insert(thongTinBaoHiemEntity);
            CurrentUnitOfWork.SaveChanges();
        }

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient_Edit)]
        private void Update(ThongTinBaoHiemInput thongTinBaoHiemInput)
        {
            var thongTinBaoHiemEntity = thongTinBaoHiemRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == thongTinBaoHiemInput.Id);
            if (thongTinBaoHiemEntity == null)
            {
            }
            ObjectMapper.Map(thongTinBaoHiemInput, thongTinBaoHiemEntity);
            SetAuditEdit(thongTinBaoHiemEntity);
            thongTinBaoHiemRepository.Update(thongTinBaoHiemEntity);
            CurrentUnitOfWork.SaveChanges();
        }

        #endregion
    }
}