using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.PhieuBaoDuongs.Dto
{
    public class PhieuBaoDuongInput : Entity
    {
		public int TaiSanCoDinhId { get; set; }
		public string SoHoaDon { get; set; }
		public decimal ChiPhi { get; set; }
	}
}
