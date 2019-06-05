using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Core.Models
{
    public class XuatTaiSan:FullAuditModel
    {
        public int MaTaiSan { get; set; }
        public string TenTaiSan { get; set; }
        public int SoLuong { get; set; }
        public DateTime NgayXuat { get; set; }
        public int MaDonVi { get; set; }
        public string TenDonVi { get; set; }
        public int MaNhanVien { get; set; }
        public string TenNhanVien { get; set; }
    }
}
