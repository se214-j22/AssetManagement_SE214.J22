using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using GWebsite.AbpZeroTemplate.Application;
using GWebsite.AbpZeroTemplate.Application.Share.Transfers;
using GWebsite.AbpZeroTemplate.Application.Share.Transfers.Dto;
using GWebsite.AbpZeroTemplate.Core.Authorization;
using GWebsite.AbpZeroTemplate.Core.Models;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace GWebsite.AbpZeroTemplate.Web.Core.Transfers
{
    [AbpAuthorize(GWebsitePermissions.Pages_Administration_Transfer)]
    public class TransferAppService : GWebsiteAppServiceBase, ITransferAppService
    {
        private readonly IRepository<Transfer> transferRepository;

        public TransferAppService(IRepository<Transfer> transferRepository)
        {
            this.transferRepository = transferRepository;
        }

        #region Public Method

        public void CreateOrEditTransfer(TransferInput transferInput)
        {
            if (transferInput.Id == 0)
            {
                Create(transferInput);
            }
            else
            {
                Update(transferInput);
            }
        }

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_Transfer_Delete)]
        public void DeleteTransfer(int id)
        {
            var transferEntity = transferRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (transferEntity != null)
            {
                transferEntity.IsDelete = true;
                transferRepository.Update(transferEntity);
                CurrentUnitOfWork.SaveChanges();
            }
        }

        public TransferInput GetTransferForEdit(int id)
        {
            var transferEntity = transferRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (transferEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<TransferInput>(transferEntity);
        }

        public TransferForViewDto GetTransferForView(int id)
        {
            var transferEntity = transferRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (transferEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<TransferForViewDto>(transferEntity);
        }

        public PagedResultDto<TransferDto> GetTransfers(TransferFilter input)
        {
            var query = transferRepository.GetAll().Where(x => !x.IsDelete);

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
            return new PagedResultDto<TransferDto>(
                totalCount,
                items.Select(item => ObjectMapper.Map<TransferDto>(item)).ToList());
        }

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_Transfer_Approve)]
        public void ApproveTransfer(int id)
        {
            var transferEntity = transferRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (transferEntity != null)
            {
                transferEntity.StatusApproved = true;
                transferRepository.Update(transferEntity);
                CurrentUnitOfWork.SaveChanges();
            }
        }

        #endregion

        #region Private Method

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_Transfer_Create)]
        private void Create(TransferInput transferInput)
        {
            transferInput.StatusApproved = false;
            var transferEntity = ObjectMapper.Map<Transfer>(transferInput);
            SetAuditInsert(transferEntity);
            transferRepository.Insert(transferEntity);
            CurrentUnitOfWork.SaveChanges();
        }

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_Transfer_Edit)]
        private void Update(TransferInput transferInput)
        {
            var transferEntity = transferRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == transferInput.Id);
            if (transferEntity == null)
            {
            }
            ObjectMapper.Map(transferInput, transferEntity);
            SetAuditEdit(transferEntity);
            transferRepository.Update(transferEntity);
            CurrentUnitOfWork.SaveChanges();
        }

        #endregion
    }
}