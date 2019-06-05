using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Core.Models
{
    public class CTDonVi:FullAuditModel
    {
        public int MaDV { get; set; }
        public int MaTS { get; set; }
        public int SoLuong { get; set; }
        public string TenDonVi { get; set; }
        public string TenTaiSan { get; set; }
    }
}
