
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using AutoMapper;
using GWebsite.AbpZeroTemplate.Application;
using GWebsite.AbpZeroTemplate.Application.Share.PurchaseOrder;
using GWebsite.AbpZeroTemplate.Application.Share.PurchaseOrder.Dto;
using GWebsite.AbpZeroTemplate.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Web.Core.ProjectOrder
{
    class PurchaseOrderAppService : GWebsiteAppServiceBase, IPurchaseOrderAppService
    {
        private readonly IRepository<PurchaseOrder> poRepo;
        private readonly IRepository<Contract> contractRepo;

        public PurchaseOrderAppService(IRepository<PurchaseOrder> poRepo, IRepository<Contract> contractRepo)
        {
            this.poRepo = poRepo;
            this.contractRepo = contractRepo;
        }

        public PurchaseOrderDto GetPurchaseOrderId(EntityDto<int> input)
        {
            var query = this.poRepo.GetAllIncluding(po => po.BidUnit, po => po.Contract).FirstOrDefault(po => po.Id == input.Id);

            return this.ObjectMapper.Map<PurchaseOrderDto>(query);
        }

        public async Task<PagedResultDto<PurchaseOrderDto>> ListPurchaseOrder(GetPurchaseOrderInput input)
        {
            var query = this.poRepo.GetAll()
                                   .Include(po => po.BidUnit)
                                   .ThenInclude(bu => bu.BidProfile)
                                   .Include(po => po.Contract)
                                   .ThenInclude(po => po.BidProfile)
                                   .AsQueryable();

            if (input.ContractId != 0)
            {
                query = query.Where(po => po.ContractId == input.ContractId);
            }
            if (input.BidProfileId != 0)
            {
                query = query.Where(po => po.BidUnit.BidProfileId == input.BidProfileId);
            }
            if (input.ProjectId != 0)
            {
                query = query.Where(po => po.Contract.BidProfile.ProjectId == input.ProjectId);
            }

            //return this.ObjectMapper.Map<PurchaseOrderDto>(query);
            int totalCount = await query.CountAsync();

            List<PurchaseOrder> items = await query.ToListAsync();
            return new PagedResultDto<PurchaseOrderDto>(
            totalCount,
            items.Select(item => this.ObjectMapper.Map<PurchaseOrderDto>(item)).ToList());
        }

        public async Task<PurchaseOrderDto> CreatePurchaseOrder(PurchaseOrderDto dto)
        {
            var po = this.ObjectMapper.Map<PurchaseOrder>(dto);
            await this.poRepo.InsertAndGetIdAsync(po);
            await this.CurrentUnitOfWork.SaveChangesAsync();
            return this.ObjectMapper.Map<PurchaseOrderDto>(po);
        }

        public async Task DeletePurchaseOrder(EntityDto<int> input)
        {
            await this.poRepo.DeleteAsync(input.Id);
            await this.CurrentUnitOfWork.SaveChangesAsync();
        }

        public async Task<PurchaseOrderDto> UpdatePurchaseOrder(PurchaseOrderDto dto)
        {
            var po = this.ObjectMapper.Map<PurchaseOrder>(dto);
            await this.poRepo.UpdateAsync(po);
            await this.CurrentUnitOfWork.SaveChangesAsync();
            return this.ObjectMapper.Map<PurchaseOrderDto>(po);
        }
    }
}
