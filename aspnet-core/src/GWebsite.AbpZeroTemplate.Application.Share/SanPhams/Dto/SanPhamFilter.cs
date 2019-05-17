using GSoft.AbpZeroTemplate.Dto;
using System;

namespace GWebsite.AbpZeroTemplate.Application.Share.SanPhams.Dto
{
    public class SanPhamFilter : PagedAndSortedInputDto
    {
        public string MaSP { get; set; }
        public string TenSP { get; set; }
        public DateTime?NgayTao { get; set; }
        public DateTime?NgayCapNhat { get; set; }
        public string TrangThai { get; set; }
    }
}
