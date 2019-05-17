using GSoft.AbpZeroTemplate.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.HoaDonNhaps.Dto
{
    public class HoaDonNhapFilter : PagedAndSortedInputDto
    {
        public int? donViCungCapTaiSanId { get; set; }
        //public DateTime NgayNhan { get; set; }
        public string SoHoaDon { get; set; }
    }
}
