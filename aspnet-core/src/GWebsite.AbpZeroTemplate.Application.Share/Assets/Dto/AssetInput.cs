using Abp.Domain.Entities;
using GWebsite.AbpZeroTemplate.Core.Models;

namespace GWebsite.AbpZeroTemplate.Application.Share.Assets.Dto
{
    /// <summary>
    /// <model cref="Asset"></model>
    /// </summary>
    public class AssetInput : Entity<int>
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
        //trạng thái của sản phẩm
        public int Status { get; set; }
        //Trạng thái duyệt
        public bool StatusApproved { get; set; }
    }
}
