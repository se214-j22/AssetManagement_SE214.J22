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
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Web.Core.SubPlans
{
    public class SubPlanAppService : GWebsiteAppServiceBase, ISubPlanAppService
    {
        private readonly IRepository<SubPlan, int> subPlanRepository;
        public SubPlanAppService(IRepository<SubPlan, int> subPlanRepository)
        {
            this.subPlanRepository = subPlanRepository;
        }

        public async Task<PagedResultDto<SubPlanDto>> GetSubPlanWithFilterAsync(SubPlanListInputDto input)
        {
            IQueryable<SubPlan> query = subPlanRepository.GetAllIncluding(p=>p.Product).Where(p => p.ProductId.Equals(input.productId) || p.Product.Name.Contains(input.Name) );
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

        //public async Task<SubPlanDto> UpdateSubPlanAsync(ProductTypeSavedDto productTypeSavedDto)
        //{
        //    SubPlan entity = await this.subPlanRepository.GetAllIncluding(p => p.).FirstOrDefaultAsync(item => item.Id == productTypeSavedDto.Id);
        //    this.ObjectMapper.Map(productTypeSavedDto, entity);
        //    entity = await this.subPlanRepository.UpdateAsync(entity);
        //    await this.CurrentUnitOfWork.SaveChangesAsync();
        //    return this.ObjectMapper.Map<SubPlanDto>(entity);
        //}
    }
}
