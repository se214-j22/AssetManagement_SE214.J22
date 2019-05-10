using Abp.Domain.Entities;
using Abp.Organizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Core.Models
{
    public class ProductOrganizationUnit : Entity<int>
    {
        public int ProductId { get; set; }
        public long OrganizationUnitId { get; set; }
        public Product Product { get; set; }
        public OrganizationUnit OrganizationUnit { get; set; }
    }
}
