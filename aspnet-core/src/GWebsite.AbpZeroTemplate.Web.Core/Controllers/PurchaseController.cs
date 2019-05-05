using Abp.Application.Services.Dto;
using GSoft.AbpZeroTemplate.Authorization.Users;
using GWebsite.AbpZeroTemplate.Application.Share;
using GWebsite.AbpZeroTemplate.Application.Share.Purchases;
using GWebsite.AbpZeroTemplate.Application.Share.Purchases.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class PurchaseController : GWebsiteControllerBase
    {
        private readonly IPurchaseAppService _PurchaseAppService;

        public PurchaseController(IPurchaseAppService PurchaseAppService)
        {
            _PurchaseAppService = PurchaseAppService;
        }

        [HttpGet]
        public async Task<ListResultDto<PurchaseDto>> GetPurchases(int start = 0, int numberItem = 10)
        {
            return await _PurchaseAppService.GetPurchasesAsync(new Pagination() { Start = start, NumberItem = numberItem });
        }

        [HttpGet]
        public async Task<ListResultDto<PurchaseDto>> GetPurchasesBySearch(string name, int numberItem = 10, int start = 0)
        {
            return await _PurchaseAppService.GetPurchasesAsync(new GetPurchaseInput() { Name = name, NumberItem = numberItem, Start = start });
        }

        [HttpGet]
        public async Task<PurchaseDto> GetPurchaseForEdit(int id)
        {
            return await _PurchaseAppService.GetPurchaseForEditAsync(new NullableIdDto() {Id=id});
        }

        [HttpPost]
        public async Task<int> CreatePurchase([FromBody]  PurchaseSave input)
        {
            return await _PurchaseAppService.CreatePurchaseAsync(input);
        }

        [HttpPut]
        public async Task<PurchaseDto> UpdatePurchase([FromBody] PurchaseSave input)
        {
            return await _PurchaseAppService.UpdatePurchaseAsync(input);
        }

        [HttpDelete("{id}")]
        public async Task DeletePurchase(int id)
        {
            await _PurchaseAppService.DeletePurchaseAsync(new EntityDto<int>() { Id = id });
        }

        [HttpGet]
        public async Task<string> ApprovePurchase(EntityDto<int> input)
        {
            return await _PurchaseAppService.ApprovePurchaseAsync(input);
        }
    }
}
