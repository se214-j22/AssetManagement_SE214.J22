using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Core.Models
{
    public class LoaiTaiSan : FullAuditModel
    {
        public string TenNhomTaiSan { get; set; }
        public float TiLeHaoMon { get; set; }
        public float ThoiHanSuDung { get; set; }
    }
}
