using Abp.Domain.Entities;

using GWebsite.AbpZeroTemplate.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.Plans.Dto
{
  public class PlanDto : Entity<int>
    {
        public DateTime ImplementDate { get; set; }
        public DateTime EffectiveDate { get; set; }
        public float TotalPrice { get; set; }
        public string UnitCode { get; set; }
        public Department Department { get; set; }
        public int Status { get; set; }
        public int CountChange { get; set; }
        public ICollection<SubPlan> SubPlans { get; set; }
    }

}
