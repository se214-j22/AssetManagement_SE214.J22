using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.BidProfile.Dto
{
    public partial class BidUnitAllDto
    {
        public ProductTitleDto Product { get; set; }
        public DateTime SubmitDate { get; set; }
        public string BeginCost { get; set; }
        public string Bank { get; set; }
        public string ProofNum { get; set; }
        public string Note { get; set; }
        public int Status { get; set; }

    }
}
