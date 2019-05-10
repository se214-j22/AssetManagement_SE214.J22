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
        private readonly IRepository<DieuChuyen> dieuChuyenrepository;

        public DieuChuyenAppService(IRepository<DieuChuyen> dieuChuyenrepository)
        {
            this.dieuChuyenrepository = dieuChuyenrepository;
        }
        #region public method
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
            var dieuChuyenEntity = dieuChuyenrepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (dieuChuyenEntity != null)
            {
                dieuChuyenEntity.IsDelete = true;
                dieuChuyenrepository.Update(dieuChuyenEntity);
                CurrentUnitOfWork.SaveChanges();
            }
        }

        public DieuChuyenForViewDto GetDieuChuyenForView(int id)
        {
            
            var dieuChuyenEntity = dieuChuyenrepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (dieuChuyenEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<DieuChuyenForViewDto>(dieuChuyenEntity);
        }

        public DieuChuyenInput GetDieuChuyenForEdit(int id)
        {
            var dieuChuyenEntity = dieuChuyenrepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (dieuChuyenEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<DieuChuyenInput>(dieuChuyenEntity);
        }

        public PagedResultDto<DieuChuyenDto> GetDieuChuyens(DieuChuyenFilter input)
        {
            var query = dieuChuyenrepository.GetAll().Where(x => !x.IsDelete);

   

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
        #endregion
        #region private method
        private void Create(DieuChuyenInput dieuChuyenInput)
        {
            var dieuChuyenEntity = ObjectMapper.Map<DieuChuyen>(dieuChuyenInput);
            SetAuditInsert(dieuChuyenEntity);
            dieuChuyenrepository.Insert(dieuChuyenEntity);
            CurrentUnitOfWork.SaveChanges();
        }

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient_Edit)]
        private void Update(DieuChuyenInput dieuChuyenInput)
        {
            var dieuChuyenEntity = dieuChuyenrepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == dieuChuyenInput.Id);
            if (dieuChuyenEntity == null)
            {
            }
            ObjectMapper.Map(dieuChuyenInput, dieuChuyenEntity);
            SetAuditEdit(dieuChuyenEntity);
            dieuChuyenrepository.Update(dieuChuyenEntity);
            CurrentUnitOfWork.SaveChanges();
        }
        #endregion
    }
}
