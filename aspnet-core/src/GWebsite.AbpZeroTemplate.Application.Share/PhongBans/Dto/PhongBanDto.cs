using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.PhongBan.Dto
{
    public class PhongBanDto : Entity<int>
    {
        public string TenPhong { get; set; } 
    }
}
