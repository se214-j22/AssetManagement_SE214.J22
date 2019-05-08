using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using GWebsite.AbpZeroTemplate.Application;
using GWebsite.AbpZeroTemplate.Application.Share.TaiSans;
using GWebsite.AbpZeroTemplate.Application.Share.TaiSans.Dto;
using GWebsite.AbpZeroTemplate.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Abp.Authorization;
using GWebsite.AbpZeroTemplate.Core.Authorization;

namespace GWebsite.AbpZeroTemplate.Web.Core.TaiSans
{
    public class TaiSanAppService : GWebsiteAppServiceBase, ITaiSanAppService
    {
        public readonly IRepository<TaiSan> taiSanRepository;
        public TaiSanAppService(IRepository<TaiSan> taiSanRepository)
        {
            this.taiSanRepository = taiSanRepository;
        }
        public void CreateOrEditTaiSan(TaiSanInput taiSanInput)
        {
            if(taiSanInput.Id==0)
            {
                Create(taiSanInput);

            }
            else
            {
                Update(taiSanInput);
            }
        }
        [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient_Edit)]
        private void Update(TaiSanInput taiSanInput)
        {
            var taiSanEntity = taiSanRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == taiSanInput.Id);
            if(taiSanEntity==null)
            { }
            ObjectMapper.Map(taiSanInput, taiSanEntity);
            SetAuditEdit(taiSanEntity);
            taiSanRepository.Update(taiSanEntity);
            CurrentUnitOfWork.SaveChanges();
        }

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient_Create)]
        private void Create(TaiSanInput taiSanInput)
        {
            var taiSanEntity = ObjectMapper.Map<TaiSan>(taiSanInput);
            SetAuditInsert(taiSanEntity);
            taiSanRepository.Insert(taiSanEntity);
            CurrentUnitOfWork.SaveChanges();
        }

        public void DeleteTaiSan(int id)
        {
            var taisanEntity = taiSanRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if(taisanEntity==null)
            {  }
            else
            {
                taisanEntity.IsDelete = true;
                taiSanRepository.Update(taisanEntity);
                CurrentUnitOfWork.SaveChanges();
            }
        }

        public TaiSanInput GetTaiSanForEdit(string maTaiSan)
        {
            var taisanEntity = taiSanRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.maTaiSan == maTaiSan);
            if (taisanEntity != null)
            {
                return ObjectMapper.Map<TaiSanInput>(taisanEntity);
            }
            else
                return null;

        }

        public TaiSanForViewDto GetTaiSanForView(string maTaiSan)
        {
            var taisanEntity = taiSanRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.maTaiSan == maTaiSan);
            if (taisanEntity != null)
            {
                return ObjectMapper.Map<TaiSanForViewDto>(taisanEntity);
            }
            else
                return null;
        }

        public PagedResultDto<TaiSanDto> GetTaiSans(TaiSanFilter filter)
        {
            var query = taiSanRepository.GetAll().Where(x => !x.IsDelete);
            if(filter.maTaiSan!=null)
            {
                query = query.Where(x => x.maTaiSan.ToLower().Equals(filter.maTaiSan));
            }
            var total = query.Count();
            if(!string.IsNullOrWhiteSpace(filter.Sorting))
            {
                query = query.OrderBy(filter.Sorting);
            }
            var items = query.PageBy(filter).ToList();
            return new PagedResultDto<TaiSanDto>(total, items.Select(item => ObjectMapper.Map<TaiSanDto>(item)).ToList());
        }
    }
}
