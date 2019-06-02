using GWebsite.AbpZeroTemplate.Application.Share.MenuClients.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.Plans.Dto
{
   public class PlanListInputDto : GetMenuClientInput
    {
        public string Id { get; set; }
        public string Year { get; set; }
        public string Status { get; set; }
        public string UnitCode  { get; set; }
        public string DepartmentCode { get; set; }
    }
}
