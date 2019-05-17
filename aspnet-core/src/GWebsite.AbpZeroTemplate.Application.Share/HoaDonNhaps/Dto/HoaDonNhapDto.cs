using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.HoaDonNhaps.Dto
{
    public class HoaDonNhapDto : Entity<int>
    {
        public int donViCungCapTaiSanId { get; set; }
        public DateTime NgayNhan { get; set; }
        public string SoHoaDon { get; set; }
        public decimal GiaMuaThucTe { get; set; }
        public decimal ChiPhiVanChuyen { get; set; }
        public decimal ChiPhiSuaChua { get; set; }
        public decimal ChiPhiNangCap { get; set; }
        public decimal ChiPhiLapDatChayThu { get; set; }
        public decimal Thue { get; set; }
        public decimal LePhi { get; set; }
        public decimal NguyenGiaTaiSan { get; set; }
    }
}
