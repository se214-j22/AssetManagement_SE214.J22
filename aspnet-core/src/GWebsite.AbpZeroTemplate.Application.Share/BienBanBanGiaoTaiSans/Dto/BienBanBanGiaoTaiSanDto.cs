using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.BienBanBanGiaoTaiSans.Dto
{
    public class BienBanBanGiaoTaiSanDto : Entity
    {
		public int TaiSanCoDinhId { get; set; }
		public int PhongBanId { get; set; }
		public DateTime NgayNhan { get; set; }
	}
}
