using Abp.Domain.Entities;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GWebsite.AbpZeroTemplate.Core.Models
{
    public partial class ProductType : Entity<int>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public int Status { get; set; }
        public ICollection<Product> Products { get; set; }
        public ProductType()
        {
            Products = new Collection<Product>();
        }
    }
}
