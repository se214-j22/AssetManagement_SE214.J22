using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.CapPhats.Dto
{
    public class CapPhatForViewDto : Entity<int>
    {
        public int PhongBanId { get; set; }
        public int SanPhamId { get; set; }
        public DateTime NgayCap { get; set; }
    }
}
