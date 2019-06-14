using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.PurchaseOrder.Dto
{
    public partial class PurchaseOrderDto : EntityDto<int>
    {
        public int BidUnitId { get; set; }
        public int ContractId { get; set; }
        public int OrderNumber { get; set; }
        public DateTime DateOfPayment { get; set; }
        public int Amount { get; set; }
        public int Paid { get; set; }
        public bool Done { get; set; }
        public string Note { get; set; }
    }
}
