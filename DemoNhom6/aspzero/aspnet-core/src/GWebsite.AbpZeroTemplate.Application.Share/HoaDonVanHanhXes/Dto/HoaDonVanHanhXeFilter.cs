using GSoft.AbpZeroTemplate.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.HoaDonVanHanhXes.Dto
{
   public class HoaDonVanHanhXeFilter :  PagedAndSortedInputDto
    {
        public string soXe { get; set; }
    }
}
