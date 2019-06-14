using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.SanPhams.Dto
{
    public class SanPhamReports : Entity<int>
    {
        public string MaSP { get; set; }
        public string TenSP { get; set; }
        public string NgayTao { get; set; }
        public string NgayCapNhat { get; set; }
        public string TrangThai { get; set; }
    }
}
