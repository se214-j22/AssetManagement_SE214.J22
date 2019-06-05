using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using GWebsite.AbpZeroTemplate.Application;
using GWebsite.AbpZeroTemplate.Application.Share.SuaChuas;
using GWebsite.AbpZeroTemplate.Application.Share.SuaChuas.Dto;
using GWebsite.AbpZeroTemplate.Core.Authorization;
using GWebsite.AbpZeroTemplate.Core.Models;
using System.Linq;
using System.Linq.Dynamic.Core;
namespace GWebsite.AbpZeroTemplate.Web.Core.SuaChuas
{
    [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient)]
    public class SuaChuaAppService : GWebsiteAppServiceBase, ISuaChuaAppService
    {
        private readonly IRepository<SuaChua> suaChuaRepository;
        public SuaChuaAppService(IRepository<SuaChua> suaChuaRepository)
        {
            this.suaChuaRepository = suaChuaRepository;
        }
        public void CreateOrEditSuaChua(SuaChuaInput suaChuaInput)
        {
            if (suaChuaInput.Id == 0)
            {
                Create(suaChuaInput);
            }
            else
            {
                Update(suaChuaInput);
            }
        }

        public void DeleteSuaChua(int id)
        {
            var suaChuaEnity = suaChuaRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (suaChuaEnity != null)
            {
                suaChuaEnity.IsDelete = true;
                suaChuaRepository.Update(suaChuaEnity);
                CurrentUnitOfWork.SaveChanges();
            }
        }

        public SuaChuaInput GetSuaChuaForEdit(int id)
        {
            var suaChuaEnity = suaChuaRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (suaChuaEnity == null)
            {
                return null;
            }
            return ObjectMapper.Map<SuaChuaInput>(suaChuaEnity);
        }

        public SuaChuaForViewDto GetSuaChuaForView(int id)
        {
            var suaChuaEnity = suaChuaRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (suaChuaEnity == null)
            {
                return null;
            }
            return ObjectMapper.Map<SuaChuaForViewDto>(suaChuaEnity);
        }

        public PagedResultDto<SuaChuaDto> GetSuaChuas(SuaChuaFilter input)
        {
            var query = suaChuaRepository.GetAll().Where(x => !x.IsDelete);

            // filter by value
            if (input.TenNhanVienpT != null)
            {
                query = query.Where(x => x.TenNhanVienpT.ToLower().Equals(input.TenNhanVienpT));
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
            return new PagedResultDto<SuaChuaDto>(
                totalCount,
                items.Select(item => ObjectMapper.Map<SuaChuaDto>(item)).ToList());
        }
        #region Private Method

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient_Create)]
        private void Create(SuaChuaInput suaChuaInput)
        {
            var suaChuaEnity = ObjectMapper.Map<SuaChua>(suaChuaInput);
            SetAuditInsert(suaChuaEnity);
            suaChuaRepository.Insert(suaChuaEnity);
            CurrentUnitOfWork.SaveChanges();
        }

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient_Edit)]
        private void Update(SuaChuaInput suaChuaInput)
        {
            var suaChuaEnity = suaChuaRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == suaChuaInput.Id);
            if (suaChuaEnity == null)
            {
            }
            ObjectMapper.Map(suaChuaInput, suaChuaEnity);
            SetAuditEdit(suaChuaEnity);
            suaChuaRepository.Update(suaChuaEnity);
            CurrentUnitOfWork.SaveChanges();
        }

        #endregion
    }
}
