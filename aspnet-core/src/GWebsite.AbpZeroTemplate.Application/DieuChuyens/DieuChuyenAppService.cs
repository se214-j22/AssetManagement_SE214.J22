using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using GWebsite.AbpZeroTemplate.Application;
using GWebsite.AbpZeroTemplate.Application.Share.CTDonVis.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.DieuChuyens;
using GWebsite.AbpZeroTemplate.Application.Share.DieuChuyens.Dto;
using GWebsite.AbpZeroTemplate.Core.Authorization;
using GWebsite.AbpZeroTemplate.Core.Models;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
namespace GWebsite.AbpZeroTemplate.Web.Core.DieuChuyens
{
    [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient)]
    public class DieuChuyenAppService : GWebsiteAppServiceBase, IDieuChuyenAppService
    {
        private readonly IRepository<DieuChuyen> dieuChuyenRepository;
        private readonly IRepository<DonVi> donVirepository;
        private readonly IRepository<NhanVien> nhanVienrepository;
        private readonly IRepository<ThongTinTaiSan> tttsrepository;
      
        public DieuChuyenAppService(IRepository<DieuChuyen> dieuChuyenRepository, IRepository<DonVi> donVirepository,
            IRepository<NhanVien> nhanVienrepository,
            IRepository<ThongTinTaiSan> tttsrepository)
        {
            this.dieuChuyenRepository = dieuChuyenRepository;
            this.donVirepository = donVirepository;
            this.nhanVienrepository = nhanVienrepository;
            this.tttsrepository = tttsrepository;
         
        }
        public void CreateOrEditDieuChuyen(DieuChuyenInput dieuChuyenInput)
        {
            if (dieuChuyenInput.Id == 0)
            {
                Create(dieuChuyenInput);
            }
            else
            {
                Update(dieuChuyenInput);
            }
        }

        public void DeleteDieuChuyen(int id)
        {
            var dieuChuyenEnity = dieuChuyenRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (dieuChuyenEnity != null)
            {
                dieuChuyenEnity.IsDelete = true;
                dieuChuyenRepository.Update(dieuChuyenEnity);
                CurrentUnitOfWork.SaveChanges();
            }
        }

        public DieuChuyenInput GetDieuChuyenForEdit(int id)
        {
            var dieuChuyenEnity = dieuChuyenRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (dieuChuyenEnity == null)
            {
                return null;
            }
            return ObjectMapper.Map<DieuChuyenInput>(dieuChuyenEnity);
        }

        public DieuChuyenForViewDto GetDieuChuyenForView(int id)
        {
            var dieuChuyenEnity = dieuChuyenRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (dieuChuyenEnity == null)
            {
                return null;
            }
            return ObjectMapper.Map<DieuChuyenForViewDto>(dieuChuyenEnity);
        }

        public PagedResultDto<DieuChuyenDto> GetDieuChuyens(DieuChuyenFilter input)
        {
            var query = dieuChuyenRepository.GetAll().Where(x => !x.IsDelete);

            // filter by value
            if (input.TenDonViNhan != null)
            {
                query = query.Where(x => x.TenDonViNhan.ToLower().Contains(input.TenDonViNhan));
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
            return new PagedResultDto<DieuChuyenDto>(
                totalCount,
                items.Select(item => ObjectMapper.Map<DieuChuyenDto>(item)).ToList());
        }

        public string[] GetArrTenDV()
        {
            var query = donVirepository.GetAll().Where(x => !x.IsDelete).Select(x => x.TenDonVi).ToArray();
            string[] str = query.Select(x => x.ToString()).ToArray();
            return str;
        }

        public string[] GetArrTenNVNhan(string tenDV)
        {
            int maDV = donVirepository.GetAll().Where(x => !x.IsDelete).FirstOrDefault(x => x.TenDonVi == tenDV).Id;

            var query = nhanVienrepository.GetAll().Where(x => !x.IsDelete).Where(x => x.MaDV == maDV).Select(x => x.TenNhanVien).ToArray();
            string[] str = query.Select(x => x.ToString()).ToArray();
            return str;
        }

        #region Private Method

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient_Create)]
        private void Create(DieuChuyenInput dieuChuyenInput)
        {
            var maDonViDC = donVirepository.GetAll().Where(x => !x.IsDelete).FirstOrDefault(x => x.TenDonVi == dieuChuyenInput.TenDonViDC).Id;
            var maDonViNhan = donVirepository.GetAll().Where(x => !x.IsDelete).FirstOrDefault(x => x.TenDonVi == dieuChuyenInput.TenDonViNhan).Id;
            var maNhanVien = nhanVienrepository.GetAll().Where(x => !x.IsDelete).FirstOrDefault(x => x.MaDV == maDonViNhan && x.TenNhanVien == dieuChuyenInput.TenNhanVienNhan).Id;

            if (maDonViDC!=maDonViNhan)
            {
                dieuChuyenInput.NgayDieuChuyen = DateTime.Now;
                dieuChuyenInput.MaNhanVienNhan = maNhanVien;
                dieuChuyenInput.MaDVNhan = maNhanVien;
                dieuChuyenInput.MaDVDC = maDonViDC;
                dieuChuyenInput.TenDonViDC = dieuChuyenInput.TenDonViDC;
                var dieuChuyenEnity = ObjectMapper.Map<DieuChuyen>(dieuChuyenInput);
                SetAuditInsert(dieuChuyenEnity);
                dieuChuyenRepository.Insert(dieuChuyenEnity);
                CurrentUnitOfWork.SaveChanges();

                var update = tttsrepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.MaTS == dieuChuyenInput.MaTaiSan);
                update.MaDV = dieuChuyenEnity.MaDVNhan;
                update.TenDV = dieuChuyenEnity.TenDonViNhan;
                CurrentUnitOfWork.SaveChanges();
            }
        
        }

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient_Edit)]
        private void Update(DieuChuyenInput dieuChuyenInput)
        {
            var dieuChuyenEnity = dieuChuyenRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == dieuChuyenInput.Id);
            if (dieuChuyenEnity == null)
            {
            }
            ObjectMapper.Map(dieuChuyenInput, dieuChuyenEnity);
            SetAuditEdit(dieuChuyenEnity);
            dieuChuyenRepository.Update(dieuChuyenEnity);
            CurrentUnitOfWork.SaveChanges();
           
        }

        #endregion
    }
}
