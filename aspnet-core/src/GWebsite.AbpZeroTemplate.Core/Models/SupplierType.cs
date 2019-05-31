using Abp.Domain.Entities;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GWebsite.AbpZeroTemplate.Core.Models
{
    public partial class SupplierType : Entity<int>
    {
        public string Code { get; set; }
        public string Name { get; set; }
<<<<<<< HEAD
        public string Code { get; set; }
        public string Note { get; set; }
        public int Status { get; set; }
        public ICollection<Supplier> Suppliers { get; set; }
        public SupplierType()
        {
            Suppliers = new Collection<Supplier>();
        }
=======
        public string Note { get; set; }
        public int Status { get; set; }
        public bool IsInCludeSupplier { get; set; }
>>>>>>> 1556a7986e8ce87e42bdd0c2c51c4419e5bcaab6
    }
}
