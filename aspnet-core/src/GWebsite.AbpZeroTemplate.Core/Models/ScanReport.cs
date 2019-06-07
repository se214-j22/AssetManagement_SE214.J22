using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Core.Models
{
    public class ScanReport: FullAuditModel
    {
        public string ScannedData { get; set; }
    }
}
