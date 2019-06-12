using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using GWebsite.AbpZeroTemplate.Core.Models;
namespace GWebsite.AbpZeroTemplate.Application.Share.TaiSans.Dto
{

    /// <summary>
    /// <model cref="TaiSan"></model>
    /// </summary>
    public class TaiSanDto:Entity<int>
    {
        public string TenTs { get; set; }
        public string ThongTinMoTa { get; set; }
        public DateTime NgayNhap { get; set; }
        public int MaNhomTS { get; set; }
        public string TenNhomTS { get; set; }
        public int? MaLo { get; set; }
        public int NguyenGia { get; set; }
        public string DSSoseri { get; set; }
        public int GiaTriKhauHao { get; set; }
        public int SoThangKhauHao { get; set; }
        public float TyLeKhauHao { get; set; }
        public int SoThangBaoHanh { get; set; }
        public string TinhTrangKhauHao { get; set; }
        public int SoLuong { get; set; }
        public int SoLuongTon { get; set; }
    }
}
