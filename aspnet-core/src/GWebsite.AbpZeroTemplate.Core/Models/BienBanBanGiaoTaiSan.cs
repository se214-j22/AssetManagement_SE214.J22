using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Core.Models
{
	public class BienBanBanGiaoTaiSan : FullAuditModel
	{
		public int TaiSanCoDinhId { get; set; }
		public int PhongBanId { get; set; }
		public DateTime NgayNhan { get; set; }
	}
}
