using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Core.Models
{
    public class SuaChua:FullAuditModel 
    {
        public int MaNhanVienPT { get; set; }
        public string TenNhanVienpT { get; set; }
        public int MaNhanVienDX { get; set; }
        public string TenNhanVienDX { get; set; }
        public int MaDV { get; set; }
        public string TenDonVi { get; set; }
        public int MaTS { get; set; }
        public string TenTaiSan { get; set; }
        public int ChiPhiduKien { get; set; }
        public string GhiChu { get; set; }
        public string NoiDungSuaChua { get; set; }
    }
}
