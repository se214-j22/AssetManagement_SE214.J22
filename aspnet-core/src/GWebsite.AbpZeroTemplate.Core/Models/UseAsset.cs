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
        public string UnitsUsedId { get; set; }
        //Mã người sử dụng
        public string UserId { get; set; }
        //Ngày xuất
        public string DateExport { get; set; }
        //Ngày bắt đầu khấu hao
        public string StartDateOfDepreciation { get; set; }
        //Ngày kết thúc khấu hao
        public string EndDateOfDepreciation { get; set; }
        //Giá trị khấu hao tháng đầu tiên
        public float DepreciationValueForTheFirstMonth { get; set; }
        //Trạng thái duyệt
        public bool StatusApproved { get; set; }
    }
}
