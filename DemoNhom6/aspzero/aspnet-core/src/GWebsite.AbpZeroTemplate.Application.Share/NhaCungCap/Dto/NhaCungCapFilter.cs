using GSoft.AbpZeroTemplate.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.NhaCungCap.Dto
{
   public class NhaCungCapFilter : PagedAndSortedInputDto
    {
        public string maCongTyBaoHiem { get; set; }
    }
}
