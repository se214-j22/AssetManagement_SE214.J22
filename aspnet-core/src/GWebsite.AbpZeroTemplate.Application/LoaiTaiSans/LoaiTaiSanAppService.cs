using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using GWebsite.AbpZeroTemplate.Application;
using GWebsite.AbpZeroTemplate.Application.Share.LoaiTaiSans;
using GWebsite.AbpZeroTemplate.Application.Share.LoaiTaiSans.Dto;
using GWebsite.AbpZeroTemplate.Core.Authorization;
using GWebsite.AbpZeroTemplate.Core.Models;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace GWebsite.AbpZeroTemplate.Web.Core.LoaiTaiSans
{
    [AbpAuthorize(GWebsitePermissions.Pages_Administration_LoaiTaiSan)]
    public class LoaiTaiSanAppService : GWebsiteAppServiceBase, ILoaiTaiSanAppService
    {
        private readonly IRepository<LoaiTaiSan> loaiTaiSanRepository;

        public LoaiTaiSanAppService(IRepository<LoaiTaiSan> loaiTaiSanRepository)
        {
            this.loaiTaiSanRepository = loaiTaiSanRepository;
        }

        #region Public Method

        public void CreateOrEditLoaiTaiSan(LoaiTaiSanInput loaiTaiSanInput)
        {
            if (loaiTaiSanInput.Id == 0)
            {
                Create(loaiTaiSanInput);
            }
            else
            {
                Update(loaiTaiSanInput);
            }
        }

        public void DeleteLoaiTaiSan(int id)
        {
            var loaiTaiSanEntity = loaiTaiSanRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (loaiTaiSanEntity != null)
            {
                loaiTaiSanEntity.IsDelete = true;
                loaiTaiSanRepository.Update(loaiTaiSanEntity);
                CurrentUnitOfWork.SaveChanges();
            }
        }

        public LoaiTaiSanInput GetLoaiTaiSanForEdit(int id)
        {
            var loaiTaiSanEntity = loaiTaiSanRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (loaiTaiSanEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<LoaiTaiSanInput>(loaiTaiSanEntity);
        }

        public LoaiTaiSanForViewDto GetLoaiTaiSanForView(int id)
        {
            var loaiTaiSanEntity = loaiTaiSanRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (loaiTaiSanEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<LoaiTaiSanForViewDto>(loaiTaiSanEntity);
        }

        public PagedResultDto<LoaiTaiSanDto> GetLoaiTaiSans(LoaiTaiSanFilter input)
        {
            var query = loaiTaiSanRepository.GetAll().Where(x => !x.IsDelete);

            // filter by value
            if (input.TenNhomTaiSan != null)
            {
                query = query.Where(x => x.TenNhomTaiSan.ToLower().Equals(input.TenNhomTaiSan));
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
            return new PagedResultDto<LoaiTaiSanDto>(
                totalCount,
                items.Select(item => ObjectMapper.Map<LoaiTaiSanDto>(item)).ToList());
        }

        #endregion

        #region Private Method

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_LoaiTaiSan_Create)]
        private void Create(LoaiTaiSanInput loaiTaiSanInput)
        {
            var loaiTaiSanEntity = ObjectMapper.Map<LoaiTaiSan>(loaiTaiSanInput);
            SetAuditInsert(loaiTaiSanEntity);
            loaiTaiSanRepository.Insert(loaiTaiSanEntity);
            CurrentUnitOfWork.SaveChanges();
        }

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_LoaiTaiSan_Edit)]
        private void Update(LoaiTaiSanInput loaiTaiSanInput)
        {
            var loaiTaiSanEntity = loaiTaiSanRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == loaiTaiSanInput.Id);
            if (loaiTaiSanEntity == null)
            {
            }
            ObjectMapper.Map(loaiTaiSanInput, loaiTaiSanEntity);
            SetAuditEdit(loaiTaiSanEntity);
            loaiTaiSanRepository.Update(loaiTaiSanEntity);
            CurrentUnitOfWork.SaveChanges();
        }

        #endregion
    }
}