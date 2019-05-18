using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Core.Models
{
    public class AssetLine : FullAuditModel
    {
        [StringLength(3, MinimumLength = 3)]
        public string Code { get; set; }
        public string Name { get; set; }
        public int AssetTypeID { get; set; }
        public int ManufacturerID { get; set; }
        public AssetType AssetType { get; set; }
        public Manufacturer Manufacturer { get; set; }
        //public ICollection<Asset> Assets { get; set; }
    
    }
}
