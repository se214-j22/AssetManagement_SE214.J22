using Abp.Application.Services.Dto;
using GSoft.AbpZeroTemplate.Authorization.Users;
using GWebsite.AbpZeroTemplate.Application.Share.Plans;
using GWebsite.AbpZeroTemplate.Application.Share.Plans.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Projects;
using GWebsite.AbpZeroTemplate.Application.Share.PurchaseOrder;
using GWebsite.AbpZeroTemplate.Application.Share.PurchaseOrder.Dto;
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
    class ProjectOrderController : GWebsiteControllerBase
    {
        private readonly IPurchaseOrderAppService purchaseOrderAppService;

        public ProjectOrderController(IPurchaseOrderAppService purchaseOrderAppService)
        {
            this.purchaseOrderAppService = purchaseOrderAppService;
        }

        [HttpGet]
        public async Task<PagedResultDto<PurchaseOrderDto>> GetPOs(GetPurchaseOrderInput input)
        {
            return await this.purchaseOrderAppService.ListPurchaseOrder(input);
        }

        [HttpPost]
        public async Task<PurchaseOrderDto> CreatePO([FromBody] PurchaseOrderDto dto)
        {
            return await this.purchaseOrderAppService.CreatePurchaseOrder(dto);
        }

        [HttpGet]
        public async Task<PurchaseOrderDto> GetPO(int id)
        {
            return this.purchaseOrderAppService.GetPurchaseOrderId(new EntityDto<int>() { Id = id });
        }

        [HttpPut]
        public async Task<PurchaseOrderDto> UpdatePO([FromBody] PurchaseOrderDto dto)
        {
            return await this.purchaseOrderAppService.CreatePurchaseOrder(dto);
        }

        [HttpDelete]
        public async Task DeletePO(int id)
        {
            await this.purchaseOrderAppService.DeletePurchaseOrder(new EntityDto<int>() { Id = id });
        }
    }
}
