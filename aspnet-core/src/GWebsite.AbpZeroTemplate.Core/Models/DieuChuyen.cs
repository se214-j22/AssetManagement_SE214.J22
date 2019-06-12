using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Core.Models
{
    public class DieuChuyen:FullAuditModel
    {
        public int MaTaiSan { get; set; }
        public int SoLuong { get; set; }
        public string TenTaiSan { get; set; }
        public int MaNhanVienDC { get; set; }
        public string TenNhanVienDC { get; set; }
        public int MaDonVi { get; set; }
        public string TenDonVi { get; set; }
        public int MaNhanVienNhan { get; set; }
        public string TenNhanVienNhan { get; set; }
        public DateTime NgayDieuChuyen { get; set; }
        public string GhiChu { get; set; }
    }
}
