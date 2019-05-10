using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Core.Models
{
    /// <summary>
    /// Thanh Lý
    /// </summary>
    public class Liquidation : FullAuditModel
    {
        // tên nhà thanh lý
        public string LiquidatorName { get; set; }
        // số hợp đồng
        public string ContractCode { get; set; }
        // ngày thanh lý
        public DateTime Date { get; set; }
        // tổng tài sản thanh lý
        public string TotalAsset { get; set; }
        // tổng giá tiền thanh lý
        public string TotalPrice { get; set; }
    }
}
