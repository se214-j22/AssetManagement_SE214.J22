using Abp.Runtime.Validation;
using GSoft.AbpZeroTemplate.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.MenuClients.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.Plans.Dto
{
   public class PlanListInputDto : PagedAndSortedInputDto, IShouldNormalize
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public int Status { get; set; }
        public string UnitCode  { get; set; }
        public string DepartmentCode { get; set; }
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "Status";
            }
        }
    }
}
