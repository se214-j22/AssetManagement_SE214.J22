using Abp.Domain.Entities;
using System;

namespace GWebsite.AbpZeroTemplate.Application.Share.SanPhams.Dto
{
    public class SanPhamDto : Entity<int>
    {
        public string MaSP { get; set; }
        public string TenSP { get; set; }
        public DateTime?NgayTao { get; set; }
        public DateTime?NgayCapNhat { get; set; }
        public string TrangThai { get; set; }
    }
}
