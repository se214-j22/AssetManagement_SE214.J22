using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.BienBanThanhLys.Dto
{
    public class BienBanThanhLyInput : Entity
    {
		public int TaiSanCoDinhId { get; set; }
		public DateTime NgayThanhLy { get; set; }
		public string TenDonViThanhLy { get; set; }
		public decimal ChiPhiThanhLy { get; set; }
		public decimal LoiNhuan { get; set; }
	}
}
