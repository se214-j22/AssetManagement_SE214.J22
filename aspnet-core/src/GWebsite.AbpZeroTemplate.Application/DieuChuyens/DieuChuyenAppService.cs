using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using GWebsite.AbpZeroTemplate.Application;
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
        public DieuChuyenAppService(IRepository<DieuChuyen> dieuChuyenRepository, IRepository<CTTaiSan> cttsrepository)
        {
            this.dieuChuyenRepository = dieuChuyenRepository;
            this.cttsrepository = cttsrepository;
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
        #region Private Method

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient_Create)]
        private void Create(DieuChuyenInput dieuChuyenInput)
        {
            var checksoluong = cttsrepository.GetAll().Where(x => !x.IsDelete).Select(x => x.MaXuatTS != 0 && x.MaTS == dieuChuyenInput.MaTaiSan).Count();
            if (dieuChuyenInput.SoLuong <= checksoluong)
            {
              
                var dieuChuyenEnity = ObjectMapper.Map<DieuChuyen>(dieuChuyenInput);
                SetAuditInsert(dieuChuyenEnity);
                dieuChuyenRepository.Insert(dieuChuyenEnity);
                CurrentUnitOfWork.SaveChanges();

                for (int i = 0; i < dieuChuyenInput.SoLuong; i++)
                {
                    var update = cttsrepository.GetAll().Where(x => !x.IsDelete).FirstOrDefault(x => x.MaXuatTS != 0 && x.MaTS == dieuChuyenInput.MaTaiSan);
                    update.MADC = dieuChuyenEnity.Id;
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
