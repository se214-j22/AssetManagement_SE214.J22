using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Core.Models
{
    public class DonViCungCapTaiSan : FullAuditModel
    {
        public string TenDonViCungCap { get; set; }
        public string DiaChiDonViCungCap { get; set; }
    }
}
