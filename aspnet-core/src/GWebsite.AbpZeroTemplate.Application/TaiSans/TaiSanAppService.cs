using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using GWebsite.AbpZeroTemplate.Application;
using GWebsite.AbpZeroTemplate.Application.Share.TaiSans;
using GWebsite.AbpZeroTemplate.Application.Share.TaiSans.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.LoTaiSans.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.CTTaiSans.Dto;
using GWebsite.AbpZeroTemplate.Core.Authorization;
using GWebsite.AbpZeroTemplate.Core.Models;
using System.Linq;
using System.Linq.Dynamic.Core;
using System;

namespace GWebsite.AbpZeroTemplate.Web.Core.TaiSans
{
    [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient)]
    public class TaiSanAppService : GWebsiteAppServiceBase, ITaiSanAppService
    {
        private readonly IRepository<ThongTinTaiSan> taisanrepository;
        private readonly IRepository<LoTaiSan> lotaisanrepository;
        private readonly IRepository<CTTaiSan> cttaisanrepository;
        private readonly IRepository<NhomTaiSan> nhomTaiSanrepository;
        public TaiSanAppService(IRepository<ThongTinTaiSan> taisanrepository, IRepository<LoTaiSan> lotaisanrepository, 
            IRepository<CTTaiSan> cttaisanrepository
            , IRepository<NhomTaiSan> nhomTaiSanrepository)
        {
            this.taisanrepository = taisanrepository;
            this.lotaisanrepository = lotaisanrepository;
            this.cttaisanrepository = cttaisanrepository;
            this.nhomTaiSanrepository = nhomTaiSanrepository;
        }
        public void CreateOrEditTaiSan(TaiSanInput taiSanInput)
        {
            if (taiSanInput.Id == 0)
            {
                Create(taiSanInput);
            }
            else
            {
                Update(taiSanInput);
            }
        }

        public void DeleteTaiSan(int id)
        {
            var taisanEntity = taisanrepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (taisanEntity != null)
            {
                taisanEntity.IsDelete = true;
                taisanrepository.Update(taisanEntity);
                CurrentUnitOfWork.SaveChanges();
            }
        }

        public TaiSanInput GetTaiSanForEdit(int id)
        {
            var taisanEntity = taisanrepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (taisanEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<TaiSanInput>(taisanEntity);
        }

        public TaiSanForViewDto GetTaiSanForView(int id)
        {
            var taisanEntity = taisanrepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (taisanEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<TaiSanForViewDto>(taisanEntity);
        }

        public PagedResultDto<TaiSanDto> GetTaiSans(TaiSanFilter input)
        {
            var query = taisanrepository.GetAll().Where(x => !x.IsDelete);

            // filter by value
            if (input.TenTs != null)
            {
                query = query.Where(x => x.TenTs.ToLower().Equals(input.TenTs));
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
            return new PagedResultDto<TaiSanDto>(
                totalCount,
                items.Select(item => ObjectMapper.Map<TaiSanDto>(item)).ToList());
        }

        public string[] GetArrTenNhomTaiSan()
        {
            var query = nhomTaiSanrepository.GetAll().Where(x => !x.IsDelete).Select(x => x.tenNhomTaiSan).ToArray();
            string[] str = query.Select(x => x.ToString()).ToArray();
            return str;
        }
        #region Private Method

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient_Create)]
        private void Create(TaiSanInput taiSanInput)
        {
            LoTaiSanInput loTaiSanInput = new LoTaiSanInput();
            loTaiSanInput.SoLuong = taiSanInput.SoLuong;
            loTaiSanInput.NgayNhap = DateTime.Now.Date;
            loTaiSanInput.TongGiaTri = taiSanInput.NguyenGia * taiSanInput.SoLuong;

            var lotaisanEntity = ObjectMapper.Map<LoTaiSan>(loTaiSanInput);
            SetAuditInsert(lotaisanEntity);
            lotaisanrepository.Insert(lotaisanEntity);
            CurrentUnitOfWork.SaveChanges();

            var manhomtaisan = nhomTaiSanrepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.tenNhomTaiSan == taiSanInput.TenNhomTS).Id;
            taiSanInput.NgayNhap =DateTime.Now.Date;
            taiSanInput.MaLo = lotaisanEntity.Id;
            taiSanInput.MaNhomTS = manhomtaisan;
            var taisanEnity = ObjectMapper.Map<ThongTinTaiSan>(taiSanInput);
            SetAuditInsert(taisanEnity);
            taisanrepository.Insert(taisanEnity);
            CurrentUnitOfWork.SaveChanges();

            string[] arrseri = taiSanInput.DSSoseri.Split(',');

            for (int i = 0; i < arrseri.Count(); i++)
            {
                CTTaiSanInput cTTaiSanInput = new CTTaiSanInput();
                cTTaiSanInput.MaLo = lotaisanEntity.Id;
                cTTaiSanInput.MaTS = taisanEnity.Id;
                cTTaiSanInput.SoSeri = arrseri[i];
                cTTaiSanInput.MaSC = 0;
                cTTaiSanInput.MaTL = 0;
                cTTaiSanInput.MADC = 0;
                cTTaiSanInput.MaXuatTS = 0;
                cTTaiSanInput.MATH = 0;

                var cttaisanEnity = ObjectMapper.Map<CTTaiSan>(cTTaiSanInput);
                SetAuditInsert(cttaisanEnity);
                cttaisanrepository.Insert(cttaisanEnity);
                CurrentUnitOfWork.SaveChanges();
            }
            for (int i = arrseri.Count(); i < taiSanInput.SoLuong; i++)
            {
                CTTaiSanInput cTTaiSanInput = new CTTaiSanInput();
                cTTaiSanInput.MaLo = lotaisanEntity.Id;
                cTTaiSanInput.MaTS = taisanEnity.Id;
                cTTaiSanInput.MaSC = 0;
                cTTaiSanInput.MaTL = 0;
                cTTaiSanInput.MADC = 0;
                cTTaiSanInput.MaXuatTS = 0;
                cTTaiSanInput.MATH = 0;

                var cttaisanEnity = ObjectMapper.Map<CTTaiSan>(cTTaiSanInput);
                SetAuditInsert(cttaisanEnity);
                cttaisanrepository.Insert(cttaisanEnity);
                CurrentUnitOfWork.SaveChanges();
            }
           

        }

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient_Edit)]
        private void Update(TaiSanInput taiSanInput)
        {
            var taisanEnity = taisanrepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == taiSanInput.Id);
            if (taisanEnity == null)
            {
            }
            ObjectMapper.Map(taiSanInput, taisanEnity);
            SetAuditEdit(taisanEnity);
            taisanrepository.Update(taisanEnity);
            CurrentUnitOfWork.SaveChanges();
        }

        #endregion
    }
}
