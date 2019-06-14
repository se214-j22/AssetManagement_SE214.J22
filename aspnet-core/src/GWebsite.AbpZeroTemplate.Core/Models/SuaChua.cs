using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Core.Models
{
    public class SuaChua:FullAuditModel 
    {
        public string MaTS { get; set; }
        public string TenTaiSan { get; set; }
        public DateTime NgayXuat { get; set; }
        public DateTime NgayDuKienSuaXong { get; set; }
        public int MaDVSuaChua { get; set; }
        public string TenDVSuaChua { get; set; }        
        public int MaNhanVienPT { get; set; }
        public string TenNhanVienPT { get; set; }
        public int MaNhanVienDX { get; set; }
        public string TenNhanVienDX { get; set; }
        public int MaDVDeXuat { get; set; }
        public string TenDVDeXuat { get; set; }
        public DateTime NgaySuaXong { get; set; }
        public bool ThayDoiCongNang { get; set; }
        public long ChiPhiduKien { get; set; }
        public string GhiChu { get; set; }
        public string NoiDungSuaChua { get; set; }
    }
}
