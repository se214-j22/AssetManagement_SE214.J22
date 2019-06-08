using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using GWebsite.AbpZeroTemplate.Application;
using GWebsite.AbpZeroTemplate.Application.Share.ThietBiKemTheos;
using GWebsite.AbpZeroTemplate.Application.Share.ThietBiKemTheos.Dto;
using GWebsite.AbpZeroTemplate.Core.Authorization;
using GWebsite.AbpZeroTemplate.Core.Models;
using System.Linq.Dynamic.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Web.Core
{
    public class ThietBiKemTheoAppService : GWebsiteAppServiceBase, IThietBiKemTheoAppService
    {
        private readonly IRepository<ThietBiKemTheo> tbktRepository;

        public ThietBiKemTheoAppService (IRepository<ThietBiKemTheo> tbktRepository)
        {
            this.tbktRepository = tbktRepository;
        }

        public void CreateOrEditThietBiKemTheo(ThietBiKemTheoInput tbktInput)
        {
            if (tbktInput.Id == 0)
            {
                Create(tbktInput);
            }
            else
                Update(tbktInput);
        }
        [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient_Edit)]
        private void Update(ThietBiKemTheoInput tbktInput)
        {
            var tbktEntity = tbktRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == tbktInput.Id);
            if (tbktEntity == null)
            {
            }
            ObjectMapper.Map(tbktInput, tbktEntity);
            SetAuditEdit(tbktEntity);
            tbktRepository.Update(tbktEntity);
            CurrentUnitOfWork.SaveChanges();
        }
        [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient_Create)]
        private void Create(ThietBiKemTheoInput tbktInput)
        {
            var tbktEntity = ObjectMapper.Map<ThietBiKemTheo>(tbktInput);
            SetAuditInsert(tbktEntity);
            tbktRepository.Insert(tbktEntity);
            CurrentUnitOfWork.SaveChanges();
        }

        public void DeleteThietBiKemTheo(int id)
        {
            var tbktEntity = tbktRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if(tbktEntity!=null)
            {
                tbktEntity.IsDelete = true;
                tbktRepository.Update(tbktEntity);
                CurrentUnitOfWork.SaveChanges();
            }
        }

        public ThietBiKemTheoInput GetThietBiKemTheoForEdit(int id)
        {
            var tbktEntity = tbktRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if(tbktEntity!=null)
            {
                return ObjectMapper.Map<ThietBiKemTheoInput>(tbktEntity);
            }
            return null;
        }

        public ThietBiKemTheoForViewDto GetThietBiKemTheoForView(int id)
        {
            var tbktEntity = tbktRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if(tbktEntity!=null)
            {
                return ObjectMapper.Map<ThietBiKemTheoForViewDto>(tbktEntity);
            }
            return null;
        }

        public PagedResultDto<ThietBiKemTheoDto> GetThietBiKemTheos(ThietBiKemTheoFilter input)
        {
            var query = tbktRepository.GetAll().Where(x => !x.IsDelete);

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
            return new PagedResultDto<ThietBiKemTheoDto>(
                totalCount,
                items.Select(item => ObjectMapper.Map<ThietBiKemTheoDto>(item)).ToList());
        }
    }
}
