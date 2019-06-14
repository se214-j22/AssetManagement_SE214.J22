using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Core.Models
{
    /// <summary>
    /// Entity Thanh lý
    /// </summary>
    public class Liquidation : FullAuditModel
    {
        //Mã tài sản
        public string AssetID { get; set; }
        //Ngày thanh lý
        public string LiquidationDate { get; set; }
        //Đơn vị mua
        public int PurchaseUnit { get; set; }
        //Tình trạng tài sản/ công cụ
        public string AssetStatus { get; set; }
        //Hình thức thanh lý
        public int LiquidationForm { get; set; }
        //Giá tiền thanh lý
        public string LiquidationPrice { get; set; }
        //Ghi chú
        public string Note { get; set; }
        //Trạng thái duyệt
        public bool StatusApproved { get; set; }
    }
}
