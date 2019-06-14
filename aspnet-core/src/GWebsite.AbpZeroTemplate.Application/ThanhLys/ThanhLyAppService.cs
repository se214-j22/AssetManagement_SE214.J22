using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using GWebsite.AbpZeroTemplate.Application;
using GWebsite.AbpZeroTemplate.Application.Share.ThanhLys;
using GWebsite.AbpZeroTemplate.Application.Share.ThanhLys.Dto;
using GWebsite.AbpZeroTemplate.Core.Authorization;
using GWebsite.AbpZeroTemplate.Core.Models;
using System.Linq;
using System.Linq.Dynamic.Core;
namespace GWebsite.AbpZeroTemplate.Web.Core.ThanhLys
{
    [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient)]
    public class ThanhLyAppService : GWebsiteAppServiceBase, IThanhLyAppService
    {
        private readonly IRepository<ThanhLy> thanhLyRepository;
        private readonly IRepository<DonVi> donvirepository;
        private readonly IRepository<ThongTinTaiSan> tttsrepository;
        public ThanhLyAppService(IRepository<ThanhLy> thanhLyRepository, IRepository<DonVi> donvirepository,
            IRepository<ThongTinTaiSan> tttsrepository)
        {
            this.thanhLyRepository = thanhLyRepository;
            this.donvirepository = donvirepository;
            this.tttsrepository = tttsrepository;
        }
        public void CreateOrEditThanhLy(ThanhLyInput thanhLyInput)
        {
            if (thanhLyInput.Id == 0)
            {
                Create(thanhLyInput);
            }
            else
            {
                Update(thanhLyInput);
            }
        }

        public void DeleteThanhLy(int id)
        {
            var thanhLyEnity = thanhLyRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (thanhLyEnity != null)
            {
                thanhLyEnity.IsDelete = true;
                thanhLyRepository.Update(thanhLyEnity);
                CurrentUnitOfWork.SaveChanges();
            }
        }

        public ThanhLyInput GetThanhLyForEdit(int id)
        {
            var thanhLyEnity = thanhLyRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (thanhLyEnity == null)
            {
                return null;
            }
            return ObjectMapper.Map<ThanhLyInput>(thanhLyEnity);
        }

        public ThanhLyForViewDto GetThanhLyForView(int id)
        {
            var thanhLyEnity = thanhLyRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (thanhLyEnity == null)
            {
                return null;
            }
            return ObjectMapper.Map<ThanhLyForViewDto>(thanhLyEnity);
        }

        public PagedResultDto<ThanhLyDto> GetThanhLys(ThanhLyFilter input)
        {
            var query = thanhLyRepository.GetAll().Where(x => !x.IsDelete);

            // filter by value
            if (input.TenTS != null)
            {
                query = query.Where(x => x.TenTS.ToLower().Contains(input.TenTS));
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
            return new PagedResultDto<ThanhLyDto>(
                totalCount,
                items.Select(item => ObjectMapper.Map<ThanhLyDto>(item)).ToList());
        }

        public string[] GetArrTenDonVi()
        {
            var query = donvirepository.GetAll().Where(x => !x.IsDelete).Select(x => x.TenDonVi).ToArray();
            string[] str = query.Select(x => x.ToString()).ToArray();
            return str;
        }

        #region Private Method

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient_Create)]
        private void Create(ThanhLyInput thanhLyInput)
        {
            var maDVMua = donvirepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.TenDonVi == thanhLyInput.DonViMua).Id;
            thanhLyInput.MaDonViMua = maDVMua;
            var thanhLyEnity = ObjectMapper.Map<ThanhLy>(thanhLyInput);
            SetAuditInsert(thanhLyEnity);
            thanhLyRepository.Insert(thanhLyEnity);
            CurrentUnitOfWork.SaveChanges();

            var updateTS = tttsrepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.MaTS == thanhLyInput.MaTS);
            updateTS.MaDV = thanhLyEnity.MaDonViMua;
            updateTS.TenDV = thanhLyEnity.DonViMua;
            updateTS.TinhTrang = "Đã thanh lý";
            CurrentUnitOfWork.SaveChanges();
        }

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient_Edit)]
        private void Update(ThanhLyInput thanhLyInput)
        {
            var thanhLyEnity = thanhLyRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == thanhLyInput.Id);
            if (thanhLyEnity == null)
            {
            }
            ObjectMapper.Map(thanhLyInput, thanhLyEnity);
            SetAuditEdit(thanhLyEnity);
            thanhLyRepository.Update(thanhLyEnity);
            CurrentUnitOfWork.SaveChanges();
        }

        #endregion
    }
}
