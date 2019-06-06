using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Core.Models
{
    public class TaiSanCoDinh : FullAuditModel
    {
        public int LoaiTaiSanId { get; set; }
        public int HoaDonNhapId { get; set; }
        public string TenTaiSan { get; set; }
        public string MoTa { get; set; }
        public bool TinhTrang { get; set; }
        public decimal GiaTriTaiSan { get; set; }
        public decimal HaoMonTaiSan { get; set; }
    }
}
