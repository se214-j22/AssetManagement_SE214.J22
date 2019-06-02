using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.SubPlans.Dto
{
   public partial class SubPlanSavedDto
    {
        public int PlanId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
