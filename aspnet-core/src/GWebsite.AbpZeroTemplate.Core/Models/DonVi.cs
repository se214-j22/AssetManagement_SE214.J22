using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GWebsite.AbpZeroTemplate.Core.Models;

namespace GWebsite.AbpZeroTemplate.Core.Models
{
    public class DonVi:FullAuditModel
    {
        public string TenDonVi { get; set; }
        public DateTime NgayTest { get; set; }
    }
}
