using System;
using System.Collections.Generic;
using System.Text;

namespace GSoft.AbpZeroTemplate.Organizations.Dto
{
    public class ProductsToOrganizationUnitInput
    {
        public List<int> ProductIds { get; set; }
        public long OrganizationUnitId { get; set; }
    }
}
