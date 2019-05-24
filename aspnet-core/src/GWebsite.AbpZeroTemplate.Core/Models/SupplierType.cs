using Abp.Domain.Entities;

namespace GWebsite.AbpZeroTemplate.Core.Models
{
    public partial class SupplierType : Entity<int>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public int Status { get; set; }
        public bool IsInCludeSupplier { get; set; }
    }
}
