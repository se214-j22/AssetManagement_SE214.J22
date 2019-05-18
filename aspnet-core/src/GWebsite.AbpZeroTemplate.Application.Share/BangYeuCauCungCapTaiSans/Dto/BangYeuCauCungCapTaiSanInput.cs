using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.BangYeuCauCungCapTaiSans.Dto
{
    public class BangYeuCauCungCapTaiSanInput : Entity<int>
    {
        public int PhongBanId { get; set; }
        public int SoLuong { get; set; }
        public DateTime NgayYeuCau { get; set; }
    }
}
