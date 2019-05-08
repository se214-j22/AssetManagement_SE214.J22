using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSoft.AbpZeroTemplate.Dto;
using GWebsite.AbpZeroTemplate.Core.Models;

namespace GWebsite.AbpZeroTemplate.Application.Share.PhiDuongBos
{
    public class PhiDuongBoFilter : PagedAndSortedInputDto
    {
        public string soXe { get; set; }
        public string ngayCapNhat { get; set; }
        public string ngayDongPhi { get; set; }
        public string congTyThuPhi { get; set; }
    }
}
