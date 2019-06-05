using Abp.Runtime.Validation;
using GSoft.AbpZeroTemplate.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.BidProfile.Dto
{
 public partial   class BidProfileImput : PagedAndSortedInputDto, IShouldNormalize
    {
        public string Code { get; set; }
        public DateTime ReceivedDate { get; set; }
      
        public string BidCatalog { get; set; }
        public string BidType { get; set; }
        public int Status { get; set; }
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "Code";
            }
        }
    }
}
