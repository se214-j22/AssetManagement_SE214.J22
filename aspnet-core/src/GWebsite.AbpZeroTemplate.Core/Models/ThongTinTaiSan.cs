using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Core.Models
{
    public class ThongTinTaiSan:FullAuditModel
    {
        public string TenTs { get; set; }
        public string MaTS { get; set; }
        public string ThongTinMoTa { get; set; }
        public DateTime NgayNhap { get; set; }
        public int MaNhomTS { get; set; }
        public string TenNhomTS { get; set; }
        public int? MaLo { get; set; }
        public long NguyenGia { get; set; }
        public string Soseri { get; set; }
        public int GiaTriKhauHao { get; set; }
        public int SoThangKhauHao { get; set; }
        public float TyLeKhauHao { get; set; }
        public int SoThangBaoHanh { get; set; }
        public string TinhTrangKhauHao { get; set; }
        public string LoaiTS { get; set; }
        public int MaDV { get; set; }
        public string TenDV { get; set; }
        public string TinhTrang { get; set; }
    }
}
