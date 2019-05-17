using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.LoaiTaiSans.Dto
{
    public class LoaiTaiSanForViewDto : Entity<int>
    {
        public string TenNhomTaiSan { get; set; }
        public float TiLeHaoMon { get; set; }
        public float ThoiHanSuDung { get; set; }
    }
}
