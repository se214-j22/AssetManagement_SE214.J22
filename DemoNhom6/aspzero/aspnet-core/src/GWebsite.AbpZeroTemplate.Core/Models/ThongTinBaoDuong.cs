using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Core.Models
{
    public class ThongTinBaoDuong:FullAuditModel
    {
        public  string SoXe { get; set; }
        public  DateTime? NgayBaoDuong { get; set; }
        public  DateTime? NgayBaoDuongTiepTheo { get; set; }
        public  int? SoTienThanhToan { get; set; }
        public  string HangMucBaoDuong { get; set; }
        public  string DonViBaoDuong { get; set; }
        public  int? TrangThaiDuyet { get; set; }
    }
}
