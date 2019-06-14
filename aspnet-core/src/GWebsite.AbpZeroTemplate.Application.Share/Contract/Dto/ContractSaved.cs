using Abp.Domain.Entities;
using GWebsite.AbpZeroTemplate.Application.Share.Bidding.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.BidProfile.Dto;
using GWebsite.AbpZeroTemplate.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.Contract.Dto
{
   public partial class ContractSavedDto : Entity<int>
    {
        public string Code { get; set; }
        public string Name { get; set; }

        public DateTime DeliveryTime { get; set; }

        public string Note { get; set; }

        public int BidProfileId { get; set; }

        public int SupplierId { get; set; }

        public float TotalValueOfContract { get; set; }
        public float TotalValueOfImplementation { get; set; }

        public int Status { get; set; }

        public int GuaranteeId { get; set; }
        public int GaranteeContractId { get; set; }
    }
}
