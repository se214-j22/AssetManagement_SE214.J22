using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Core.Models
{
    public class LoTaiSan:FullAuditModel 
    {
        public int SoLuong { get; set; }
        public int TongGiaTri { get; set; }
        public DateTime NgayNhap { get; set; }
    }
}
