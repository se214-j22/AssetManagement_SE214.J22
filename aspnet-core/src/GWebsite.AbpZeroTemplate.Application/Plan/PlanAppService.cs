using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using GSoft.AbpZeroTemplate.Authorization.Users;
using GWebsite.AbpZeroTemplate.Application;
using GWebsite.AbpZeroTemplate.Application.Share.Plans;
using GWebsite.AbpZeroTemplate.Application.Share.Plans.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.SubPlans.Dto;
using GWebsite.AbpZeroTemplate.Core.Authorization;
using GWebsite.AbpZeroTemplate.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Web.Core.Plans
{

    [AbpAuthorize(GWebsitePermissions.Pages_Administration_Plan)]
    public class PlanAppService : GWebsiteAppServiceBase, IPlanAppService
    {
        private readonly IRepository<Plan, int> planRepository;
        private readonly IRepository<Department, int> departmentRepository;
        private readonly IRepository<Product, int> productRepository;

        public PlanAppService(IRepository<Plan, int> planRepository, IRepository<Department, int> departmentRepository, IRepository<Product, int> productRepository)
        {
            this.planRepository = planRepository;
            this.departmentRepository = departmentRepository;
            this.productRepository = productRepository;
        }
        public async Task<PagedResultDto<PlanDto>> GetPlanWithFilterAsync(PlanListInputDto input)
        {
            IQueryable<Plan> query = planRepository.GetAll();
            if (input.Id != default(int))
            {
                query = query.Where(p => p.Id.Equals(input.Id));
            }
            if (input.Year != default(int))
            {
                query = query.Where(p => p.ImplementDate.Year.Equals(input.Year));
            }
            if (input.Status != 3)
            {
                query = query.Where(p => p.Status.Equals(input.Status));
            }
            if (input.UnitCode != null)
            {
                query = query.Where(p => p.UnitCode.Equals(input.UnitCode));
            }
            if (input.DepartmentCode != null)
            {
                query = query.Where(p => p.DepartmentCode.Equals(input.DepartmentCode));
            }
            int totalCount = await query.CountAsync();
            //if (totalCount == 0)
            //{
            //    query = planRepository.GetAll();
            //    totalCount = await query.CountAsync();
            //}
            List<Plan> items = await query.OrderBy(input.Sorting).PageBy(input).ToListAsync();
            return new PagedResultDto<PlanDto>(
            totalCount,
             items.Select(item => this.ObjectMapper.Map<PlanDto>(item)).ToList());
        }

        public async Task<IEnumerable<string>> GetAllDepartmentAsync()
        {
            var query = await this.departmentRepository.GetAllListAsync();
            return query.Select(p => p.Name);
        }

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_Plan_Edit)]
        public async Task<PlanDto> ApprovedPlanAsync(int id)
        {
            var query = await planRepository.GetAllIncluding(p => p.SubPlans).FirstOrDefaultAsync(item => item.Id == id);
            query.Status = query.Status == 1 ? 2 : 1;
            query = await planRepository.UpdateAsync(query);
            await CurrentUnitOfWork.SaveChangesAsync();
            return ObjectMapper.Map<PlanDto>(query);
        }

        public async Task<User> CurrentUserInfoAsync()
        {
            return await GetCurrentUserAsync();
        }

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_Plan_Create)]
        public async Task<PlanDto> CreatePlanAsync(PlanSavedDto PlanSavedDto)
        {
            //Plan productType = ObjectMapper.Map<Plan>(PlanSavedDto);
            User user = await GetCurrentUserAsync();
            Plan plan = new Plan();
            plan.UnitCode = user.UnitCode;
            plan.DepartmentCode = user.DepartmentCode;
            plan.EffectiveDate = DateTime.Now;
            plan.CountChange = 0;
            plan.ImplementDate = DateTime.Now;
            plan.TotalPrice = 0;
            plan.Status = 0;
            foreach (SubPlanSavedDto subplan in PlanSavedDto.SubPlans)
            {
                Product product = this.productRepository.FirstOrDefault(p => p.Id == subplan.ProductId);
                plan.SubPlans.Add(new SubPlan()
                {
                    Totalprice = product.UnitPrice * subplan.Quantity,
                    ScheduleMonth = DateTime.Now.ToString("MMM"),
                    ImplementQantity = 0,
                    ImplementPrice = 0,
                    PesidualQuantity = 0,
                    PesidualPrice = 0,
                    ProductId = subplan.ProductId,
                    Quantity = subplan.Quantity,
                    PlanId = plan.Id
                });
            };

            await planRepository.InsertAndGetIdAsync(plan);
        await CurrentUnitOfWork.SaveChangesAsync();
            return ObjectMapper.Map<PlanDto>(plan);
        }

        public async Task<PlanDto> GetPlanForEditAsync(NullableIdDto input)
        {
            var item = await planRepository.GetAllIncluding(p => p.SubPlans).FirstOrDefaultAsync(x => x.Id == input.Id);
            return ObjectMapper.Map<PlanDto>(item);
        }
    }
}
