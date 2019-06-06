using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using AutoMapper.QueryableExtensions;
using GWebsite.AbpZeroTemplate.Application;
using GWebsite.AbpZeroTemplate.Application.Share.TaiSanCoDinhs;
using GWebsite.AbpZeroTemplate.Application.Share.TaiSanCoDinhs.Dto;
using GWebsite.AbpZeroTemplate.Core.Authorization;
using GWebsite.AbpZeroTemplate.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Web.Core.TaiSanCoDinhs
{
    [AbpAuthorize(GWebsitePermissions.Pages_Administration_TaiSanCoDinh)]
    public class TaiSanCoDinhAppService : GWebsiteAppServiceBase, ITaiSanCoDinhAppService
    {
        private readonly IRepository<TaiSanCoDinh> taiSanCoDinhRepository;
        private readonly IRepository<LoaiTaiSan> loaiTaiSanRepository;

        public TaiSanCoDinhAppService(IRepository<TaiSanCoDinh> taiSanCoDinhRepository, IRepository<LoaiTaiSan> loaiTaiSanRepository)
        {
            this.taiSanCoDinhRepository = taiSanCoDinhRepository;
            this.loaiTaiSanRepository = loaiTaiSanRepository;
        }

        #region Public Method

        public void CreateOrEditTaiSanCoDinh(TaiSanCoDinhInput taiSanCoDinhInput)
        {
            if (taiSanCoDinhInput.Id == 0)
            {
                Create(taiSanCoDinhInput);
            }
            else
            {
                Update(taiSanCoDinhInput);
            }
        }

        public void DeleteTaiSanCoDinh(int id)
        {
            var taiSanCoDinhEntity = taiSanCoDinhRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (taiSanCoDinhEntity != null)
            {
                taiSanCoDinhEntity.IsDelete = true;
                taiSanCoDinhRepository.Update(taiSanCoDinhEntity);
                CurrentUnitOfWork.SaveChanges();
            }
        }

        public TaiSanCoDinhInput GetTaiSanCoDinhForEdit(int id)
        {
            var taiSanCoDinhEntity = taiSanCoDinhRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (taiSanCoDinhEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<TaiSanCoDinhInput>(taiSanCoDinhEntity);
        }

        public TaiSanCoDinhDto GetTaiSanCoDinhForView(int id)
        {
            var taiSanCoDinhEntity = taiSanCoDinhRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (taiSanCoDinhEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<TaiSanCoDinhDto>(taiSanCoDinhEntity);
        }

        public PagedResultDto<TaiSanCoDinhForViewDto> GetTaiSanCoDinhs(TaiSanCoDinhFilter input)
        {
            IQueryable<TaiSanCoDinh> query = taiSanCoDinhRepository.GetAll().Where(x => !x.IsDelete);

            // filter by value
            if (input.TenTaiSan != null)
            {
                query = query.Where(x => x.TenTaiSan.ToLower().Contains(input.TenTaiSan.ToLower()));
            }
            // IQueryable
            IQueryable<TaiSanCoDinhDto> taiSanCoDinhQuery = query.ProjectTo<TaiSanCoDinhDto>();

            IQueryable<LoaiTaiSan> loaiTaiSanQuery = loaiTaiSanRepository.GetAll().Where(x => x.IsDelete == false);

            IQueryable<TaiSanCoDinhForViewDto> hoadonNhapOutputQuery =(from tscd in taiSanCoDinhQuery
                                                                        join lts in loaiTaiSanQuery
                                                                        on tscd.LoaiTaiSanId equals lts.Id
                                                                        select new TaiSanCoDinhForViewDto()
                                                                        {
                                                                            TaiSanCoDinh = tscd,
                                                                            TenLoaiTaiSan = lts.TenNhomTaiSan
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
            return new PagedResultDto<TaiSanCoDinhForViewDto>(
                totalCount,
                items);
        }

        #endregion

        #region Private Method

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_TaiSanCoDinh_Create)]
        private void Create(TaiSanCoDinhInput taiSanCoDinhInput)
        {
            //var donViCungCapTaiSanEntity = donViCungCapTaiSanRepository.GetAll().Where(x => !x.IsDelete).FirstOrDefault(x => x.Id == taiSanCoDinhInput.donViCungCapTaiSanId);
            //if (donViCungCapTaiSanEntity == null)
            //	return;

            var taiSanCoDinhEntity = ObjectMapper.Map<TaiSanCoDinh>(taiSanCoDinhInput);

            //taiSanCoDinhEntity.donViCungCapTaiSan = donViCungCapTaiSanEntity;

            SetAuditInsert(taiSanCoDinhEntity);
            taiSanCoDinhRepository.Insert(taiSanCoDinhEntity);
            CurrentUnitOfWork.SaveChanges();
        }

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_TaiSanCoDinh_Edit)]
        private void Update(TaiSanCoDinhInput taiSanCoDinhInput)
        {
            var taiSanCoDinhEntity = taiSanCoDinhRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == taiSanCoDinhInput.Id);
            if (taiSanCoDinhEntity == null)
            {
            }
            ObjectMapper.Map(taiSanCoDinhInput, taiSanCoDinhEntity);
            SetAuditEdit(taiSanCoDinhEntity);
            taiSanCoDinhRepository.Update(taiSanCoDinhEntity);
            CurrentUnitOfWork.SaveChanges();
        }

        #endregion
    }
}
