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
        //tên tài sản
        public string Name { get; set; }
        //mã danh mục tài sản
        public int CateID { get; set; }
        //mã nhà cung cấp
        public int ProviderID { get; set; }
        //số serial tài sản
        public string SerialNumber { get; set; }
        //tình trạng tài sản
        public string Status { get; set; }
        //ngày mua tài sản
        public DateTime PurchaseDate { get; set; }
        //ngày kết thúc bảo hành
        public DateTime WarrantyEndDate { get; set; }
        //ngày thanh lý
        public DateTime LiquidationDate { get; set; }
        //giá trị đầu tiên(giá mua vào)
        public float PurchasePrice { get; set; }
        //tỉ lệ khấu hao hằng năm
        public float AnnualDepreciationRate { get; set; }
        //giá trị còn lại
        public float ResidualPrice { get; set; }
        //phòng cần theo dõi
        public string DepartmentsNeedFollow { get; set; }
        //tổng tiền bảo hành
        public float TotalMaintenanceCosts { get; set; }
        //tổng tiền sửa chữa
        public float TotalRepairCost { get; set; }
        //mô tả về tài sản
        public string Description { get; set; }
    }
}
