using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Core.Models
{
  public partial  class BidUnit : Entity<int>
    {
        public int BidProfileId { get; set; }
        public BidProfile BidProfile { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public DateTime	SubmitDate  { get; set; }
        public string 	BeginCost { get; set; }
        public string 	Bank { get; set; }
        public string ProofNum { get; set; }
        public string Note { get; set; }
        public int Status { get; set; }
    }
}
