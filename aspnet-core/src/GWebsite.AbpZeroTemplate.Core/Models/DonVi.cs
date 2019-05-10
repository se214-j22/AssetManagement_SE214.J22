using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Core.Models
{
    public class DonVi : FullAuditModel
    {
        public int? MaKhuVuc { get; set; }
        public int? MaChiNhanh { get; set; }
        public string TenDonVi { get; set; }
        public string DiaChi { get; set; }

    }
}
