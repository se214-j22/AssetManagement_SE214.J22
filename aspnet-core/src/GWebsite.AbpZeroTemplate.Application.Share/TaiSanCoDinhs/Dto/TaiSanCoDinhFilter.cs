using GSoft.AbpZeroTemplate.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.TaiSanCoDinhs.Dto
{
    public class TaiSanCoDinhFilter : PagedAndSortedInputDto
    {
        public string TenTaiSan { get; set; }
    }
}
