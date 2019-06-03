using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.Product.Dto
{
   public partial class ProductSavedDto : Entity<int>
    {
        public string Name { get; set; }
        public string UnitPrice { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
    }
}
