using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GWebsite.AbpZeroTemplate.Core.Models;

namespace GWebsite.AbpZeroTemplate.Core.Models
{
    public class TaiSan : FullAuditModel
    {
        public int? MaLo { get; set; }
        public string TenTaiSan { get; set; }
        public int? MaNhomTaiSan { get; set; }
        public string NguyenGia { get; set; }
        public int? TrangThai { get; set; }
        public int? GiaTriKhauHao { get; set; }
        public DateTime NgayNhap { get; set; }
        public string ThongTinMoTa { get; set; }
        public int DSSoSeri { get; set; }
     
    }
}
