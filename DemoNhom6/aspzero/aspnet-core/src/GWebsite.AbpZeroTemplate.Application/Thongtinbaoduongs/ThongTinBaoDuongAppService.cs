using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using GWebsite.AbpZeroTemplate.Application;
using GWebsite.AbpZeroTemplate.Application.Share.ThongTinBaoDuongs;
using GWebsite.AbpZeroTemplate.Application.Share.ThongTinBaoDuongs.Dto;
using GWebsite.AbpZeroTemplate.Core.Authorization;
using GWebsite.AbpZeroTemplate.Core.Models;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace GWebsite.AbpZeroTemplate.Web.Core.ThongTinBaoDuongs
{
    [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient)]
    public class ThongTinBaoDuongAppService : GWebsiteAppServiceBase, IThongTinBaoDuongAppService
    {
        private readonly IRepository<ThongTinBaoDuong> thongtinbaoduongRepository;

        public ThongTinBaoDuongAppService(IRepository<ThongTinBaoDuong> thongtinbaoduongRepository)
        {
            this.thongtinbaoduongRepository = thongtinbaoduongRepository;
        }

        #region Public Method

        public void CreateOrEditThongTinBaoDuong(ThongTinBaoDuongInput thongtinbaoduongInput)
        {
            if (thongtinbaoduongInput.Id == 0)
            {
                Create(thongtinbaoduongInput);
            }
            else
            {
                Update(thongtinbaoduongInput);
            }
        }

        public void DeleteThongTinBaoDuong(int id)
        {
            var thongtinbaoduongEntity = thongtinbaoduongRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (thongtinbaoduongEntity != null)
            {
                thongtinbaoduongEntity.IsDelete = true;
                thongtinbaoduongRepository.Update(thongtinbaoduongEntity);
                CurrentUnitOfWork.SaveChanges();
            }
        }

        public ThongTinBaoDuongInput GetThongTinBaoDuongForEdit(int id)
        {
            var thongtinbaoduongEntity = thongtinbaoduongRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (thongtinbaoduongEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<ThongTinBaoDuongInput>(thongtinbaoduongEntity);
        }

        public ThongTinBaoDuongForViewDto GetThongTinBaoDuongForView(int id)
        {
            var thongtinbaoduongEntity = thongtinbaoduongRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (thongtinbaoduongEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<ThongTinBaoDuongForViewDto>(thongtinbaoduongEntity);
        }

        public PagedResultDto<ThongTinBaoDuongDto> GetThongTinBaoDuongs(ThongTinBaoDuongFilter input)
        {
            var query = thongtinbaoduongRepository.GetAll().Where(x => !x.IsDelete);

            // filter by value
            if (input.SoXe != null)
            {
                query = query.Where(x => x.SoXe.ToLower().Equals(input.SoXe));
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
            return new PagedResultDto<ThongTinBaoDuongDto>(
                totalCount,
                items.Select(item => ObjectMapper.Map<ThongTinBaoDuongDto>(item)).ToList());
        }

        #endregion

        #region Private Method

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient_Create)]
        private void Create(ThongTinBaoDuongInput thongtinbaoduongInput)
        {
            var thongtinbaoduongEntity = ObjectMapper.Map<ThongTinBaoDuong>(thongtinbaoduongInput);
            SetAuditInsert(thongtinbaoduongEntity);
            thongtinbaoduongRepository.Insert(thongtinbaoduongEntity);
            CurrentUnitOfWork.SaveChanges();
        }

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient_Edit)]
        private void Update(ThongTinBaoDuongInput thongtinbaoduongInput)
        {
            var thongtinbaoduongEntity = thongtinbaoduongRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == thongtinbaoduongInput.Id);
            if (thongtinbaoduongEntity == null)
            {
            }
            ObjectMapper.Map(thongtinbaoduongInput, thongtinbaoduongEntity);
            SetAuditEdit(thongtinbaoduongEntity);
            thongtinbaoduongRepository.Update(thongtinbaoduongEntity);
            CurrentUnitOfWork.SaveChanges();
        }

        #endregion
    }
}
