using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Core.Models
{
    public class CTTS:FullAuditModel
    {
        public int MaTS { get; set; }
        public int MaLo { get; set; }
        public string SoSeri { get; set; }
        public int MaXuatTS { get; set; }
        public int MADC { get; set; }
        public int MATH { get; set; }
        public int MaSC { get; set; }
        public int MaTL { get; set; }
        public int MaDV { get; set; }
        public string TenDV { get; set; }
    }
}
