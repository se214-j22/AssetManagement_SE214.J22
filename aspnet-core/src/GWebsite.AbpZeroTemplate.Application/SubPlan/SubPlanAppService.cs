using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using GWebsite.AbpZeroTemplate.Application;
using GWebsite.AbpZeroTemplate.Application.Share.SubPlans;
using GWebsite.AbpZeroTemplate.Application.Share.SubPlans.Dto;
using GWebsite.AbpZeroTemplate.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
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
            IQueryable<SubPlan> query = subPlanRepository.GetAllIncluding(p => p.Product);
            if (input.ProductId != default(int))
            {
                query = query.Where(p => p.ProductId.Equals(input.ProductId));
            }
            if (input.Name != null)
            {
                query = query.Where(p => p.Product.Name.Contains(input.Name));
            }
            int totalCount = await query.CountAsync();
            //if (totalCount == 0)
            //{
            //    query = subPlanRepository.GetAll();
            //    totalCount = await query.CountAsync();
            //}
            var items = await query.PageBy(input).ToListAsync();
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
            subPlan.Totalprice = product.UnitPrice * subPlanSavedDto.Quantity;
            subPlan.ScheduleMonth = DateTime.Now.ToString("MMM");
            subPlan.ImplementQantity = 0;
            subPlan.ImplementPrice = 0;
            subPlan.PesidualQuantity = 0;
            subPlan.PesidualPrice = 0;
            await subPlanRepository.InsertAndGetIdAsync(subPlan);
            await CurrentUnitOfWork.SaveChangesAsync();
            return ObjectMapper.Map<SubPlanDto>(subPlan);
        }


        public async Task<SubPlanDto> GetSubPlanByIdAsync(EntityDto<int> input)
        {
            var entity = await this.subPlanRepository.GetAllIncluding(p => p.Product).FirstOrDefaultAsync(x => x.Id == input.Id);
            return this.ObjectMapper.Map<SubPlanDto>(entity);
        }
    }
}
