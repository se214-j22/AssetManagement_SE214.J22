using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using GWebsite.AbpZeroTemplate.Application;
using GWebsite.AbpZeroTemplate.Application.Share.NhanViens;
using GWebsite.AbpZeroTemplate.Application.Share.NhanViens.Dto;
using GWebsite.AbpZeroTemplate.Core.Authorization;
using GWebsite.AbpZeroTemplate.Core.Models;
using System.Linq;
using System.Linq.Dynamic.Core;
namespace GWebsite.AbpZeroTemplate.Web.Core.NhanViens
{
    [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient)]
    public class NhanVienAppService : GWebsiteAppServiceBase, INhanVienAppService
    {
        private readonly IRepository<NhanVien> nhanVienRepository;
        public NhanVienAppService(IRepository<NhanVien> nhanVienRepository)
        {
            this.nhanVienRepository = nhanVienRepository;
        }
        public void CreateOrEditNhanVien(NhanVienInput nhanVienInput)
        {
            if (nhanVienInput.Id == 0)
            {
                Create(nhanVienInput);
            }
            else
            {
                Update(nhanVienInput);
            }
        }

        public void DeleteNhanVien(int id)
        {
            var nhanVienEnity = nhanVienRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (nhanVienEnity != null)
            {
                nhanVienEnity.IsDelete = true;
                nhanVienRepository.Update(nhanVienEnity);
                CurrentUnitOfWork.SaveChanges();
            }
        }

        public NhanVienInput GetNhanVienForEdit(int id)
        {
            var nhanVienEnity = nhanVienRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (nhanVienEnity == null)
            {
                return null;
            }
            return ObjectMapper.Map<NhanVienInput>(nhanVienEnity);
        }

        public NhanVienForViewDto GetNhanVienForView(int id)
        {
            var nhanVienEnity = nhanVienRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (nhanVienEnity == null)
            {
                return null;
            }
            return ObjectMapper.Map<NhanVienForViewDto>(nhanVienEnity);
        }

        public PagedResultDto<NhanVienDto> GetNhanViens(NhanVienFilter input)
        {
            var query = nhanVienRepository.GetAll().Where(x => !x.IsDelete);

            // filter by value
            if (input.TenNhanVien != null)
            {
                query = query.Where(x => x.TenNhanVien.ToLower().Equals(input.TenNhanVien));
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
            return new PagedResultDto<NhanVienDto>(
                totalCount,
                items.Select(item => ObjectMapper.Map<NhanVienDto>(item)).ToList());
        }
        #region Private Method

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient_Create)]
        private void Create(NhanVienInput nhanVienInput)
        {
            var nhanVienEnity = ObjectMapper.Map<NhanVien>(nhanVienInput);
            SetAuditInsert(nhanVienEnity);
            nhanVienRepository.Insert(nhanVienEnity);
            CurrentUnitOfWork.SaveChanges();
        }

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient_Edit)]
        private void Update(NhanVienInput nhanVienInput)
        {
            var nhanVienEnity = nhanVienRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == nhanVienInput.Id);
            if (nhanVienEnity == null)
            {
            }
            ObjectMapper.Map(nhanVienInput, nhanVienEnity);
            SetAuditEdit(nhanVienEnity);
            nhanVienRepository.Update(nhanVienEnity);
            CurrentUnitOfWork.SaveChanges();
        }

        #endregion
    }
}
