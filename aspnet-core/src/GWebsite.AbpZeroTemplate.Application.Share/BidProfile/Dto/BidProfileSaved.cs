using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.BidProfile.Dto
{
    public partial class BidProfileSaved : Entity<int>
    {
        public string Code { get; set; }
        public string BidCatalog { get; set; }
        public string BidType { get; set; }
        public int ProjectId { get; set; }
    }
}
