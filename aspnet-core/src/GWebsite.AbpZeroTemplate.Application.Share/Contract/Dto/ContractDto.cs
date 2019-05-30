using Abp.Domain.Entities;
using GWebsite.AbpZeroTemplate.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.Contract.Dto
{
   public partial class ContractDto : Entity<int>
    {
        public int Index { get; set; }
        public string Name { get; set; }
        public BiddingDto Bidding { get; set; }
        public float TotalValueOfContract { get; set; }
        public float TotalValueOfImplementation { get; set; }
        public DateTime DeliveryTime { get; set; }
        public string Note { get; set; }
        public Guarantee Guarantee { get; set; }
        public GaranteeContract GaranteeContract { get; set; }
    }
}
