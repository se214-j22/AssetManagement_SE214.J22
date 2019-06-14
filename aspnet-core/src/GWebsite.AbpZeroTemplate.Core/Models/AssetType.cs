using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Core.Models
{
    public class AssetType : FullAuditModel
    {
        [StringLength(3, MinimumLength = 3)]
        public string Code { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        //public ICollection<AssetLine> AssetLines { get; set; }
        public string Descriptions { get; set; }
        public float DepreciationRate { get; set; }
    }
}
