using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Core.Models
{
  public partial  class SubPlan : Entity<int>
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public float Totalprice { get; set; }
        public string ScheduleMonth  { get; set; }
        public int ImplementQantity { get; set; }
        public int Quantity { get; set; }
        public float ImplementPrice { get; set; }
        public int PesidualQuantity { get; set; }
        public float PesidualPrice { get; set; }
        public int PlanId { get; set; }
        public Plan Plan { get; set; }

    }
}
