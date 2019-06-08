using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using GWebsite.AbpZeroTemplate.Application;
using GWebsite.AbpZeroTemplate.Application.Share.PhiDuongBos;
using GWebsite.AbpZeroTemplate.Core.Authorization;
using GWebsite.AbpZeroTemplate.Core.Models;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace GWebsite.AbpZeroTemplate.Web.Core.PhiDuongBos
{
    [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient)]
    public class PhiDuongBoAppService : GWebsiteAppServiceBase, IPhiDuongBoAppService
    {
        private readonly IRepository<PhiDuongBo> phiDuongBoRepository;

        public PhiDuongBoAppService(IRepository<PhiDuongBo> phiDuongBoRepository)
        {
            this.phiDuongBoRepository = phiDuongBoRepository;
        }

        #region Public Method

        public void CreateOrEditPhiDuongBo(PhiDuongBoInput phiDuongBoInput)
        {
            if (phiDuongBoInput.Id == 0)
            {
                Create(phiDuongBoInput);
            }
            else
            {
                Update(phiDuongBoInput);
            }
        }

        public void DeletePhiDuongBo(int id)
        {
            var phiDuongBoEntity = phiDuongBoRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (phiDuongBoEntity != null)
            {
                phiDuongBoEntity.IsDelete = true;
                phiDuongBoRepository.Update(phiDuongBoEntity);
                CurrentUnitOfWork.SaveChanges();
            }
        }

        public PhiDuongBoInput GetPhiDuongBoForEdit(int id)
        {
            var phiDuongBoEntity = phiDuongBoRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (phiDuongBoEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<PhiDuongBoInput>(phiDuongBoEntity);
        }

        public PhiDuongBoForViewDTO GetPhiDuongBoForView(int id)
        {
            var phiDuongBoEntity = phiDuongBoRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (phiDuongBoEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<PhiDuongBoForViewDTO>(phiDuongBoEntity);
        }

        public PagedResultDto<PhiDuongBoDTO> GetPhiDuongBos(PhiDuongBoFilter input)
        {
            var query = phiDuongBoRepository.GetAll().Where(x => !x.IsDelete);

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
            return new PagedResultDto<PhiDuongBoDTO>(
                totalCount,
                items.Select(item => ObjectMapper.Map<PhiDuongBoDTO>(item)).ToList());
        }

        #endregion

        #region Private Method

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient_Create)]
        private void Create(PhiDuongBoInput phiDuongBoInput)
        {
            var phiDuongBoEntity = ObjectMapper.Map<PhiDuongBo>(phiDuongBoInput);
            SetAuditInsert(phiDuongBoEntity);
            phiDuongBoRepository.Insert(phiDuongBoEntity);
            CurrentUnitOfWork.SaveChanges();
        }

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient_Edit)]
        private void Update(PhiDuongBoInput phiDuongBoInput)
        {
            var phiDuongBoEntity = phiDuongBoRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == phiDuongBoInput.Id);
            if (phiDuongBoEntity == null)
            {
            }
            ObjectMapper.Map(phiDuongBoInput, phiDuongBoEntity);
            SetAuditEdit(phiDuongBoEntity);
            phiDuongBoRepository.Update(phiDuongBoEntity);
            CurrentUnitOfWork.SaveChanges();
        }

        #endregion
    }
}
