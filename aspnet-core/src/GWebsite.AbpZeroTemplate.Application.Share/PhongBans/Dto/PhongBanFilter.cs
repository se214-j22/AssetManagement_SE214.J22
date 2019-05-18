using GSoft.AbpZeroTemplate.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.PhongBan.Dto
{
    public class PhongBanFilter : PagedAndSortedInputDto
    {
        public int? PhongBanId { get; set; }
        public string TenPhong { get; set; }
    }
}
