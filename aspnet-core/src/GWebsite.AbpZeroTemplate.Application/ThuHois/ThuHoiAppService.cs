using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using GWebsite.AbpZeroTemplate.Application;
using GWebsite.AbpZeroTemplate.Application.Share.ThuHois;
using GWebsite.AbpZeroTemplate.Application.Share.ThuHois.Dto;
using GWebsite.AbpZeroTemplate.Core.Authorization;
using GWebsite.AbpZeroTemplate.Core.Models;
using System.Linq;
using System.Linq.Dynamic.Core;
namespace GWebsite.AbpZeroTemplate.Web.Core.ThuHois
{
    [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient)]
    public class ThuHoiAppService:GWebsiteAppServiceBase,IThuHoiAppService
    {
        private readonly IRepository<ThuHoi> thuHoirepository;
        private readonly IRepository<CapPhat> capPhatrepository;
        private readonly IRepository<TaiSan> taisanrepository;

        public ThuHoiAppService(IRepository<ThuHoi> thuHoirepository, IRepository<CapPhat> capPhatrepository, IRepository<TaiSan> taisanrepository)
        {
            this.thuHoirepository = thuHoirepository;
            this.capPhatrepository = capPhatrepository;
            this.taisanrepository = taisanrepository;
        }
        public void CreateOrEditThuHoi(ThuHoiInput thuHoiInput)
        {
            if (thuHoiInput.Id == 0)
            {
                Create(thuHoiInput);
            }
            else
            {
                Update(thuHoiInput);
            }
        }

        public void DeleteThuHoi(int id)
        {
            var thuHoiEnity = thuHoirepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (thuHoiEnity != null)
            {
                thuHoiEnity.IsDelete = true;
                thuHoirepository.Update(thuHoiEnity);
                CurrentUnitOfWork.SaveChanges();
            }
        }

        public ThuHoiForViewDto GetThuHoiForView(int id)
        {
            var thuHoiEnity = thuHoirepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (thuHoiEnity == null)
            {
                return null;
            }
            return ObjectMapper.Map<ThuHoiForViewDto>(thuHoiEnity);
        }

        public ThuHoiInput GetThuHoiForEdit(int id)
        {
            var thuHoiEnity = thuHoirepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (thuHoiEnity == null)
            {
                return null;
            }
            return ObjectMapper.Map<ThuHoiInput>(thuHoiEnity);
        }

        public PagedResultDto<ThuHoiDto> GetThuHois(ThuHoiFilter input)
        {
            var query = thuHoirepository.GetAll().Where(x => !x.IsDelete);

            // filter by value
           

            var totalCount = query.Count();

            // sorting
            if (!string.IsNullOrWhiteSpace(input.Sorting))
            {
                query = query.OrderBy(input.Sorting);
            }

            // paging
            var items = query.PageBy(input).ToList();

            // result
            return new PagedResultDto<ThuHoiDto>(
                totalCount,
                items.Select(item => ObjectMapper.Map<ThuHoiDto>(item)).ToList());
        }
        #region Private Method

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient_Create)]
        private void Create(ThuHoiInput thuHoiInput)
        {
            var capPhatEnity = capPhatrepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.TenDonVi == thuHoiInput.TenDonViThuHoi && x.MaTaiSan == thuHoiInput.MaTaiSan);
            if (capPhatEnity != null && thuHoiInput.SoLuong > capPhatEnity.SoLuong || capPhatEnity == null)
            {
                return;
            }

            var thuHoiEnity = ObjectMapper.Map<ThuHoi>(thuHoiInput);
            SetAuditInsert(thuHoiEnity);
            thuHoirepository.Insert(thuHoiEnity);

           

            CurrentUnitOfWork.SaveChanges();
        }

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient_Edit)]
        private void Update(ThuHoiInput thuHoiInput)
        {
            var thuHoiEnity = thuHoirepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == thuHoiInput.Id);
            if (thuHoiEnity == null)
            {
            }
            ObjectMapper.Map(thuHoiInput, thuHoiEnity);
            SetAuditEdit(thuHoiEnity);
            thuHoirepository.Update(thuHoiEnity);
            CurrentUnitOfWork.SaveChanges();
        }

        #endregion
    }
}
