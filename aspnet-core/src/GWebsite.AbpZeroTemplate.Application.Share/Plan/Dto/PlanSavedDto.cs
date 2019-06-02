using Abp.Domain.Entities;
using GWebsite.AbpZeroTemplate.Application.Share.SubPlans.Dto;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.Plans.Dto
{
   public partial class PlanSavedDto : Entity<int>
    {
        public DateTime ImplementDate { get; set; }
        public DateTime EffectiveDate { get; set; }
        public float TotalPrice { get; set; }
        public string UnitCode { get; set; }
        public string DepartmentCode { get; set; }
        public int Status { get; set; }
        public int CountChange { get; set; }
        public ICollection<SubPlanSavedDto> SubPlans { get; set; }
        public PlanSavedDto()
        {
            SubPlans = new Collection<SubPlanSavedDto>();
            Status = 0;
            CountChange = 0;
            TotalPrice = 0;
            UnitCode = "HN";
            DepartmentCode = "IT";
        }
    }
}
