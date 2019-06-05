using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Core.Models
{
    public class ThuHoi:FullAuditModel
    {
        public int MaDV { get; set; }
        public string TenDonVi { get; set; }
        public int MaTS { get; set; }
        public string TenTaiSan { get; set; }
        public int SoLuongTh { get; set; }
        public DateTime NgayThuHoi { get; set; }
        public string LyDo { get; set; }
        public bool TrangThaiDuyet { get; set; }
        public string NoiDungTh { get; set; }
    }
}
