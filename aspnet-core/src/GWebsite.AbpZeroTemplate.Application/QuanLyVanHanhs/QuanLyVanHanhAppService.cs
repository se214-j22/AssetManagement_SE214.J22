using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using GWebsite.AbpZeroTemplate.Application;
using GWebsite.AbpZeroTemplate.Application.Share.QuanLyVanHanhs;
using GWebsite.AbpZeroTemplate.Application.Share.QuanLyVanHanhs.Dto;
using GWebsite.AbpZeroTemplate.Core.Authorization;
using GWebsite.AbpZeroTemplate.Core.Models;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace GWebsite.AbpZeroTemplate.Web.Core.QuanLyVanHanhs
{
    //[AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient)]
    public class QuanLyVanHanhAppService : GWebsiteAppServiceBase, IQuanLyVanHanhAppService
    {
        private readonly IRepository<QuanLyVanHanh> quanlyvanhanhRepository;

        public QuanLyVanHanhAppService(IRepository<QuanLyVanHanh> quanlyvanhanhRepository)
        {
            this.quanlyvanhanhRepository = quanlyvanhanhRepository;
        }

        #region Public Method

        public void CreateOrEditQuanLyVanHanh(QuanLyVanHanhInput quanlyvanhanhInput)
        {
            if (quanlyvanhanhInput.Id == 0)
            {
                Create(quanlyvanhanhInput);
            }
            else
            {
                Update(quanlyvanhanhInput);
            }
        }

        public void DeleteQuanLyVanHanh(int id)
        {
            var quanlyvanhanhEntity = quanlyvanhanhRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (quanlyvanhanhEntity != null)
            {
                quanlyvanhanhEntity.IsDelete = true;
                quanlyvanhanhRepository.Update(quanlyvanhanhEntity);
                CurrentUnitOfWork.SaveChanges();
            }
        }

        public QuanLyVanHanhInput GetQuanLyVanHanhForEdit(int id)
        {
            var quanlyvanhanhEntity = quanlyvanhanhRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (quanlyvanhanhEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<QuanLyVanHanhInput>(quanlyvanhanhEntity);
        }

        public QuanLyVanHanhForViewDto GetQuanLyVanHanhForView(int id)
        {
            var quanlyvanhanhEntity = quanlyvanhanhRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (quanlyvanhanhEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<QuanLyVanHanhForViewDto>(quanlyvanhanhEntity);
        }

        public PagedResultDto<QuanLyVanHanhDto> GetQuanLyVanHanhs(QuanLyVanHanhFilter input)
        {
            var query = quanlyvanhanhRepository.GetAll().Where(x => !x.IsDelete);

            // filter by value
            if (input.SoXe != null)
            {
                query = query.Where(x => x.SoXe.ToLower().Equals(input.SoXe));
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
            return new PagedResultDto<QuanLyVanHanhDto>(
                totalCount,
                items.Select(item => ObjectMapper.Map<QuanLyVanHanhDto>(item)).ToList());
        }

        #endregion

        #region Private Method

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient_Create)]
        private void Create(QuanLyVanHanhInput quanlyvanhanhInput)
        {
            var quanlyvanhanhEntity = ObjectMapper.Map<QuanLyVanHanh>(quanlyvanhanhInput);
            SetAuditInsert(quanlyvanhanhEntity);
            quanlyvanhanhRepository.Insert(quanlyvanhanhEntity);
            CurrentUnitOfWork.SaveChanges();
        }

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient_Edit)]
        private void Update(QuanLyVanHanhInput quanlyvanhanhInput)
        {
            var quanlyvanhanhEntity = quanlyvanhanhRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == quanlyvanhanhInput.Id);
            if (quanlyvanhanhEntity == null)
            {
            }
            ObjectMapper.Map(quanlyvanhanhInput, quanlyvanhanhEntity);
            SetAuditEdit(quanlyvanhanhEntity);
            quanlyvanhanhRepository.Update(quanlyvanhanhEntity);
            CurrentUnitOfWork.SaveChanges();
        }

        #endregion
    }
}
