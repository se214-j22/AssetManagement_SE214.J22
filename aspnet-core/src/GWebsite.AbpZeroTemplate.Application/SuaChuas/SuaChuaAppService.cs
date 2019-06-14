using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using GWebsite.AbpZeroTemplate.Application;
using GWebsite.AbpZeroTemplate.Application.Share.SuaChuas;
using GWebsite.AbpZeroTemplate.Application.Share.SuaChuas.Dto;
using GWebsite.AbpZeroTemplate.Core.Authorization;
using GWebsite.AbpZeroTemplate.Core.Models;
using System.Linq;
using System;
using System.Linq.Dynamic.Core;
namespace GWebsite.AbpZeroTemplate.Web.Core.SuaChuas
{
    [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient)]
    public class SuaChuaAppService : GWebsiteAppServiceBase, ISuaChuaAppService
    {
        private readonly IRepository<SuaChua> suaChuaRepository;
        private readonly IRepository<DonVi> donVirepository;
        private readonly IRepository<NhanVien> nhanVienrepository;
        private readonly IRepository<ThongTinTaiSan> tttsrepository;
        public SuaChuaAppService(IRepository<SuaChua> suaChuaRepository, IRepository<ThongTinTaiSan> tttsrepository,
            IRepository<DonVi> donVirepository, IRepository<NhanVien> nhanVienrepository)
        {
            this.suaChuaRepository = suaChuaRepository;
            this.donVirepository = donVirepository;
            this.nhanVienrepository = nhanVienrepository;
            this.tttsrepository = tttsrepository;
        }
        #region public method
        public void CreateOrEditSuaChua(SuaChuaInput suaChuaInput)
        {
            if (suaChuaInput.Id == 0)
            {
                Create(suaChuaInput);
            }
            else
            {
                Update(suaChuaInput);
            }
        }

        public void DeleteSuaChua(int id)
        {
            var suaChuaEnity = suaChuaRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (suaChuaEnity != null)
            {
                suaChuaEnity.IsDelete = true;
                suaChuaRepository.Update(suaChuaEnity);
                CurrentUnitOfWork.SaveChanges();
            }
        }

        public SuaChuaInput GetSuaChuaForEdit(int id)
        {
            var suaChuaEnity = suaChuaRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (suaChuaEnity == null)
            {
                return null;
            }
            return ObjectMapper.Map<SuaChuaInput>(suaChuaEnity);
        }

        public SuaChuaForViewDto GetSuaChuaForView(int id)
        {
            var suaChuaEnity = suaChuaRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (suaChuaEnity == null)
            {
                return null;
            }
            return ObjectMapper.Map<SuaChuaForViewDto>(suaChuaEnity);
        }

        public PagedResultDto<SuaChuaDto> GetSuaChuas(SuaChuaFilter input)
        {
            var query = suaChuaRepository.GetAll().Where(x => !x.IsDelete);

            // filter by value
            if (input.TenNhanVienPT != null)
            {
                query = query.Where(x => x.TenNhanVienPT.ToLower().Equals(input.TenNhanVienPT));
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
            return new PagedResultDto<SuaChuaDto>(
                totalCount,
                items.Select(item => ObjectMapper.Map<SuaChuaDto>(item)).ToList());
        }

        public string[] GetArrTenDVSC()
        {
            var query = donVirepository.GetAll().Where(x => !x.IsDelete).Select(x => x.TenDonVi).ToArray();
            string[] str = query.Select(x => x.ToString()).ToArray();
            return str;
        }

        public string[] GetArrTenNVPT(string tenDV)
        {
            int maDV = donVirepository.GetAll().Where(x => !x.IsDelete).FirstOrDefault(x => x.TenDonVi == tenDV).Id;

            var query = nhanVienrepository.GetAll().Where(x => !x.IsDelete).Where(x => x.MaDV == maDV).Select(x => x.TenNhanVien).ToArray();
            string[] str = query.Select(x => x.ToString()).ToArray();
            return str;
        }

        public string[] GetArrTenNVDX()
        {            
            var query = nhanVienrepository.GetAll().Where(x => !x.IsDelete).Select(x => x.TenNhanVien).ToArray();
            string[] str = query.Select(x => x.ToString()).ToArray();
            return str;
        }

        public string GetTenDVDX(string tenNV)
        {
            string tenDVDX = nhanVienrepository.GetAll().Where(x => !x.IsDelete).FirstOrDefault(x => x.TenNhanVien == tenNV).TenDV;

            return tenDVDX;
        }
        #endregion
        #region Private Method

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient_Create)]
        private void Create(SuaChuaInput suaChuaInput)
        {
            suaChuaInput.NgayXuat = DateTime.Now;
            var maDVDX = donVirepository.GetAll().Where(x => !x.IsDelete).FirstOrDefault(x => x.TenDonVi == suaChuaInput.TenDVDeXuat).Id;
            var maDVSC = donVirepository.GetAll().Where(x => !x.IsDelete).FirstOrDefault(x => x.TenDonVi == suaChuaInput.TenDVSuaChua).Id;
            var maNVDX = nhanVienrepository.GetAll().Where(x => !x.IsDelete).FirstOrDefault(x=>x.TenNhanVien==suaChuaInput.TenNhanVienDX && x.MaDV==maDVDX).Id;
            var maNVPT = nhanVienrepository.GetAll().Where(x => !x.IsDelete).FirstOrDefault(x => x.TenNhanVien == suaChuaInput.TenNhanVienPT && x.MaDV == maDVSC).Id;

            suaChuaInput.MaDVDeXuat = maDVDX;
            suaChuaInput.MaDVSuaChua = maDVSC;
            suaChuaInput.MaNhanVienDX = maNVDX;
            suaChuaInput.MaNhanVienPT = maNVPT;
            var suaChuaEnity = ObjectMapper.Map<SuaChua>(suaChuaInput);
            SetAuditInsert(suaChuaEnity);
            suaChuaRepository.Insert(suaChuaEnity);
            CurrentUnitOfWork.SaveChanges();

            var updateTs = tttsrepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.MaTS == suaChuaInput.MaTS);
            updateTs.TinhTrang = "Sửa Chữa";
            updateTs.MaDV = suaChuaEnity.MaDVSuaChua;
            updateTs.TenDV = suaChuaEnity.TenDVSuaChua;
            CurrentUnitOfWork.SaveChanges();
        }

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient_Edit)]
        private void Update(SuaChuaInput suaChuaInput)
        {
            var suaChuaEnity = suaChuaRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == suaChuaInput.Id);
            if (suaChuaEnity == null)
            {
            }
            ObjectMapper.Map(suaChuaInput, suaChuaEnity);
            SetAuditEdit(suaChuaEnity);
            suaChuaRepository.Update(suaChuaEnity);
            CurrentUnitOfWork.SaveChanges();
        }

        #endregion
    }
}
