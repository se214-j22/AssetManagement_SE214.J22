using Abp.Application.Services.Dto;
using GSoft.AbpZeroTemplate.Authorization.Users;
using GWebsite.AbpZeroTemplate.Application.Share.Plans;
using GWebsite.AbpZeroTemplate.Application.Share.Plans.Dto;
using GWebsite.AbpZeroTemplate.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public  class PlanController : GWebsiteControllerBase
    {
        private readonly IPlanAppService _PlanAppService;
        public PlanController(IPlanAppService PlanAppService)
        {
            _PlanAppService = PlanAppService;
        }


        [HttpGet]
        public async Task<ListResultDto<PlanDto>> GetPlans(PlanListInputDto input)
        {
            return await this._PlanAppService.GetPlanWithFilterAsync(input);
        }

        [HttpGet]
        public async Task<IEnumerable<string>> GetAllDepartment()
        {
            return await this._PlanAppService.GetAllDepartmentAsync();
        }

        [HttpPut("status/{id}")]
        public async Task<PlanDto> ApprovedPlanAsync(int id)
        {
            return await this._PlanAppService.ApprovedPlanAsync(id);
        }
        [HttpGet]
        public async Task<User> CurrentUserInfo()
        {
            return await this._PlanAppService.CurrentUserInfoAsync();
        }

        [HttpPost]
        public async Task<PlanDto> CreatePlanAsync([FromBody] PlanSavedDto planSavedDto)
        {
            return await this._PlanAppService.CreatePlanAsync(planSavedDto);
        }
        [HttpGet]
        public async Task<PlanDto> GetPlanForEdit(int id)
        {
            return await _PlanAppService.GetPlanForEditAsync(new NullableIdDto() { Id = id });
        }
    }
}
