using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.SubPlans.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.SubPlans
{
   public interface ISubPlanAppService
    {
        Task<PagedResultDto<SubPlanDto>> GetSubPlanWithFilterAsync(SubPlanListInputDto input);
        Task<SubPlanDto> UpdateSubPlanAsync(SubPlanSavedDto SubPlanSavedDto);
        Task<SubPlanDto> CreateSubPlanAsync(SubPlanSavedDto subPlanSavedDto);
    }
}
