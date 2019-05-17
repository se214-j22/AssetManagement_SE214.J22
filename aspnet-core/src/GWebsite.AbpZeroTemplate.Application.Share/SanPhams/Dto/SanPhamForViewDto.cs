using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.SanPhams.Dto
{
    public class SanPhamForViewDto
    {
        public string MaSP { get; set; }
        public string TenSP { get; set; }
        public DateTime?NgayTao { get; set; }
        public DateTime?NgayCapNhat { get; set; }
        public string TrangThai { get; set; }
    }
}
