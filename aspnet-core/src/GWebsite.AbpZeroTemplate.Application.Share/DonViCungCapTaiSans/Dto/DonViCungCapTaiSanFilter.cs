using GSoft.AbpZeroTemplate.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.DonViCungCapTaiSans.Dto
{
    public class DonViCungCapTaiSanFilter : PagedAndSortedInputDto
    {
        public string TenDonViCungCap { get; set; }
    }
}
