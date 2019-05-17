using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using GWebsite.AbpZeroTemplate.Application;
using GWebsite.AbpZeroTemplate.Application.Share.PhongBan;
using GWebsite.AbpZeroTemplate.Application.Share.PhongBan.Dto;
using GWebsite.AbpZeroTemplate.Core.Authorization;
using GWebsite.AbpZeroTemplate.Core.Models;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace GWebsite.AbpZeroTemplate.Web.Core.PhongBans
{
    [AbpAuthorize(GWebsitePermissions.Pages_Administration_PhongBan)]
    public class PhongBanAppService : GWebsiteAppServiceBase, IPhongBanAppService
    {
        private readonly IRepository<PhongBan> phongBanRepository;

        public PhongBanAppService(IRepository<PhongBan> phongBanRepository)
        {
            this.phongBanRepository = phongBanRepository;
        }

        #region Public Method

        public void CreateOrEditPhongBan(PhongBanInput phongBanInput)
        {
            if (phongBanInput.Id == 0)
            {
                Create(phongBanInput);
            }
            else
            {
                Update(phongBanInput);
            }
        }

        public void DeletePhongBan(int id)
        {
            var phongBanEntity = phongBanRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (phongBanEntity != null)
            {
                phongBanEntity.IsDelete = true;
                phongBanRepository.Update(phongBanEntity);
                CurrentUnitOfWork.SaveChanges();
            }
        }

        public PhongBanInput GetPhongBanForEdit(int id)
        {
            var phongBanEntity = phongBanRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (phongBanEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<PhongBanInput>(phongBanEntity);
        }

        public PhongBanForViewDto GetPhongBanForView(int id)
        {
            var phongBanEntity = phongBanRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (phongBanEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<PhongBanForViewDto>(phongBanEntity);
        }

        public PagedResultDto<PhongBanDto> GetPhongBans(PhongBanFilter input)
        {
            var query = phongBanRepository.GetAll().Where(x => !x.IsDelete);

            // filter by value
            if (input.PhongBanId != null) {
                query = query.Where(x => x.Id == input.PhongBanId);
            }
            if (input.TenPhong != null)
            {
                query = query.Where(x => x.TenPhong.ToLower().Equals(input.TenPhong));
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
            return new PagedResultDto<PhongBanDto>(
                totalCount,
                items.Select(item => ObjectMapper.Map<PhongBanDto>(item)).ToList());
        }

        #endregion

        #region Private Method

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_PhongBan_Create)]
        private void Create(PhongBanInput phongBanInput)
        {
            var phongBanEntity = ObjectMapper.Map<PhongBan>(phongBanInput);
            SetAuditInsert(phongBanEntity);
            phongBanRepository.Insert(phongBanEntity);
            CurrentUnitOfWork.SaveChanges();
        }

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_PhongBan_Edit)]
        private void Update(PhongBanInput phongBanInput)
        {
            var phongBanEntity = phongBanRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == phongBanInput.Id);
            if (phongBanEntity == null)
            {
            }
            ObjectMapper.Map(phongBanInput, phongBanEntity);
            SetAuditEdit(phongBanEntity);
            phongBanRepository.Update(phongBanEntity);
            CurrentUnitOfWork.SaveChanges();
        }

        #endregion
    }
}