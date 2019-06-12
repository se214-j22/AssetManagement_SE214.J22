using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using GWebsite.AbpZeroTemplate.Application;
using GWebsite.AbpZeroTemplate.Application.Share.DonVis;
using GWebsite.AbpZeroTemplate.Application.Share.DonVis.Dto;
using GWebsite.AbpZeroTemplate.Core.Authorization;
using GWebsite.AbpZeroTemplate.Core.Models;
using System.Linq;
using System.Linq.Dynamic.Core;
namespace GWebsite.AbpZeroTemplate.Web.Core.DonVis
{
    [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient)]
    public class DonViAppService : GWebsiteAppServiceBase, IDonViAppService
    {
        private readonly IRepository<DonVi> donvirepository;
        public DonViAppService(IRepository<DonVi> donvirepository)
        {
            this.donvirepository = donvirepository;
        }
        public void CreateOrEditDonVi(DonViInput donViInput)
        {
            if (donViInput.Id == 0)
            {
                Create(donViInput);
            }
            else
            {
                Update(donViInput);
            }
        }

        public void DeleteDonVi(int id)
        {
            var donviEntity = donvirepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (donviEntity != null)
            {
                donviEntity.IsDelete = true;
                donvirepository.Update(donviEntity);
                CurrentUnitOfWork.SaveChanges();
            }
        }

        public DonViInput GetDonViForEdit(int id)
        {
            var donviEntity = donvirepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (donviEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<DonViInput>(donviEntity);
        }

        public DonViForViewDto GetDonViForView(int id)
        {
            var donviEntity = donvirepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (donviEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<DonViForViewDto>(donviEntity);
        }

        public PagedResultDto<DonViDto> GetDonVis(DonViFilter input)
        {
            var query = donvirepository.GetAll().Where(x => !x.IsDelete);

            // filter by value
            if (input.TenDonVi != null)
            {
                query = query.Where(x => x.TenDonVi.ToLower().Contains(input.TenDonVi));
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
            return new PagedResultDto<DonViDto>(
                totalCount,
                items.Select(item => ObjectMapper.Map<DonViDto>(item)).ToList());
        }
        #region Private Method

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient_Create)]
        private void Create(DonViInput donViInput)
        {
            var donviEnity = ObjectMapper.Map<DonVi>(donViInput);
            SetAuditInsert(donviEnity);
            donvirepository.Insert(donviEnity);
            CurrentUnitOfWork.SaveChanges();
        }

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient_Edit)]
        private void Update(DonViInput donViInput)
        {
            var donviEnity = donvirepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == donViInput.Id);
            if (donviEnity == null)
            {
            }
            ObjectMapper.Map(donViInput, donviEnity);
            SetAuditEdit(donviEnity);
            donvirepository.Update(donviEnity);
            CurrentUnitOfWork.SaveChanges();
        }

        #endregion
    }
}
