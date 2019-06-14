using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using GWebsite.AbpZeroTemplate.Core.Models;
namespace GWebsite.AbpZeroTemplate.Application.Share.XuatTaiSans.Dto
{

    /// <summary>
    /// <model cref="XuatTaiSan"></model>
    /// </summary>
    public class XuatTaiSanInput:Entity<int>
    {

        public string MaTaiSan { get; set; }
        public string TenTaiSan { get; set; }
        public int SoLuong { get; set; }
        public DateTime NgayXuat { get; set; }
        public int MaDonVi { get; set; }
        public string TenDonVi { get; set; }
        public int MaNhanVien { get; set; }
        public string TenNhanVien { get; set; }
    }
}

