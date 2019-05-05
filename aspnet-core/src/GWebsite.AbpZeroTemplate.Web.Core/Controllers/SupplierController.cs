using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using GWebsite.AbpZeroTemplate.Application.Share;
using GWebsite.AbpZeroTemplate.Application.Share.Bidding;
using GWebsite.AbpZeroTemplate.Application.Share.Bidding.Dto;
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
    public  class SupplierController : GWebsiteControllerBase
    {

        private readonly ISupplierAppService _SupplierAppService;

        public SupplierController(ISupplierAppService SuppllierService)
        {
            _SupplierAppService = SuppllierService;
        }

        [HttpGet]
        public async Task<ListResultDto<SupplierDto>> GetSupplierByProduct(int start = 0, int numberItem = 10, int productId=0)
        {
            return await _SupplierAppService.GetSupplierByProductAsync(new Pagination() { Start = start, NumberItem = numberItem }, productId);
        }

        [HttpGet]
        public async Task<ListResultDto<SupplierDto>> GetAllBiddingPass()
        {
            return await _SupplierAppService.GetAllBiddingPassAsync();
        }

        [HttpGet]
        public async Task<SupplierDto> GetSupplierById(EntityDto<int> input)
        {
            return await _SupplierAppService.GetSupplierByIdAsync(input);
        }

        [HttpPost]
        public async Task<BiddingProduct> CreateBidding([FromBody]  BiddingSaved BiddingSaved)
        {
            return await _SupplierAppService.BiddingProductAsync(BiddingSaved);
        }

        [HttpPut]
        public async Task<BiddingProduct> ChangeOwnerBiddingProduct([FromBody]  BiddingSaved BiddingSaved)
        {
            return await _SupplierAppService.ChangeOwnerBiddingProductAsync(BiddingSaved);
        }

        [HttpPost]
        public async Task<SupplierDto> CreateSupplier([FromBody]  SupplierSavedDto supplierSavedDto)
        {
            return await _SupplierAppService.CreateSupplierAsync(supplierSavedDto);
        }
        [HttpPut]
        public async Task<SupplierDto> UpdateSupplier([FromBody]  SupplierSavedDto supplierSavedDto)
        {
            return await _SupplierAppService.UpdateSupplierAsync(supplierSavedDto);
        }
    }
}
