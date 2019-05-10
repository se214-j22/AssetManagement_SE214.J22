using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Core.Models
{
    public class NhomTaiSan:FullAuditModel
    {
        public string TenNhomTaiSan { get; set; }
        public string LoaiTaiSan { get; set; }
        public int SoThangKhauHao { get; set; }
        public float TyLeKhauHao { get; set; }
    }
}
