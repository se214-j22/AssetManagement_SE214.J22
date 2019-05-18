using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Core.Models
{
    public class HoaDonNhap : FullAuditModel
    {
        public DonViCungCapTaiSan donViCungCapTaiSan { get; set; }
		public int? donViCungCapTaiSanId { get; set; }
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

		//[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		//public decimal NguyenGiaTaiSan
		//{
		//	get { return (GiaMuaThucTe + ChiPhiVanChuyen + ChiPhiSuaChua + ChiPhiNangCap + ChiPhiLapDatChayThu + Thue + LePhi); }
		//	private set { }
		//}
	}
}
