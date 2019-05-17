using GSoft.AbpZeroTemplate.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.LoaiTaiSans.Dto
{
    public class LoaiTaiSanFilter : PagedAndSortedInputDto
    {
        public string TenNhomTaiSan { get; set; }
    }
}
