using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Core.Models
{
    public class ChiTietTaiSan:FullAuditModel
    {
        public int? MaTaiSan { get; set; }
        public int? MaLo { get; set; }
        public int? SoSeri { get; set; }
        public int? MaXuatTaiSan { get; set; }
        public int MaCapPhat { get; set; }
        public int MaThuHoi { get; set; }
        public int MaDieuChuyen { get; set; }
        public int MaTL { get; set; }
        public int MaSC { get; set; }
    }
}
