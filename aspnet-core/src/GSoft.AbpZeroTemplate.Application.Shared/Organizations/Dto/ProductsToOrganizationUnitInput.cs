using System;
using System.Collections.Generic;
using System.Text;

namespace GSoft.AbpZeroTemplate.Organizations.Dto
{
    public class AssetsToOrganizationUnitInput
    {
        public List<int> AssetIds { get; set; }
        public long OrganizationUnitId { get; set; }
    }
}
