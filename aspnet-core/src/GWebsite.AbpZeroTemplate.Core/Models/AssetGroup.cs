using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Core.Models
{
    /// <summary>
    /// Entity Nhóm tài sản
    /// </summary>
    public class AssetGroup : FullAuditModel
    {
        //Mã nhóm tài sản
        public string AssetGrouptId { get; set; }
        //Tên nhóm tài sản
        public string AssetGroupName { get; set; }
        //Loại tài sản
        public int AssetType { get; set; }
        //Nhóm tài sản cha
        public string AssetGroupParentId { get; set; }
        //Số tháng khấu hao
        public int MonthOfDepreciation { get; set; }
        //Tỷ lệ khấu hao
        public float DepreciationRates { get; set; }
    }
}
