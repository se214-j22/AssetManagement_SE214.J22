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
                query = query.Where(x => x.TenTs.ToLower().Contains(input.TenTs));
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
        public TaiSanInput getSoLuongTonTaiSan (int id)
        {

            var taisanEntity = taisanrepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (taisanEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<TaiSanInput>(taisanEntity);
        }
        #region Private Method

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient_Create)]
        private void Create(TaiSanInput taiSanInput)
        {
            for (int i = 0; i < taiSanInput.SoLuong; i++)
            {
                var manhomtaisan = nhomTaiSanrepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.tenNhomTaiSan == taiSanInput.TenNhomTS).Id;
                taiSanInput.NgayNhap = DateTime.Now.Date;
                taiSanInput.MaNhomTS = manhomtaisan;
                var taisanEnity = ObjectMapper.Map<ThongTinTaiSan>(taiSanInput);
                SetAuditInsert(taisanEnity);
                taisanrepository.Insert(taisanEnity);
                CurrentUnitOfWork.SaveChanges();
  
                switch (taisanEnity.MaNhomTS.ToString().Length)
                {
                    case 1:
                        taisanEnity.MaTS = "T" + "00" + manhomtaisan.ToString();
                        break;
                    case 2:
                        taisanEnity.MaTS = "T" + "0" + manhomtaisan.ToString();
                        break;
                    case 3:
                        taisanEnity.MaTS = "T" + manhomtaisan.ToString();
                        break;
                    default:
                        break;
                }
                switch (taisanEnity.Id.ToString().Length)
                {
                    case 1:
                        taisanEnity.MaTS += "00000" + taisanEnity.Id;
                        break;
                    case 2:
                        taisanEnity.MaTS += "0000" + taisanEnity.Id;
                        break;
                    case 3:
                        taisanEnity.MaTS += "000" + taisanEnity.Id;
                        break;
                    case 4:
                        taisanEnity.MaTS += "00" + taisanEnity.Id;
                        break;
                    case 5:
                        taisanEnity.MaTS += "0" + taisanEnity.Id;
                        break;
                    case 6:
                        taisanEnity.MaTS += taisanEnity.Id;
                        break;
                    default:
                        break;
                }
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
