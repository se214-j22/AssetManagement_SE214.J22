using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using GWebsite.AbpZeroTemplate.Application;
using GWebsite.AbpZeroTemplate.Application.Share.Plans;
using GWebsite.AbpZeroTemplate.Application.Share.Plans.Dto;
using GWebsite.AbpZeroTemplate.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Web.Core.Plans
{
    public class PlanAppService : GWebsiteAppServiceBase, IPlanAppService
    {
        private readonly IRepository<Plan, int> planRepository;


        public PlanAppService(IRepository<Plan, int> productTypeRepository)
        {
            this.planRepository = productTypeRepository;
        }
        public async Task<PagedResultDto<Plan>> GetPlanWithFilterAsync(PlanListInputDto input)
        {
            IQueryable<Plan> query = planRepository.GetAll().Where(p => p.Id.Equals(input.Id) || p.ImplementDate.Year.Equals(input.Year) || p.Status.Equals(input.Status) || p.UnitCode.Equals(input.UnitCode) || p.DepartmentCode.Equals(input.DepartmentCode));
            int totalCount = await query.CountAsync();
            if (totalCount == 0)
            {
                query = planRepository.GetAll();
                totalCount = await query.CountAsync();
            }
            List<Plan> items = await query.OrderBy(input.Sorting).PageBy(input).ToListAsync();
            return new PagedResultDto<Plan>(
            totalCount,
            items.ToList());
        }
    }
}
