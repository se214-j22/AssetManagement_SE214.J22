using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Core.Models
{
    /// <summary>
    /// Entity Điều chuyển
    /// </summary>
    public class Transfer : FullAuditModel
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
