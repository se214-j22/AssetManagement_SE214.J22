using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Core.Models
{
    /// <summary>
    /// Entity Xuất sử dụng
    /// </summary>
    public class UseAsset : FullAuditModel
    {
        //Mã tài sản
        public string AssetId { get; set; }
        //Mã đơn vị sử dụng
        public int UnitsUsedId { get; set; }
        //Mã người sử dụng
        public int UserId { get; set; }
        //Ngày xuất
        public string DateExport { get; set; }
        //Trạng thái duyệt
        public bool StatusApproved { get; set; }
    }
}
