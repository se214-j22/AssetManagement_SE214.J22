using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Core.Models
{
    public class SuaChua:FullAuditModel
    {
        public int? MaNhanVien { get; set; }
        public int? MaTaiSan { get; set; }
        public int? ChiPhiDuKien { get; set; }
        public int? MaNhanVienDeXuat { get; set; }
        public String NoiDungSuaChua { get; set; }
        public String GhiChu { get; set; }
    }
}
