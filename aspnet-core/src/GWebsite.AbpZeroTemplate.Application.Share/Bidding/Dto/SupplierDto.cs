using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.Bidding.Dto
{
   public class SupplierDto : Entity<int>
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string Phone { get; set; }
        public string Contact { get; set; }
        public string SupplierType { get; set; }
        public ICollection<ProductTitleDto> Products { get; set; }
    }
}
