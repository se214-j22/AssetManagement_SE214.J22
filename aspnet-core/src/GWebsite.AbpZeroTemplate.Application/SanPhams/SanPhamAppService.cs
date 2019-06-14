using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using GWebsite.AbpZeroTemplate.Application;
using GWebsite.AbpZeroTemplate.Application.Share.SanPhams;
using GWebsite.AbpZeroTemplate.Application.Share.SanPhams.Dto;
using GWebsite.AbpZeroTemplate.Core.Authorization;
using GWebsite.AbpZeroTemplate.Core.Models;
using System.Linq.Dynamic.Core;
using System.Linq;
using Abp.Linq.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace GWebsite.AbpZeroTemplate.Web.Core.SanPhams
{
    [AbpAuthorize(GWebsitePermissions.Pages_Administration_SanPham)]
    public class SanPhamAppService : GWebsiteAppServiceBase, ISanPhamAppService
    {
        private readonly IRepository<SanPham> sanPhamRepository;

        public SanPhamAppService(IRepository<SanPham> sanPhamRepository)
        {
            this.sanPhamRepository = sanPhamRepository;
        }

        #region Public Method
        public void CreateOrEditSanPham(SanPhamInput sanPhamInput)
        {
            if (sanPhamInput.Id == 0)
            {
                Create(sanPhamInput);
            }
            else
            {
                Update(sanPhamInput);
            }
        }

        public void DeleteSanPham(int id)
        {
            var sanPhamEntity = sanPhamRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (sanPhamEntity != null)
            {
                sanPhamEntity.IsDelete = true;
                sanPhamRepository.Update(sanPhamEntity);
                CurrentUnitOfWork.SaveChanges();
            }
        }

        public IEnumerable<SanPhamReports> GetAllSanPhams()
        {
            var listSanPham= sanPhamRepository.GetAll().Where(x => !x.IsDelete).ToList();

            List<SanPhamReports> list = new List<SanPhamReports>();
            if (listSanPham != null)
            {
                foreach(SanPham s in listSanPham){
                    SanPhamReports sanPham = new SanPhamReports();

                    sanPham.MaSP = s.MaSP;
                    sanPham.TenSP = s.TenSP;
                    sanPham.NgayCapNhat = s.NgayCapNhat.ToString("dd/MM/yyyy");
                    sanPham.NgayTao = s.NgayTao.ToString("dd/MM/yyyy");
                    sanPham.TrangThai = s.TrangThai;

                    list.Add(sanPham);
               }
            }
            if (list == null)
                return null;
            return list;
        }

        public SanPhamInput GetSanPhamForEdit(int id)
        {
            var sanPhamEntity = sanPhamRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (sanPhamEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<SanPhamInput>(sanPhamEntity);
        }

        public SanPhamInput GetSanPhamForEditMaSP(string maSP)
        {
            var sanPhamEntity = sanPhamRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => ("https://"+x.MaSP+".com") == maSP);
            if (sanPhamEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<SanPhamInput>(sanPhamEntity);
        }

        public SanPhamForViewDto GetSanPhamForView(int id)
        {
            var sanPhamEntity = sanPhamRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (sanPhamEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<SanPhamForViewDto>(sanPhamEntity);
        }

        public PagedResultDto<SanPhamDto> GetSanPhams(SanPhamFilter input)
        {
            var query = sanPhamRepository.GetAll().Where(x => !x.IsDelete);

            // filter by value
            if (input.MaSP != null)
            {
                query = query.Where(x => x.MaSP.ToLower().Contains(input.MaSP));
            }

            if (input.TenSP != null)
            {
                query = query.Where(x => x.TenSP.ToLower().Contains(input.TenSP));
            }

            if (input.NgayTao != null)
            {
                query = query.Where(x => (x.NgayTao.Year == input.NgayTao.Value.Year
                                && x.NgayTao.Month == input.NgayTao.Value.Month
                               && x.NgayTao.Day == input.NgayTao.Value.Day));
               // query = query.Where(x => x.NgayTao == input.NgayTao);
                //query = query.Where(x => DateTime.Compare(x.NgayTao, input.NgayTao.Value) == 0);
               // query = query.Where(x=> (Convert.ToDateTime(x.NgayTao.Date)== Convert.ToDateTime(input.NgayTao.Value.Date)));
            }

            if (input.NgayCapNhat != null)
            {
                query = query.Where(x => !(x.NgayCapNhat.Year == input.NgayCapNhat.Value.Year
                                 && x.NgayCapNhat.Month == input.NgayCapNhat.Value.Month
                                && x.NgayCapNhat.Day == input.NgayCapNhat.Value.Day));
            }

            if (input.TrangThai != null)
            {
                query = query.Where(x => x.TrangThai == input.TrangThai);
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
            return new PagedResultDto<SanPhamDto>(
                totalCount,
                items.Select(item => ObjectMapper.Map<SanPhamDto>(item)).ToList());
        }

        #endregion

        #region Private Method

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_SanPham_Create)]
        private void Create(SanPhamInput sanPhamInput)
        {
            var sanPhamEntity = ObjectMapper.Map<SanPham>(sanPhamInput);
            SetAuditInsert(sanPhamEntity);
            sanPhamRepository.Insert(sanPhamEntity);
            CurrentUnitOfWork.SaveChanges();
        }


        [AbpAuthorize(GWebsitePermissions.Pages_Administration_SanPham_Edit)]
        private void Update(SanPhamInput sanPhamInput)
        {
            var sanPhamEntity = sanPhamRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == sanPhamInput.Id);
            if (sanPhamEntity == null)
            {
            }
            ObjectMapper.Map(sanPhamInput, sanPhamEntity);
            SetAuditEdit(sanPhamEntity);
            sanPhamRepository.Update(sanPhamEntity);
            CurrentUnitOfWork.SaveChanges();
        }

        #endregion
    }
}
