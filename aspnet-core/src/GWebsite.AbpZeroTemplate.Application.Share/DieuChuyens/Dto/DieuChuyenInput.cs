using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using GWebsite.AbpZeroTemplate.Core.Models;
namespace GWebsite.AbpZeroTemplate.Application.Share.DieuChuyens.Dto
{

    /// <summary>
    /// <model cref="DieuChuyen"></model>
    /// </summary>
    public class DieuChuyenInput:Entity<int>
    {
        public int MaTaiSan { get; set; }
        public int SoLuong { get; set; }
        public string TenTaiSan { get; set; }
        public int MaNhanVienDC { get; set; }
        public string TenNhanVienDC { get; set; }
        public int MaDonVi { get; set; }
        public string TenDonVi { get; set; }
        public int MaNhanVienNhan { get; set; }
        public string TenNhanVienNhan { get; set; }
        public string GhiChu { get; set; }
        public  DateTime NgayDieuChuyen { get; set; }
    }
}

