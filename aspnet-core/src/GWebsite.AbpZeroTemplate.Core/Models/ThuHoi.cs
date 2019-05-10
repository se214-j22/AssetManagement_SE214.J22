using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
namespace GWebsite.AbpZeroTemplate.Core.Models
{
    public class ThuHoi : FullAuditModel
    {
        public int? MaNhanVien { get; set; }
        public int? MaTaiSan { get; set; }
        public DateTime NgayThuHoi { get; set; }
        public int? SoLuongTH { get; set; }
        public string LyDo { get; set; }
        public int? TrangThaiDuyet { get; set; }
        public String NoidungThuhoi { get; set; }

    }
}
