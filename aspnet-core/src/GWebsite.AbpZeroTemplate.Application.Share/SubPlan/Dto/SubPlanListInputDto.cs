using Abp.Runtime.Validation;
using GSoft.AbpZeroTemplate.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.MenuClients.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.SubPlans.Dto
{
  public  class SubPlanListInputDto : PagedAndSortedInputDto, IShouldNormalize
    {

        public string Name { get; set; }
        public int ProductId { get; set; }
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "Name";
            }
        }
    }
}
