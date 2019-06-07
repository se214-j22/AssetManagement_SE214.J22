using GSoft.AbpZeroTemplate.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.CapPhats.Dto
{
    public class CapPhatFilter : PagedAndSortedInputDto
    {
        public int? PhongBanId { get; set; }
    }
}
