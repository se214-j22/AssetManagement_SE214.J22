using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Transfers;
using GWebsite.AbpZeroTemplate.Application.Share.Transfers.Dto;
using Microsoft.AspNetCore.Mvc;

namespace GWebsite.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class TransferController : GWebsiteControllerBase
    {
        private readonly ITransferAppService transferAppService;

        public TransferController(ITransferAppService transferAppService)
        {
            this.transferAppService = transferAppService;
        }

        [HttpGet]
        public PagedResultDto<TransferDto> GetTransfersByFilter(TransferFilter transferFilter)
        {
            return transferAppService.GetTransfers(transferFilter);
        }

        [HttpGet]
        public TransferInput GetTransferForEdit(int id)
        {
            return transferAppService.GetTransferForEdit(id);
        }

        [HttpPost]
        public void CreateOrEditTransfer([FromBody] TransferInput input)
        {
            transferAppService.CreateOrEditTransfer(input);
        }

        [HttpDelete("{id}")]
        public void DeleteTransfer(int id)
        {
            transferAppService.DeleteTransfer(id);
        }

        [HttpGet]
        public TransferForViewDto GetTransferForView(int id)
        {
            return transferAppService.GetTransferForView(id);
        }

        [HttpGet("{id}")]
        public void ApproveTransfer(int id)
        {
            transferAppService.ApproveTransfer(id);
        }
    }
}