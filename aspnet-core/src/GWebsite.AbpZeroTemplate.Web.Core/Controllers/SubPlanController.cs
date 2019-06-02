using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.SubPlans;
using GWebsite.AbpZeroTemplate.Application.Share.SubPlans.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class SubPlanController : GWebsiteControllerBase
    {
        private readonly ISubPlanAppService _SubPlanAppService;
        public SubPlanController(ISubPlanAppService SubPlanAppService)
        {
            _SubPlanAppService = SubPlanAppService;
        }

        [HttpGet]
        public async Task<ListResultDto<SubPlanDto>> GetSubPlans(SubPlanListInputDto input)
        {
            return await this._SubPlanAppService.GetSubPlanWithFilterAsync(input);
        }



    }
}
