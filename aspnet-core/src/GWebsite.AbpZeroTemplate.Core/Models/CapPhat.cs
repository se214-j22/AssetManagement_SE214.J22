using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;

namespace GWebsite.AbpZeroTemplate.Core.Models
{
    public class CapPhat : FullAuditModel
    {
        public string TenDonVi { get; set; }
        public int? SoLuong { get; set; }
        public int? MaTaiSan { get; set; }
        public DateTime NgayCapPhat { get; set; }
    }
}
