using GWebsite.AbpZeroTemplate.Core.Models;

namespace GWebsite.AbpZeroTemplate.Application.Share.Transfers.Dto
{
    /// <summary>
    /// <model cref="Transfer"></model>
    /// </summary>
    public class TransferForViewDto
    {
        //Mã tài sản
        public string AssetId { get; set; }
        //Ngày điều chuyển
        public string TransferDate { get; set; }
        //Đơn vị nhận
        public string ReceivingUnit { get; set; }
        //Người nhận
        public string Receiver { get; set; }
        //Ghi chú
        public string Note { get; set; }
        //Trạng thái duyệt
        public bool StatusApproved { get; set; }
    }
}