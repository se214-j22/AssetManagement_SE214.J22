using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Core.Models
{
	public class PhieuBaoDuong : FullAuditModel
	{
		public int TaiSanCoDinhId { get; set; }
		public string SoHoaDon { get; set; }
		public decimal ChiPhi { get; set; }
	}
}
