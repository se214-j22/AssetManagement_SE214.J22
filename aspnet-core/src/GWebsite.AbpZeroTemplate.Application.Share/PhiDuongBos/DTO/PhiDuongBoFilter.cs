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
        public DateTime ngayCapNhat { get; set; }
        public DateTime ngayDongPhi { get; set; }
        public DateTime congTyThuPhi { get; set; }
        
    }
}
