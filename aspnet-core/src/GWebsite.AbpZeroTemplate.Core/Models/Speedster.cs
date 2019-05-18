using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Core.Models
{
    public class Speedster : FullAuditModel
    {
        public int SpeedsterId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Info { get; set; }
    }
}
