using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using GSoft.AbpZeroTemplate.Authorization.Users;
using GWebsite.AbpZeroTemplate.Application;
using GWebsite.AbpZeroTemplate.Application.Share.SubPlans;
using GWebsite.AbpZeroTemplate.Application.Share.SubPlans.Dto;
using GWebsite.AbpZeroTemplate.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Web.Core.SubPlans
{
    public class SubPlanAppService : GWebsiteAppServiceBase, ISubPlanAppService
    {
        private readonly IRepository<SubPlan, int> subPlanRepository;
        private readonly IRepository<Product, int> productRepository;
        public SubPlanAppService(IRepository<SubPlan, int> subPlanRepository, IRepository<Product, int> productRepository)
        {
            this.subPlanRepository = subPlanRepository;
            this.productRepository = productRepository;
        }

        public async Task<PagedResultDto<SubPlanDto>> GetSubPlanWithFilterAsync(SubPlanListInputDto input)
        {
            IQueryable<SubPlan> query = subPlanRepository.GetAllIncluding(p => p.Product).Where(p => p.ProductId.Equals(input.productId) || p.Product.Name.Contains(input.Name));
            int totalCount = await query.CountAsync();
            if (totalCount == 0)
            {
                query = subPlanRepository.GetAll();
                totalCount = await query.CountAsync();
            }
            List<SubPlan> items = await query.OrderBy(input.Sorting).PageBy(input).ToListAsync();
            return new PagedResultDto<SubPlanDto>(
            totalCount,
             items.Select(item => this.ObjectMapper.Map<SubPlanDto>(item)).ToList());
        }

        public async Task<SubPlanDto> UpdateSubPlanAsync(SubPlanSavedDto SubPlanSavedDto)
        {
            SubPlan entity = await this.subPlanRepository.GetAllIncluding(p => p.Product, p => p.Plan).FirstOrDefaultAsync(item => item.ProductId == SubPlanSavedDto.ProductId && item.PlanId == SubPlanSavedDto.PlanId);
            entity.Quantity = entity.Quantity > entity.ImplementQantity ? SubPlanSavedDto.Quantity : entity.Quantity;
            entity = await this.subPlanRepository.UpdateAsync(entity);
            await this.CurrentUnitOfWork.SaveChangesAsync();
            return this.ObjectMapper.Map<SubPlanDto>(entity);
        }

        public async Task<SubPlanDto> CreateSubPlanAsync(SubPlanSavedDto subPlanSavedDto)
        {
            SubPlan subPlan = ObjectMapper.Map<SubPlan>(subPlanSavedDto);
            Product product = this.productRepository.FirstOrDefault(p => p.Id == subPlanSavedDto.ProductId);
            subPlan.Totalprice = product.Price * subPlanSavedDto.Quantity;
            subPlan.ScheduleMonth = DateTime.Now.ToString("MMM");
            subPlan.ImplementQantity = 0;
            subPlan.ImplementPrice = 0;
            subPlan.PesidualQuantity = 0;
            subPlan.PesidualPrice = 0;
            await subPlanRepository.InsertAndGetIdAsync(subPlan);
            await CurrentUnitOfWork.SaveChangesAsync();
            return ObjectMapper.Map<SubPlanDto>(subPlan);
        }
    }
}
