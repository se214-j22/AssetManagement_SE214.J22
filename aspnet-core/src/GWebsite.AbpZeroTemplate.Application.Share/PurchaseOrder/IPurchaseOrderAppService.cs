using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.PurchaseOrder.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.PurchaseOrder
{
    public interface IPurchaseOrderAppService
    {
        Task<PagedResultDto<PurchaseOrderDto>> ListPurchaseOrder(GetPurchaseOrderInput input);
        PurchaseOrderDto GetPurchaseOrderId(EntityDto<int> input);

        Task<PurchaseOrderDto> CreatePurchaseOrder(PurchaseOrderDto dto);
        Task<PurchaseOrderDto> UpdatePurchaseOrder(PurchaseOrderDto dto);
        Task DeletePurchaseOrder(EntityDto<int> input);
    }
}
