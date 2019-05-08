using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using GWebsite.AbpZeroTemplate.Application;
using GWebsite.AbpZeroTemplate.Application.Share.ThongTinSuaChuas;
using GWebsite.AbpZeroTemplate.Application.Share.ThongTinSuaChuas.DTO;
using GWebsite.AbpZeroTemplate.Core.Authorization;
using GWebsite.AbpZeroTemplate.Core.Models;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace GWebsite.AbpZeroTemplate.Web.Core.ThongTinSuaChuas
{
    [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient)]
    public class ThongTinSuaChuaAppService : GWebsiteAppServiceBase, IThongTinSuaChuaAppService
    {
        private readonly IRepository<ThongTinSuaChua> thongTinSuaChuaRepository;

        public ThongTinSuaChuaAppService(IRepository<ThongTinSuaChua> thongTinSuaChuaRepository)
        {
            this.thongTinSuaChuaRepository = thongTinSuaChuaRepository;
        }

        #region Public Method

        public void CreateOrEditThongTinSuaChua(ThongTinSuaChuaInput thongTinSuaChuaInput)
        {
            if (thongTinSuaChuaInput.Id == 0)
            {
                Create(thongTinSuaChuaInput);
            }
            else
            {
                Update(thongTinSuaChuaInput);
            }
        }

        public void DeleteThongTinSuaChua(int id)
        {
            var thongTinSuaChuaEntity = thongTinSuaChuaRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (thongTinSuaChuaEntity != null)
            {
                thongTinSuaChuaEntity.IsDelete = true;
                thongTinSuaChuaRepository.Update(thongTinSuaChuaEntity);
                CurrentUnitOfWork.SaveChanges();
            }
        }

        public ThongTinSuaChuaInput GetThongTinSuaChuaForEdit(int id)
        {
            var thongTinSuaChuaEntity = thongTinSuaChuaRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (thongTinSuaChuaEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<ThongTinSuaChuaInput>(thongTinSuaChuaEntity);
        }

        public ThongTinSuaChuaForViewDTO GetThongTinSuaChuaForView(int id)
        {
            var thongTinSuaChuaEntity = thongTinSuaChuaRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (thongTinSuaChuaEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<ThongTinSuaChuaForViewDTO>(thongTinSuaChuaEntity);
        }

        public PagedResultDto<ThongTinSuaChuaDTO> GetThongTinSuaChuas(ThongTinSuaChuaFilter input)
        {
            var query = thongTinSuaChuaRepository.GetAll().Where(x => !x.IsDelete);

            // filter by value
            if (input.soXe != null)
            {
                query = query.Where(x => x.soXe.ToLower().Equals(input.soXe));
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
            return new PagedResultDto<ThongTinSuaChuaDTO>(
                totalCount,
                items.Select(item => ObjectMapper.Map<ThongTinSuaChuaDTO>(item)).ToList());
        }

      

        #endregion

        #region Private Method

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient_Create)]
        private void Create(ThongTinSuaChuaInput thongTinSuaChuaInput)
        {
            var thongTinSuaChuaEntity = ObjectMapper.Map<ThongTinSuaChua>(thongTinSuaChuaInput);
            SetAuditInsert(thongTinSuaChuaEntity);
            thongTinSuaChuaRepository.Insert(thongTinSuaChuaEntity);
            CurrentUnitOfWork.SaveChanges();
        }

        ThongTinSuaChuaInput IThongTinSuaChuaAppService.GetThongTinSuaChuaForEdit(int id)
        {
            throw new System.NotImplementedException();
        }

        ThongTinSuaChuaForViewDTO IThongTinSuaChuaAppService.GetThongTinSuaChuaForView(int id)
        {
            throw new System.NotImplementedException();
        }

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient_Edit)]
        private void Update(ThongTinSuaChuaInput thongTinSuaChuaInput)
        {
            var thongTinSuaChuaEntity = thongTinSuaChuaRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == thongTinSuaChuaInput.Id);
            if (thongTinSuaChuaEntity == null)
            {
            }
            ObjectMapper.Map(thongTinSuaChuaInput, thongTinSuaChuaEntity);
            SetAuditEdit(thongTinSuaChuaEntity);
            thongTinSuaChuaRepository.Update(thongTinSuaChuaEntity);
            CurrentUnitOfWork.SaveChanges();
        }

        #endregion
    }
}
