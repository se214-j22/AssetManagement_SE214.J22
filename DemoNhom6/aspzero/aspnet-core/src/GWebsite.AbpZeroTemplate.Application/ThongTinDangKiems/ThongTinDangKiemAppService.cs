using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using GWebsite.AbpZeroTemplate.Application;
using GWebsite.AbpZeroTemplate.Application.Share.ThongTinDangKiems;
using GWebsite.AbpZeroTemplate.Application.Share.ThongTinDangKiems.Dto;
using GWebsite.AbpZeroTemplate.Core.Authorization;
using GWebsite.AbpZeroTemplate.Core.Models;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace GWebsite.AbpZeroTemplate.Web.Core.ThongTinDangKiems
{
    [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient)]
    public class ThongTinDangKiemAppService : GWebsiteAppServiceBase, IThongTinDangKiemAppService
    {
        private readonly IRepository<ThongTinDangKiem> thongTinDangKiemRepository;

        public ThongTinDangKiemAppService(IRepository<ThongTinDangKiem> thongTinDangKiemRepository)
        {
            this.thongTinDangKiemRepository = thongTinDangKiemRepository;
        }

        #region Public Method

        public void CreateOrEditThongTinDangKiem(ThongTinDangKiemInput thongTinDangKiemInput)
        {
            if (thongTinDangKiemInput.Id == 0)
            {
                Create(thongTinDangKiemInput);
            }
            else
            {
                Update(thongTinDangKiemInput);
            }
        }

        public void DeleteThongTinDangKiem(int soXe)
        {
            var thongTinDangKiemEntity = thongTinDangKiemRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == soXe);
            if (thongTinDangKiemEntity != null)
            {
                thongTinDangKiemEntity.IsDelete = true;
                thongTinDangKiemRepository.Update(thongTinDangKiemEntity);
                CurrentUnitOfWork.SaveChanges();
            }
        }

        public ThongTinDangKiemInput GetThongTinDangKiemForEdit(int soXe)
        {
            var thongTinDangKiemEntity = thongTinDangKiemRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == soXe);
            if (thongTinDangKiemEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<ThongTinDangKiemInput>(thongTinDangKiemEntity);
        }

        public ThongTinDangKiemForViewDto GetThongTinDangKiemForView(int soXe)
        {
            var thongTinDangKiemEntity = thongTinDangKiemRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == soXe);
            if (thongTinDangKiemEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<ThongTinDangKiemForViewDto>(thongTinDangKiemEntity);
        }

        public PagedResultDto<ThongTinDangKiemDto> GetThongTinDangKiems(ThongTinDangKiemFilter input)
        {
            var query = thongTinDangKiemRepository.GetAll().Where(x => !x.IsDelete);

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
            return new PagedResultDto<ThongTinDangKiemDto>(
                totalCount,
                items.Select(item => ObjectMapper.Map<ThongTinDangKiemDto>(item)).ToList());
        }

        #endregion

        #region Private Method

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient_Create)]
        private void Create(ThongTinDangKiemInput thongTinDangKiemInput)
        {
            var thongTinDangKiemEntity = ObjectMapper.Map<ThongTinDangKiem>(thongTinDangKiemInput);
            SetAuditInsert(thongTinDangKiemEntity);
            thongTinDangKiemRepository.Insert(thongTinDangKiemEntity);
            CurrentUnitOfWork.SaveChanges();
        }

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient_Edit)]
        private void Update(ThongTinDangKiemInput thongTinDangKiemInput)
        {
            var thongTinDangKiemEntity = thongTinDangKiemRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == thongTinDangKiemInput.Id);
            if (thongTinDangKiemEntity == null)
            {
            }
            ObjectMapper.Map(thongTinDangKiemInput, thongTinDangKiemEntity);
            SetAuditEdit(thongTinDangKiemEntity);
            thongTinDangKiemRepository.Update(thongTinDangKiemEntity);
            CurrentUnitOfWork.SaveChanges();
        }

        #endregion
    }
}