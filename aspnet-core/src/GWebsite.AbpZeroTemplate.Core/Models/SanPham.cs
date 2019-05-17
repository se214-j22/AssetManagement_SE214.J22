using System;

namespace GWebsite.AbpZeroTemplate.Core.Models
{
    public class SanPham : FullAuditModel
    {
        public string MaSP { get; set; }
        public string TenSP { get; set; }
        public DateTime NgayTao { get; set; }
        public DateTime NgayCapNhat { get; set; }
        public string TrangThai { get; set; }
    }
}
