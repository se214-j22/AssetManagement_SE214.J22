using GWebsite.AbpZeroTemplate.Core.Models;

namespace GWebsite.AbpZeroTemplate.Application.Share.Repairs.Dto
{
    public class RepairForViewDto
    {
        //Mã tài sản
        public string AssetId { get; set; }
        //Ngày xuất
        public string ExportDate { get; set; }
        //Ngày dự kiến sửa xong
        public string ExpectedDateRepaired { get; set; }
        //Đơn vị dự kiến sửa chữa - select để chọn
        public string ExpectedRepairUnit { get; set; }
        //Người đề xuất
        public string Proposer { get; set; }
        //NV phụ trách
        public string StaffInCharge { get; set; }
        //Chi phí dự kiến
        public float ExpectedCost { get; set; }
        //Nội dung dự kiến
        public string ExpectedContent { get; set; }
        //Ghi chú dự kiến
        public string ExpectedNote { get; set; }
        //Ngày sửa xong
        public string DateRepaired { get; set; }
        //Đơn vị sửa chữa thực tế - select để chọn
        public int? RepairUnit { get; set; }
        //Chi phí thực tế
        public float? Cost { get; set; }
        //Thay đổi công năng - checked true false
        public bool IsChangeFunction { get; set; }
        //Nội dung
        public string Content { get; set; }
        //Ghi chú
        public string Note { get; set; }
        //Trạng thái sửa chữa
        public bool Status { get; set; }
        //Trạng thái duyệt
        public bool StatusApproved { get; set; }
    }
}
