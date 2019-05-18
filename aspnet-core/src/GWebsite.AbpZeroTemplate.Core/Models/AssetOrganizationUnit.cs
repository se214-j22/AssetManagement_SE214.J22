using Abp.Domain.Entities;
using Abp.Organizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Core.Models
{
    public class AssetOrganizationUnit : Entity<int>
    {
        public int AssetId { get; set; }
        public long OrganizationUnitId { get; set; }
        public Asset Asset { get; set; }
        public OrganizationUnit OrganizationUnit { get; set; }
    }
}
