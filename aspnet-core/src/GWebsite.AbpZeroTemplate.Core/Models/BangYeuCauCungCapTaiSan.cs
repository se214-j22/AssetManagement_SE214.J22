using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Core.Models
{
    public class BangYeuCauCungCapTaiSan : FullAuditModel
    {
        public PhongBan PhongBan { get; set; }
        public int? PhongBanId { get; set; }
        public int SoLuong { get; set; }
        public DateTime NgayYeuCau { get; set; }
    }
}
