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
        public int Index { get; set; }
        public string Name { get; set; }

        // ngày giao
        public DateTime DeliveryTime { get; set; }

        // ghi chú
        public string Note { get; set; }

        // hồ sơ thầu
        public int BiddingId { get; set; }
        public Bidding Bidding { get; set; }

        public float TotalValueOfContract { get; set; }
        public float TotalValueOfImplementation { get; set; }

        // bảo hành và bảo lãnh
        public int GuaranteeId { get; set; }
        public Guarantee Guarantee { get; set; }
        public int GaranteeContractId { get; set; }
        public GaranteeContract GaranteeContract { get; set; }

    }
}
