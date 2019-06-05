using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using GWebsite.AbpZeroTemplate.Application;
using GWebsite.AbpZeroTemplate.Application.Share.ChiNhanhs;
using GWebsite.AbpZeroTemplate.Application.Share.ChiNhanhs.Dto;
using GWebsite.AbpZeroTemplate.Core.Authorization;
using GWebsite.AbpZeroTemplate.Core.Models;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace GWebsite.AbpZeroTemplate.Web.Core.ChiNhanhs
{
    [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient)]
    public class ChiNhanhAppService : GWebsiteAppServiceBase, IChiNhanhAppService
    {
        private readonly IRepository<ChiNhanh> chiNhanhRepository;

        public ChiNhanhAppService(IRepository<ChiNhanh> chiNhanhRepository)
        {
            this.chiNhanhRepository = chiNhanhRepository;
        }

        #region Public Method

        public void CreateOrEditChiNhanh(ChiNhanhInput chiNhanhInput)
        {
            if (chiNhanhInput.Id == 0)
            {
                Create(chiNhanhInput);
            }
            else
            {
                Update(chiNhanhInput);
            }
        }

        public void DeleteChiNhanh(int id)
        {
            var chiNhanhEntity = chiNhanhRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (chiNhanhEntity != null)
            {
                chiNhanhEntity.IsDelete = true;
                chiNhanhRepository.Update(chiNhanhEntity);
                CurrentUnitOfWork.SaveChanges();
            }
        }

        public ChiNhanhInput GetChiNhanhForEdit(int id)
        {
            var chiNhanhEntity = chiNhanhRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (chiNhanhEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<ChiNhanhInput>(chiNhanhEntity);
        }

        public ChiNhanhForViewDto GetChiNhanhForView(int id)
        {
            var chiNhanhEntity = chiNhanhRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (chiNhanhEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<ChiNhanhForViewDto>(chiNhanhEntity);
        }

        public PagedResultDto<ChiNhanhDto> GetChiNhanhs(ChiNhanhFilter input)
        {
            var query = chiNhanhRepository.GetAll().Where(x => !x.IsDelete);

            // filter by value
            if (input.TenChiNhanh != null)
            {
                query = query.Where(x => x.TenChiNhanh.ToLower().Equals(input.TenChiNhanh));
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
            return new PagedResultDto<ChiNhanhDto>(
                totalCount,
                items.Select(item => ObjectMapper.Map<ChiNhanhDto>(item)).ToList());
        }

        #endregion

        #region Private Method

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient_Create)]
        private void Create(ChiNhanhInput chiNhanhInput)
        {
            var chiNhanhEntity = ObjectMapper.Map<ChiNhanh>(chiNhanhInput);
            SetAuditInsert(chiNhanhEntity);
            chiNhanhRepository.Insert(chiNhanhEntity);
            CurrentUnitOfWork.SaveChanges();
        }

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient_Edit)]
        private void Update(ChiNhanhInput chiNhanhInput)
        {
            var chiNhanhEntity = chiNhanhRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == chiNhanhInput.Id);
            if (chiNhanhEntity == null)
            {
            }
            ObjectMapper.Map(chiNhanhInput, chiNhanhEntity);
            SetAuditEdit(chiNhanhEntity);
            chiNhanhRepository.Update(chiNhanhEntity);
            CurrentUnitOfWork.SaveChanges();
        }

        #endregion
    }
}
