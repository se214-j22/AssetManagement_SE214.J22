using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.DonViCungCapTaiSans.Dto
{
    public class DonViCungCapTaiSanInput : Entity<int>
    {
        public string TenDonViCungCap { get; set; }
        public string DiaChiDonViCungCap { get; set; }
    }
}
