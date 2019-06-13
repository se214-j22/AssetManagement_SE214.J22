using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Transfers.Dto;

namespace GWebsite.AbpZeroTemplate.Application.Share.Transfers
{
    public interface ITransferAppService
    {
        void CreateOrEditTransfer(TransferInput transferInput);
        TransferInput GetTransferForEdit(int id);
        void DeleteTransfer(int id);
        PagedResultDto<TransferDto> GetTransfers(TransferFilter input);
        TransferForViewDto GetTransferForView(int id);
        void ApproveTransfer(int id);
    }
}
