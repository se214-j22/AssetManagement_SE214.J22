using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Plans.Dto;
using GWebsite.AbpZeroTemplate.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.Plans
{
    public interface IPlanAppService
    {
        Task<PagedResultDto<Plan>> GetPlanWithFilterAsync(PlanListInputDto input);

    }
}
