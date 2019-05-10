using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Core.Models
{
    public class NhanVien:FullAuditModel
    {
        public String TenNhanVien { get; set; }
        public int MaDV { get; set; }
    }
}
