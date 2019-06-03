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
        public ICollection<SubPlanSavedDto> SubPlans { get; set; }
        public PlanSavedDto()
        {
            SubPlans = new Collection<SubPlanSavedDto>();
           
        }
    }
}
