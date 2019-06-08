using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSoft.AbpZeroTemplate.Dto;
using GWebsite.AbpZeroTemplate.Core.Models;

namespace GWebsite.AbpZeroTemplate.Application.Share.ThongTinSuaChuas.DTO
{
    public class ThongTinSuaChuaFilter : PagedAndSortedInputDto
    {
        public string soXe { get; set; }
        public DateTime ngaySuaChua { get; set; }
        public DateTime chiPhiSuaChua { get; set; }
        public string trangThaiDuyet { get; set; }
    }
}
