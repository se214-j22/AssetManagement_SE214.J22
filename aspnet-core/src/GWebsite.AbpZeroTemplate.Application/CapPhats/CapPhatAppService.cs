using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using GWebsite.AbpZeroTemplate.Application;
using GWebsite.AbpZeroTemplate.Application.Share.CapPhats;
using GWebsite.AbpZeroTemplate.Application.Share.CapPhats.Dto;
using GWebsite.AbpZeroTemplate.Core.Authorization;
using GWebsite.AbpZeroTemplate.Core.Models;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace GWebsite.AbpZeroTemplate.Web.Core.CapPhats
{
    [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient)]
    public class CapPhatAppService : GWebsiteAppServiceBase, ICapPhatAppService
    {
        private readonly IRepository<CapPhat> capPhatrepository;
        private readonly IRepository<TaiSan> taisanrepository;

        public CapPhatAppService(IRepository<CapPhat> capPhatrepository, IRepository<TaiSan> taisanrepository)
        {
            this.capPhatrepository = capPhatrepository;
            this.taisanrepository = taisanrepository;
        }

        #region public method

        public void CreateOrEditCapPhat(CapPhatInput capPhatInput)
        {
            if (capPhatInput.Id == 0)
            {
                Create(capPhatInput);
            }
            else
            {
                Update(capPhatInput);
            }
        }

        public void DeleteCapPhat(int id)
        {
            var capPhatEntity = capPhatrepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (capPhatEntity != null)
            {
                capPhatEntity.IsDelete = true;
                capPhatrepository.Update(capPhatEntity);
                CurrentUnitOfWork.SaveChanges();
            }
        }

        public CapPhatInput GetCapPhatForEdit(int id)
        {
            var capPhatEntity = capPhatrepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (capPhatEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<CapPhatInput>(capPhatEntity);
        }

        public CapPhatForViewDto GetCapPhatForView(int id)
        {
            var capPhatEntity = capPhatrepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (capPhatEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<CapPhatForViewDto>(capPhatEntity);
        }

        public PagedResultDto<CapPhatDto> GetCapPhats(CapPhatFilter input)
        {
            var query = capPhatrepository.GetAll().Where(x => !x.IsDelete);

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
            return new PagedResultDto<CapPhatDto>(
                totalCount,
                items.Select(item => ObjectMapper.Map<CapPhatDto>(item)).ToList());
        }    

        #endregion

        #region Private Method

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient_Create)]
        private void Create(CapPhatInput CapPhatInput)
        {            
            var taiSanEnity = taisanrepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == CapPhatInput.MaTaiSan);
           
            var capPhatEnity = ObjectMapper.Map<CapPhat>(CapPhatInput);
            SetAuditInsert(capPhatEnity);
            capPhatrepository.Insert(capPhatEnity);
            CurrentUnitOfWork.SaveChanges();
        }

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient_Edit)]
        private void Update(CapPhatInput CapPhatInput)
        {
            var capPhatEnity = capPhatrepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == CapPhatInput.Id);
            if (capPhatEnity == null)
            {
            }
            ObjectMapper.Map(CapPhatInput, capPhatEnity);
            SetAuditEdit(capPhatEnity);
            capPhatrepository.Update(capPhatEnity);
            CurrentUnitOfWork.SaveChanges();
        }

        #endregion
    }
}
