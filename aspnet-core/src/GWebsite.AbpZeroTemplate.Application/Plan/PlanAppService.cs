﻿using Abp.Application.Services.Dto;
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
        private readonly IRepository<Department, int> departmentRepository;

        public PlanAppService(IRepository<Plan, int> planRepository, IRepository<Department, int> departmentRepository)
        {
            this.planRepository = planRepository;
            this.departmentRepository = departmentRepository;
        }
        public async Task<PagedResultDto<PlanDto>> GetPlanWithFilterAsync(PlanListInputDto input)
        {
            IQueryable<Plan> query = planRepository.GetAllIncluding(p=>p.Department).Where(p => p.Id.Equals(input.Id) || p.ImplementDate.Year.Equals(input.Year) || p.Status.Equals(input.Status) || p.UnitCode.Equals(input.UnitCode) || p.DepartmentId.Equals(input.DepartmentId));
            int totalCount = await query.CountAsync();
            if (totalCount == 0)
            {
                query = planRepository.GetAll();
                totalCount = await query.CountAsync();
            }
            List<Plan> items = await query.OrderBy(input.Sorting).PageBy(input).ToListAsync();
            return new PagedResultDto<PlanDto>(
            totalCount,
            null);
        }

        public async Task<IEnumerable<string>> GetAllDepartmentAsync()
        {
           var query = await this.departmentRepository.GetAllListAsync();
           return query.Select(p => p.Name);
        }


    }
}