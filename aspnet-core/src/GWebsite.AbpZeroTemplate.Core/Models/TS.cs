using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Core.Models
{
    public class TS:FullAuditModel
    {
        public string TenTs { get; set; }
        public string ThongTinMoTa { get; set; }
        public DateTime NgayNhap { get; set; }
        public int MaNhomTS { get; set; }
        public int? MaLo { get; set; }
        public int NguyenGia { get; set; }
        public string DSSoseri { get; set; }
    }
}
