using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GWebsite.AbpZeroTemplate.Core.Models;

namespace GWebsite.AbpZeroTemplate.Core.Models
{
    public class NhomTaiSan : FullAuditModel
    {
        public string tenNhomTaiSan { get; set; }
        public string loaiTaiSan { get; set; }
        public int? soThangKhauHao { get; set; }
        public double tyLeKhauHao { get; set; }
    }
}
