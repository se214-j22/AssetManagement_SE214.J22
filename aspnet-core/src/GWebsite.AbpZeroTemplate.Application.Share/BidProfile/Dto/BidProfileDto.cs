using Abp.Domain.Entities;
using GWebsite.AbpZeroTemplate.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.BidProfile.Dto
{
  public  class BidProfileDto : Entity<int>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string BidCatalog { get; set; }
        public string BidType { get; set; }
        public int ProjectId { get; set; }
        //public Project Project { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime StartReceivedDate { get; set; }
        public DateTime EndReceivedDate { get; set; }
        public float CautionMoney { get; set; }
        public string Note { get; set; }
        public int Status { get; set; }
        public int WinStatus { get; set; }
        public int Type { get; set; }
    }
}
