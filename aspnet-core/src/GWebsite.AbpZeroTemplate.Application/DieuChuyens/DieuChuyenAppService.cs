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
using System.Linq;
using System.Linq.Dynamic.Core;
namespace GWebsite.AbpZeroTemplate.Web.Core.DieuChuyens
{
    [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient)]
    public class DieuChuyenAppService : GWebsiteAppServiceBase, IDieuChuyenAppService
    {
        private readonly IRepository<DieuChuyen> dieuChuyenRepository;
        private readonly IRepository<CTTaiSan> cttsrepository;
        private readonly IRepository<DonVi> donVirepository;
        private readonly IRepository<NhanVien> nhanVienrepository;
        private readonly IRepository<CTDonVi> ctDonVirepository;
        public DieuChuyenAppService(IRepository<DieuChuyen> dieuChuyenRepository, IRepository<CTTaiSan> cttsrepository, IRepository<DonVi> donVirepository, IRepository<NhanVien> nhanVienrepository, IRepository<CTDonVi> ctDonVirepository)
        {
            this.dieuChuyenRepository = dieuChuyenRepository;
            this.cttsrepository = cttsrepository;
            this.donVirepository = donVirepository;
            this.nhanVienrepository = nhanVienrepository;
            this.ctDonVirepository = ctDonVirepository;
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
            if (input.TenDonVi != null)
            {
                query = query.Where(x => x.TenDonVi.ToLower().Equals(input.TenDonVi));
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
            var maDonViDC = donVirepository.GetAll().Where(x => !x.IsDelete).FirstOrDefault(x => x.TenDonVi == dieuChuyenInput.TenNhanVienDC).Id;
            var maDonViNhan = donVirepository.GetAll().Where(x => !x.IsDelete).FirstOrDefault(x => x.TenDonVi == dieuChuyenInput.TenDonVi).Id;
            var maNhanVien = nhanVienrepository.GetAll().Where(x => !x.IsDelete).FirstOrDefault(x => x.MaDV == maDonViNhan && x.TenNhanVien == dieuChuyenInput.TenNhanVienNhan).Id;

            var soluong = ctDonVirepository.GetAll().Where(x => !x.IsDelete).FirstOrDefault(x => x.MaDV == maDonViDC && x.MaTS == dieuChuyenInput.MaTaiSan).SoLuong;

            if (dieuChuyenInput.SoLuong <= soluong && maDonViDC!=maDonViNhan)
            {

                dieuChuyenInput.MaNhanVienNhan = maNhanVien;
                dieuChuyenInput.MaDonVi = maDonViNhan;
                var dieuChuyenEnity = ObjectMapper.Map<DieuChuyen>(dieuChuyenInput);
                SetAuditInsert(dieuChuyenEnity);
                dieuChuyenRepository.Insert(dieuChuyenEnity);
                CurrentUnitOfWork.SaveChanges();

                for (int i = 0; i < dieuChuyenInput.SoLuong; i++)
                {
                    var update = cttsrepository.GetAll().Where(x => !x.IsDelete).FirstOrDefault(x => x.MaTS == dieuChuyenInput.MaTaiSan && x.MaDV == maDonViDC);
                    update.MADC = dieuChuyenEnity.Id;
                    update.MaDV = maDonViNhan;
                    CurrentUnitOfWork.SaveChanges();
                }
                var checkMaTaiSanInCTDonVi = ctDonVirepository.GetAll().Where(x => !x.IsDelete).Where(x => x.MaTS == dieuChuyenInput.MaTaiSan && x.MaDV == maDonViNhan).Count();
                if (checkMaTaiSanInCTDonVi == 0)
                {
                    CTDonViInput cTDonViInput = new CTDonViInput();
                    cTDonViInput.MaDV = maDonViNhan;
                    cTDonViInput.MaTS = dieuChuyenInput.MaTaiSan;
                    cTDonViInput.TenDonVi = dieuChuyenInput.TenDonVi;
                    cTDonViInput.TenTaiSan = dieuChuyenInput.TenTaiSan;
                    cTDonViInput.SoLuong = dieuChuyenInput.SoLuong;
                    var ctDonViEntity = ObjectMapper.Map<CTDonVi>(cTDonViInput);
                    SetAuditInsert(ctDonViEntity);
                    ctDonVirepository.Insert(ctDonViEntity);
                    CurrentUnitOfWork.SaveChanges();

                    var sldonvidc = ctDonVirepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.MaTS == dieuChuyenInput.MaTaiSan && x.MaDV == maDonViDC);
                    sldonvidc.SoLuong -= dieuChuyenInput.SoLuong;
                    CurrentUnitOfWork.SaveChanges();
                }
                else
                {
                    var sldonvinhan = ctDonVirepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.MaTS == dieuChuyenInput.MaTaiSan && x.MaDV == maDonViNhan);
                    sldonvinhan.SoLuong += dieuChuyenInput.SoLuong;
                    CurrentUnitOfWork.SaveChanges();
                    var sldonvidc = ctDonVirepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.MaTS == dieuChuyenInput.MaTaiSan && x.MaDV == maDonViDC);
                    sldonvidc.SoLuong -= dieuChuyenInput.SoLuong;
                    CurrentUnitOfWork.SaveChanges();
                }

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
