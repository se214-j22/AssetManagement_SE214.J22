using GSoft.AbpZeroTemplate.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.TaiSans.Dto
{
  public  class TaiSanFilter : PagedAndSortedInputDto
    {
        public string maTaiSan { get; set; }
    }
}
