using GSoft.AbpZeroTemplate.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.PhieuBaoDuongs.Dto
{
    public class PhieuBaoDuongFilter : PagedAndSortedInputDto
    {
        public string SoHoaDon { get; set; }
    }
}
