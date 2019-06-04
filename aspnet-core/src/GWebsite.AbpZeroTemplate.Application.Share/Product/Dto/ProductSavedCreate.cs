using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.Product.Dto
{
   public partial class ProductSavedCreate
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Address { get; set; }
        public string UnitPrice { get; set; }
        public string CalUnit { get; set; }
        public DateTime CreateDate { get; set; }
        public int Status { get; set; }
        public string Description { get; set; }
        public int? SupplierId { get; set; }
        public int? ProductTypeId { get; set; }
    }
}
