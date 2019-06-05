using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Core.Models
{
    public class ChiNhanh:FullAuditModel
    {
        public string TenChiNhanh { get; set; }
        public int MaDonVi { get; set; }
        public string DiaChi { get; set; }
    }
}
