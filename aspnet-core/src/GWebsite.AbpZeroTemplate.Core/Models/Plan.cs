using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Core.Models
{
   public partial class Plan : Entity<int>
    {
        public DateTime ImplementDate { get; set; }
        public DateTime EffectiveDate { get; set; }
        public float TotalPrice { get; set; }
        public string UnitCode { get; set; }
        public string DepartmentCode { get; set; }
        public int Status { get; set; }
        public int CountChange { get; set; }
        public ICollection<SubPlan> SubPlans { get; set; }

        public Plan()
        {
            SubPlans = new Collection<SubPlan>();
        }
    }
}
