using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using AutoMapper.QueryableExtensions;
using GWebsite.AbpZeroTemplate.Application;
using GWebsite.AbpZeroTemplate.Application.Share.PhieuBaoDuongs;
using GWebsite.AbpZeroTemplate.Application.Share.PhieuBaoDuongs.Dto;
using GWebsite.AbpZeroTemplate.Core.Authorization;
using GWebsite.AbpZeroTemplate.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Web.Core.PhieuBaoDuongs
{
    [AbpAuthorize(GWebsitePermissions.Pages_Administration_PhieuBaoDuong)]
    public class PhieuBaoDuongAppService : GWebsiteAppServiceBase, IPhieuBaoDuongAppService
    {
        private readonly IRepository<PhieuBaoDuong> phieuBaoDuongRepository;
        private readonly IRepository<TaiSanCoDinh> taiSanCoDinhRepository;

        public PhieuBaoDuongAppService(IRepository<PhieuBaoDuong> phieuBaoDuongRepository, IRepository<TaiSanCoDinh> taiSanCoDinhRepository)
        {
            this.phieuBaoDuongRepository = phieuBaoDuongRepository;
            this.taiSanCoDinhRepository = taiSanCoDinhRepository;
        }

        #region Public Method

        public void CreateOrEditPhieuBaoDuong(PhieuBaoDuongInput phieuBaoDuongInput)
        {
            if (phieuBaoDuongInput.Id == 0)
            {
                Create(phieuBaoDuongInput);
            }
            else
            {
                Update(phieuBaoDuongInput);
            }
        }

        public void DeletePhieuBaoDuong(int id)
        {
            var phieuBaoDuongEntity = phieuBaoDuongRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (phieuBaoDuongEntity != null)
            {
                phieuBaoDuongEntity.IsDelete = true;
                phieuBaoDuongRepository.Update(phieuBaoDuongEntity);
                CurrentUnitOfWork.SaveChanges();
            }
        }

        public PhieuBaoDuongInput GetPhieuBaoDuongForEdit(int id)
        {
            var phieuBaoDuongEntity = phieuBaoDuongRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (phieuBaoDuongEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<PhieuBaoDuongInput>(phieuBaoDuongEntity);
        }

        public PhieuBaoDuongDto GetPhieuBaoDuongForView(int id)
        {
            var phieuBaoDuongEntity = phieuBaoDuongRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (phieuBaoDuongEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<PhieuBaoDuongDto>(phieuBaoDuongEntity);
        }

        public PagedResultDto<PhieuBaoDuongForViewDto> GetPhieuBaoDuongs(PhieuBaoDuongFilter input)
        {
            IQueryable<PhieuBaoDuong> query = phieuBaoDuongRepository.GetAll().Where(x => !x.IsDelete);

            // filter by value
            if (input.SoHoaDon != null)
            {
                query = query.Where(x => x.SoHoaDon.ToLower().Contains(input.SoHoaDon.ToLower()));
            }
            // IQueryable
            IQueryable<PhieuBaoDuongDto> phieuBaoDuongQuery = query.ProjectTo<PhieuBaoDuongDto>();

            IQueryable<TaiSanCoDinh> taiSanCoDinhQuery = taiSanCoDinhRepository.GetAll().Where(x => x.IsDelete == false);

            IQueryable<PhieuBaoDuongForViewDto> phieuBaoDuongOutputQuery =(from pbd in phieuBaoDuongQuery
                                                                        join tscd in taiSanCoDinhQuery
                                                                        on pbd.TaiSanCoDinhId equals tscd.Id
                                                                        select new PhieuBaoDuongForViewDto()
                                                                        {
                                                                            PhieuBaoDuong = pbd,
                                                                            TenTaiSan = tscd.TenTaiSan
                                                                        });


            var totalCount = phieuBaoDuongOutputQuery.Count();

            // sorting
            if (!string.IsNullOrWhiteSpace(input.Sorting))
            {
                phieuBaoDuongOutputQuery = phieuBaoDuongOutputQuery.OrderBy(input.Sorting);
            }

            // paging
            var items = phieuBaoDuongOutputQuery.PageBy(input).ToList();

            // result
            return new PagedResultDto<PhieuBaoDuongForViewDto>(
                totalCount,
                items);
        }

        #endregion

        #region Private Method

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_PhieuBaoDuong_Create)]
        private void Create(PhieuBaoDuongInput phieuBaoDuongInput)
        {
            //var donViCungCapTaiSanEntity = donViCungCapTaiSanRepository.GetAll().Where(x => !x.IsDelete).FirstOrDefault(x => x.Id == phieuBaoDuongInput.donViCungCapTaiSanId);
            //if (donViCungCapTaiSanEntity == null)
            //	return;

            var phieuBaoDuongEntity = ObjectMapper.Map<PhieuBaoDuong>(phieuBaoDuongInput);

            //phieuBaoDuongEntity.donViCungCapTaiSan = donViCungCapTaiSanEntity;

            SetAuditInsert(phieuBaoDuongEntity);
            phieuBaoDuongRepository.Insert(phieuBaoDuongEntity);
            CurrentUnitOfWork.SaveChanges();
        }

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_PhieuBaoDuong_Edit)]
        private void Update(PhieuBaoDuongInput phieuBaoDuongInput)
        {
            var phieuBaoDuongEntity = phieuBaoDuongRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == phieuBaoDuongInput.Id);
            if (phieuBaoDuongEntity == null)
            {
            }
            ObjectMapper.Map(phieuBaoDuongInput, phieuBaoDuongEntity);
            SetAuditEdit(phieuBaoDuongEntity);
            phieuBaoDuongRepository.Update(phieuBaoDuongEntity);
            CurrentUnitOfWork.SaveChanges();
        }

        #endregion
    }
}
