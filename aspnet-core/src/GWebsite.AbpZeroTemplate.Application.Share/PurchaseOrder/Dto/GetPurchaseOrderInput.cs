using Abp.Runtime.Validation;
using GSoft.AbpZeroTemplate.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.PurchaseOrder.Dto
{
    public class GetPurchaseOrderInput : PagedAndSortedInputDto
    {
        public int ContractId { get; set; }
        public int ProjectId { get; set; }
        public int BidProfileId { get; set; }
    }
}
