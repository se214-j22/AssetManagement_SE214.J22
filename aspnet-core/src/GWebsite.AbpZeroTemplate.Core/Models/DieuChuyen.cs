using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Core.Models
{
    public class DieuChuyen:FullAuditModel
    {
        public string MaTaiSan { get; set; }
        public int SoLuong { get; set; }
        public string TenTaiSan { get; set; }
        public int MaDVDC { get; set; }
        public string TenDonViDC { get; set; }
        public int MaDVNhan { get; set; }
        public string TenDonViNhan { get; set; }
        public int MaNhanVienNhan { get; set; }
        public string TenNhanVienNhan { get; set; }
        public DateTime NgayDieuChuyen { get; set; }
        public string GhiChu { get; set; }
    }
}
