using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Core.Models
{
    /// <summary>
    /// Entity tài sản
    /// </summary>
    public class Asset : FullAuditModel
    {
        //Mã tài sản
        public string AssetId { get; set; }
        //Tên tài sản
        public string AssetName { get; set; }
        //Loại tài sản
        public int AssetType { get; set; }
        //Mã nhóm tài sản
        public string AssetGrouptId { get; set; }
        //Ngày nhập
        public string DateAdded { get; set; }
        //Mô tả
        public string Describe { get; set; }
        //Số tháng khấu hao
        public int MonthOfDepreciation { get; set; }
        //Nguyên giá
        public float OriginalPrice { get; set; }
        //Giá trị khấu hao
        public float DepreciationValue { get; set; }
        //Ngày hết hạn bảo hành
        public string WarrantyExpiryDate { get; set; }
        //Nhà cung cấp
        public string ProviderId { get; set; }
        //Ghi chú
        public string Note { get; set; }
        //trạng thái của tài sản
        public int Status { get; set; }
        //Trạng thái duyệt
        public bool StatusApproved { get; set; }
    }
}
