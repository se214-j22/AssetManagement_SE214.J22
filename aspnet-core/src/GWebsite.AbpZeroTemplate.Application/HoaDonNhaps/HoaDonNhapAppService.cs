using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using AutoMapper.QueryableExtensions;
using GWebsite.AbpZeroTemplate.Application;
using GWebsite.AbpZeroTemplate.Application.Share.HoaDonNhaps;
using GWebsite.AbpZeroTemplate.Application.Share.HoaDonNhaps.Dto;
using GWebsite.AbpZeroTemplate.Core.Authorization;
using GWebsite.AbpZeroTemplate.Core.Models;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace GWebsite.AbpZeroTemplate.Web.Core.HoaDonNhaps
{
    [AbpAuthorize(GWebsitePermissions.Pages_Administration_HoaDonNhap)]
    public class HoaDonNhapAppService : GWebsiteAppServiceBase, IHoaDonNhapAppService
    {
        private readonly IRepository<HoaDonNhap> hoaDonNhapRepository;
		private readonly IRepository<DonViCungCapTaiSan> donViCungCapTaiSanRepository;

		public HoaDonNhapAppService(IRepository<HoaDonNhap> hoaDonNhapRepository, IRepository<DonViCungCapTaiSan> donViCungCapTaiSanRepository)
        {
            this.hoaDonNhapRepository = hoaDonNhapRepository;
			this.donViCungCapTaiSanRepository = donViCungCapTaiSanRepository;
		}

        #region Public Method

        public void CreateOrEditHoaDonNhap(HoaDonNhapInput hoaDonNhapInput)
        {
            if (hoaDonNhapInput.Id == 0)
            {
                Create(hoaDonNhapInput);
            }
            else
            {
                Update(hoaDonNhapInput);
            }
        }

        public void DeleteHoaDonNhap(int id)
        {
            var hoaDonNhapEntity = hoaDonNhapRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (hoaDonNhapEntity != null)
            {
                hoaDonNhapEntity.IsDelete = true;
                hoaDonNhapRepository.Update(hoaDonNhapEntity);
                CurrentUnitOfWork.SaveChanges();
            }
        }

        public HoaDonNhapInput GetHoaDonNhapForEdit(int id)
        {
            var hoaDonNhapEntity = hoaDonNhapRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (hoaDonNhapEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<HoaDonNhapInput>(hoaDonNhapEntity);
        }

        public HoaDonNhapForViewDto GetHoaDonNhapForView(int id)
        {
            var hoaDonNhapEntity = hoaDonNhapRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (hoaDonNhapEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<HoaDonNhapForViewDto>(hoaDonNhapEntity);
        }

        public PagedResultDto<HoaDonNhapOutput> GetHoaDonNhaps(HoaDonNhapFilter input)
        {
			IQueryable<HoaDonNhap> query = hoaDonNhapRepository.GetAll().Where(x => !x.IsDelete);

            // filter by value
            if (input.donViCungCapTaiSanId != null)
            {
                query = query.Where(x => x.donViCungCapTaiSan.Id == input.donViCungCapTaiSanId);
            }
            if (input.SoHoaDon != null)
            {
                query = query.Where(x => x.SoHoaDon.ToLower().Equals(input.SoHoaDon));
            }
			// IQueryable
			IQueryable<HoaDonNhapDto> hoaDonNhapQuery = query.ProjectTo<HoaDonNhapDto>();

			IQueryable<DonViCungCapTaiSan> donViCungCapQuery = donViCungCapTaiSanRepository.GetAll().Where(x => x.IsDelete == false);

			IQueryable<HoaDonNhapOutput> hoadonNhapOutputQuery = (from hdn in hoaDonNhapQuery
																  join dvcc in donViCungCapQuery
																  on hdn.donViCungCapTaiSanId equals dvcc.Id
																  select new HoaDonNhapOutput()
																  {
																	  HoaDonNhap = hdn,
																	  TenDonViCungCap = dvcc.TenDonViCungCap
																  });


			var totalCount = hoadonNhapOutputQuery.Count();

            // sorting
            if (!string.IsNullOrWhiteSpace(input.Sorting))
            {
				hoadonNhapOutputQuery = hoadonNhapOutputQuery.OrderBy(input.Sorting);
            }

            // paging
            var items = hoadonNhapOutputQuery.PageBy(input).ToList();

            // result
            return new PagedResultDto<HoaDonNhapOutput>(
                totalCount,
                items);
        }

        #endregion

        #region Private Method

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_HoaDonNhap_Create)]
        private void Create(HoaDonNhapInput hoaDonNhapInput)
        {
			//var donViCungCapTaiSanEntity = donViCungCapTaiSanRepository.GetAll().Where(x => !x.IsDelete).FirstOrDefault(x => x.Id == hoaDonNhapInput.donViCungCapTaiSanId);
			//if (donViCungCapTaiSanEntity == null)
			//	return;

			var hoaDonNhapEntity = ObjectMapper.Map<HoaDonNhap>(hoaDonNhapInput);

			//hoaDonNhapEntity.donViCungCapTaiSan = donViCungCapTaiSanEntity;

			SetAuditInsert(hoaDonNhapEntity);
            hoaDonNhapRepository.Insert(hoaDonNhapEntity);
            CurrentUnitOfWork.SaveChanges();
        }

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_HoaDonNhap_Edit)]
        private void Update(HoaDonNhapInput hoaDonNhapInput)
        {
            var hoaDonNhapEntity = hoaDonNhapRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == hoaDonNhapInput.Id);
            if (hoaDonNhapEntity == null)
            {
            }
            ObjectMapper.Map(hoaDonNhapInput, hoaDonNhapEntity);
            SetAuditEdit(hoaDonNhapEntity);
            hoaDonNhapRepository.Update(hoaDonNhapEntity);
            CurrentUnitOfWork.SaveChanges();
        }

        #endregion
    }
}