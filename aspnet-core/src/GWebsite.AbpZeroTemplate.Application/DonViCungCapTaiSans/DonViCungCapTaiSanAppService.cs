using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using GWebsite.AbpZeroTemplate.Application;
using GWebsite.AbpZeroTemplate.Application.Share.DonViCungCapTaiSans;
using GWebsite.AbpZeroTemplate.Application.Share.DonViCungCapTaiSans.Dto;
using GWebsite.AbpZeroTemplate.Core.Authorization;
using GWebsite.AbpZeroTemplate.Core.Models;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace GWebsite.AbpZeroTemplate.Web.Core.DonViCungCapTaiSans
{
    [AbpAuthorize(GWebsitePermissions.Pages_Administration_DonViCungCapTaiSan)]
    public class DonViCungCapTaiSanAppService : GWebsiteAppServiceBase, IDonViCungCapTaiSanAppService
    {
        private readonly IRepository<DonViCungCapTaiSan> donViCungCapTaiSanRepository;

        public DonViCungCapTaiSanAppService(IRepository<DonViCungCapTaiSan> donViCungCapTaiSanRepository)
        {
            this.donViCungCapTaiSanRepository = donViCungCapTaiSanRepository;
        }

        #region Public Method

        public void CreateOrEditDonViCungCapTaiSan(DonViCungCapTaiSanInput donViCungCapTaiSanInput)
        {
            if (donViCungCapTaiSanInput.Id == 0)
            {
                Create(donViCungCapTaiSanInput);
            }
            else
            {
                Update(donViCungCapTaiSanInput);
            }
        }

        public void DeleteDonViCungCapTaiSan(int id)
        {
            var donViCungCapTaiSanEntity = donViCungCapTaiSanRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (donViCungCapTaiSanEntity != null)
            {
                donViCungCapTaiSanEntity.IsDelete = true;
                donViCungCapTaiSanRepository.Update(donViCungCapTaiSanEntity);
                CurrentUnitOfWork.SaveChanges();
            }
        }

        public DonViCungCapTaiSanInput GetDonViCungCapTaiSanForEdit(int id)
        {
            var donViCungCapTaiSanEntity = donViCungCapTaiSanRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (donViCungCapTaiSanEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<DonViCungCapTaiSanInput>(donViCungCapTaiSanEntity);
        }

        public DonViCungCapTaiSanForViewDto GetDonViCungCapTaiSanForView(int id)
        {
            var donViCungCapTaiSanEntity = donViCungCapTaiSanRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (donViCungCapTaiSanEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<DonViCungCapTaiSanForViewDto>(donViCungCapTaiSanEntity);
        }

        public PagedResultDto<DonViCungCapTaiSanDto> GetDonViCungCapTaiSans(DonViCungCapTaiSanFilter input)
        {
            var query = donViCungCapTaiSanRepository.GetAll().Where(x => !x.IsDelete);

            // filter by value
            if (input.TenDonViCungCap != null)
            {
                query = query.Where(x => x.TenDonViCungCap.ToLower().Equals(input.TenDonViCungCap));
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
            return new PagedResultDto<DonViCungCapTaiSanDto>(
                totalCount,
                items.Select(item => ObjectMapper.Map<DonViCungCapTaiSanDto>(item)).ToList());
        }

        #endregion

        #region Private Method

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_DonViCungCapTaiSan_Create)]
        private void Create(DonViCungCapTaiSanInput donViCungCapTaiSanInput)
        {
            var donViCungCapTaiSanEntity = ObjectMapper.Map<DonViCungCapTaiSan>(donViCungCapTaiSanInput);
            SetAuditInsert(donViCungCapTaiSanEntity);
            donViCungCapTaiSanRepository.Insert(donViCungCapTaiSanEntity);
            CurrentUnitOfWork.SaveChanges();
        }

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_DonViCungCapTaiSan_Edit)]
        private void Update(DonViCungCapTaiSanInput donViCungCapTaiSanInput)
        {
            var donViCungCapTaiSanEntity = donViCungCapTaiSanRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == donViCungCapTaiSanInput.Id);
            if (donViCungCapTaiSanEntity == null)
            {
            }
            ObjectMapper.Map(donViCungCapTaiSanInput, donViCungCapTaiSanEntity);
            SetAuditEdit(donViCungCapTaiSanEntity);
            donViCungCapTaiSanRepository.Update(donViCungCapTaiSanEntity);
            CurrentUnitOfWork.SaveChanges();
        }

        #endregion
    }
}