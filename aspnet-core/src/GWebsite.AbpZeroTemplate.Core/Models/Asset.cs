using Abp.Organizations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Core.Models
{
    public class Asset : FullAuditModel
    {
        [StringLength(15, MinimumLength = 15)]
        public string Code { get; set; }
        public int AssetLineId { get; set; }
        public AssetLine AssetLine { get; set; }
        public bool IsDamaged { get; set; }
        public string Note { get; set; }
        public long PONumber { get; set; }
        public int DepreciationMonths { get; set; }
        public float FullDepreciationPrice { get; set; }
        public float DepreciationValue { get; set; }
    }
}
