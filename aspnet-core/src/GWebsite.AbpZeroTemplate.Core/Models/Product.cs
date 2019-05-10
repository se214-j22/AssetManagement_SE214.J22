using Abp.Organizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Core.Models
{
    public class Product : FullAuditModel
    {
        public string Name { get; set; }
    }
}
