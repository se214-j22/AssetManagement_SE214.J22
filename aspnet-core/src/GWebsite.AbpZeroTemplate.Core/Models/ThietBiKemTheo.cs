using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Core.Models
{
  public class ThietBiKemTheo : FullAuditModel
    {
        public string soXe { get; set; }
        public string thietBiKemTheo { get; set; }
        public int? soLuong { get; set; }
        public string dienGiai { get; set; }

    }
}
