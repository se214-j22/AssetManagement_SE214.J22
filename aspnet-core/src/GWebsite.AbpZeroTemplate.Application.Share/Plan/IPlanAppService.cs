using Abp.Application.Services.Dto;
using GSoft.AbpZeroTemplate.Authorization.Users;
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
        Task<PagedResultDto<PlanDto>> GetPlanWithFilterAsync(PlanListInputDto input);
        Task<IEnumerable<string>> GetAllDepartmentAsync();
        Task<PlanDto> ApprovedPlanAsync(int id);
        Task<User> CurrentUserInfoAsync();
        Task<PlanDto> CreatePlanAsync(PlanSavedDto PlanSavedDto);
        Task<PlanDto> GetPlanForEditAsync(NullableIdDto input);
    }
}
