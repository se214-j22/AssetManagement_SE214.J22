using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Core.Models
{
    public class CapPhat : FullAuditModel
    {
        public PhongBan PhongBan { get; set; }
        public SanPham SanPham { get; set; }
        public int? PhongBanId { get; set; }
        public int? SanPhamId { get; set; }
        public DateTime NgayCap { get; set; }
    }
}
