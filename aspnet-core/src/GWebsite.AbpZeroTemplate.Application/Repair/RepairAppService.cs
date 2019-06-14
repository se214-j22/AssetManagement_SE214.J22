using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using AutoMapper.QueryableExtensions;
using GWebsite.AbpZeroTemplate.Application;
using GWebsite.AbpZeroTemplate.Application.Share.Repairs;
using GWebsite.AbpZeroTemplate.Application.Share.Repairs.Dto;
using GWebsite.AbpZeroTemplate.Core.Authorization;
using GWebsite.AbpZeroTemplate.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace GWebsite.AbpZeroTemplate.Web.Core.Repairs
{
    [AbpAuthorize(GWebsitePermissions.Pages_Administration_Repair)]
    public class RepairAppService : GWebsiteAppServiceBase, IRepairAppService
    {
        private readonly IRepository<Repair> repairRepository;

        public RepairAppService(IRepository<Repair> repairRepository)
        {
            this.repairRepository = repairRepository;
        }

        #region Public Method

        public void CreateOrEditRepair(RepairInput repairInput)
        {
            if (repairInput.Id == 0)
            {
                Create(repairInput);
            }
            else
            {
                Update(repairInput);
            }
        }

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_Repair_Delete)]
        public void DeleteRepair(int id)
        {
            var repairEntity = repairRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (repairEntity != null)
            {
                repairEntity.IsDelete = true;
                repairRepository.Update(repairEntity);
                CurrentUnitOfWork.SaveChanges();
            }
        }

        public List<RepairForViewDto> GetListRepairByAssetId(string assetId)
        {
            IQueryable<Repair> query = repairRepository.GetAll().Where(x => !x.IsDelete).Where(x => x.AssetId == assetId);
            IQueryable<RepairForViewDto> repairDtoQuery = query.ProjectTo<RepairForViewDto>(query);
            return repairDtoQuery.ToList();
        }

        public RepairInput GetRepairForEdit(int id)
        {
            var repairEntity = repairRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (repairEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<RepairInput>(repairEntity);
        }

        public RepairForViewDto GetRepairForView(int id)
        {
            var repairEntity = repairRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (repairEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<RepairForViewDto>(repairEntity);
        }

        public PagedResultDto<RepairDto> GetRepairs(RepairFilter input)
        {
            var query = repairRepository.GetAll().Where(x => !x.IsDelete);

            // filter by value
            if (input.AssetId != null)
            {
                query = query.Where(x => x.AssetId.ToLower().Equals(input.AssetId));
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
            return new PagedResultDto<RepairDto>(
                totalCount,
                items.Select(item => ObjectMapper.Map<RepairDto>(item)).ToList());
        }

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_Repair_Approve)]
        public void ApproveRepair(int id)
        {
            var repairEntity = repairRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (repairEntity != null)
            {
                repairEntity.StatusApproved = true;
                repairRepository.Update(repairEntity);
                CurrentUnitOfWork.SaveChanges();
            }
        }

        #endregion

        #region Private Method

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_Repair_Create)]
        private void Create(RepairInput repairInput)
        {
            repairInput.StatusApproved = false;
            var repairEntity = ObjectMapper.Map<Repair>(repairInput);
            SetAuditInsert(repairEntity);
            repairRepository.Insert(repairEntity);
            CurrentUnitOfWork.SaveChanges();
        }

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_Repair_Edit)]
        private void Update(RepairInput repairInput)
        {
            var repairEntity = repairRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == repairInput.Id);
            if (repairEntity == null)
            {
            }
            ObjectMapper.Map(repairInput, repairEntity);
            SetAuditEdit(repairEntity);
            repairRepository.Update(repairEntity);
            CurrentUnitOfWork.SaveChanges();
        }

        #endregion
    }
}