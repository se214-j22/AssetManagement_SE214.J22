using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Core.Models
{
    public partial class PurchaseOrder : Entity<int>
    {
        public int BidUnitId { get; set; }
        public BidUnit BidUnit { get; set; }
        public int ContractId { get; set; }
        public Contract Contract { get; set; }
        public int OrderNumber { get; set; }
        public DateTime DateOfPayment { get; set; }
        public int Amount { get; set; }
        public int Paid { get; set; }
        public bool Done { get; set; }
        public string Note { get; set; }
    }
}
