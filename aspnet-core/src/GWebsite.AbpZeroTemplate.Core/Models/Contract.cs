using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;

namespace GWebsite.AbpZeroTemplate.Core.Models
{
    public partial class Contract : Entity<int>
    {
        public string Code { get; set; }
        public string Name { get; set; }

        public DateTime DeliveryTime { get; set; }

        public string Note { get; set; }

        public int BidProfileId { get; set; }
        public BidProfile BidProfile { get; set; }

        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }

        public float TotalValueOfContract { get; set; }
        public float TotalValueOfImplementation { get; set; }

        public int Status { get; set; }

        public int GuaranteeId { get; set; }
        public Guarantee Guarantee { get; set; }
        public int GaranteeContractId { get; set; }
        public GaranteeContract GaranteeContract { get; set; }
    }
}
