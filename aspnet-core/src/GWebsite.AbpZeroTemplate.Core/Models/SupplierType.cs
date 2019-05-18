using Abp.Domain.Entities;

namespace GWebsite.AbpZeroTemplate.Core.Models
{
    public partial class SupplierType : Entity<int>
    {
        public string Name { get; set; }
    }
}
