using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Core.Models
{
    /// <summary>
    /// Chi tiết thanh lý
    /// </summary>
    public class LiquidationDetail : FullAuditModel
    {   
        // mã thanh lý
        public string LiquidationID { get; set; }
        // mã tài sản
        public string AssetID { get; set; }
        //giá tiền thanh lý
        public string LiquidationPrice { get; set; }
        //ghi chú
        public string Note { get; set; }
    }
}
