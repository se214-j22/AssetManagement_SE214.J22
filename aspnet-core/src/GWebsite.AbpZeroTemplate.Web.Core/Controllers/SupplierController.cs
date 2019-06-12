using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using GWebsite.AbpZeroTemplate.Application.Share;
using GWebsite.AbpZeroTemplate.Application.Share.Bidding;
using GWebsite.AbpZeroTemplate.Application.Share.Bidding.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.MenuClients.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Product.Dto;
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
    public class SupplierController : GWebsiteControllerBase
    {

        private readonly ISupplierAppService _SupplierAppService;

        public SupplierController(ISupplierAppService SuppllierService)
        {
            this._SupplierAppService = SuppllierService;
        }

        //[HttpGet]
        //public async Task<ListResultDto<SupplierDto>> GetSupplierByProduct(GetMenuClientInput input, int productId = 0)
        //{
        //    return await this._SupplierAppService.GetSupplierByProductAsync(input, productId);
        //}

        [HttpGet]
        public async Task<IActionResult> GetSupplierByProductType(GetMenuClientInput input, string supplierType = null)
        {
            if (string.IsNullOrEmpty(supplierType))
            {
                return this.BadRequest();
            }
            return this.Ok(await this._SupplierAppService.GetSupplierByTypeAsync(input, supplierType));
        }

        //[HttpGet]
        //public async Task<IEnumerable<SupplierTypeDto>> GetSupplierTypes()
        //{
        //    return await this._SupplierAppService.GetSupplierTypesAsync();
        //}

        //[HttpGet]
        //public async Task<ListResultDto<SupplierDto>> GetAllBiddingPass(GetMenuClientInput input)
        //{
        //    return await this._SupplierAppService.GetAllBiddingPassAsync(input);
        //}

        [HttpGet]
        public async Task<SupplierDto> GetSupplierById(EntityDto<int> input)
        {
            return await this._SupplierAppService.GetSupplierByIdAsync(input);
        }

        [HttpGet]
        public async Task<PagedResultDto<FilterSupplierTypeResponeModel>> GetSupplierTypesWithFilter(
            int? skipCount, 
            int? maxResultCount,
            int? status,
            string code = "",
            string name = "")
        {
            if (!maxResultCount.HasValue || maxResultCount.Value < 1)
                maxResultCount = 10;
           

            if (!skipCount.HasValue || skipCount.Value < 0)
                skipCount = 0;

            if (!status.HasValue || status.Value < 1 || status.Value > 3)
                status = 3; //select all status

            if (string.IsNullOrEmpty(code))
                code = "";

            if (string.IsNullOrEmpty(name))
                name = "";

            return await this._SupplierAppService.GetSupplierTypesWithFilterAsync(skipCount.Value, maxResultCount.Value, code, name, status.Value);
        }


        [HttpGet]
        public async Task<PagedResultDto<SupplierDto>> GetSupplierWithFilterAsync(GetProductInput input)
        {
            return await this._SupplierAppService.GetSupplierWithFilterAsync(input);
        }
        //[HttpPost]
        //public async Task<BiddingProduct> CreateBidding([FromBody]  BiddingSaved BiddingSaved)
        //{
        //    return await this._SupplierAppService.BiddingProductAsync(BiddingSaved);
        //}

        //[HttpPut]
        //public async Task<BiddingProduct> ChangeOwnerBiddingProduct([FromBody]  BiddingSaved BiddingSaved)
        //{
        //    return await this._SupplierAppService.ChangeOwnerBiddingProductAsync(BiddingSaved);
        //}

        [HttpPost]
        public async Task<SupplierDto> CreateSupplier([FromBody]  SupplierSavedDto supplierSavedDto)
        {
            return await this._SupplierAppService.CreateSupplierAsync(supplierSavedDto);
        }
        [HttpPut]
        public async Task<SupplierDto> UpdateSupplier([FromBody]  SupplierSavedDto supplierSavedDto)
        {
            return await this._SupplierAppService.UpdateSupplierAsync(supplierSavedDto);
        }

        //[HttpPut]
        //public async Task<SupplierTypeDto> ToggleStatusSupplierCatalog([FromBody]  EntityDto<int> input)
        //{
        //    return await this._SupplierAppService.ToggleStatusSupplierCatalogAsync(input);
        //}

        [HttpDelete("{id}")]
        public async Task DeleteSupplierAsync(int id)
        {
            await this._SupplierAppService.DeleteSupplierAsync(new EntityDto<int>() { Id = id });
        }

        [HttpDelete("{id}")]
        public async Task DeleteSupplierCatalog(int id)
        {
            await _SupplierAppService.DeleteSupplierCatalogAsync(new EntityDto<int>() { Id = id });
        }

        [HttpPost]
        public async Task<SupplierTypeDto> CreateSupplierCatalog([FromBody]  SupplierTypeSavedDto supplierTypeSavedDto)
        {
            return await this._SupplierAppService.CreateSupplierCatalogAsync(supplierTypeSavedDto);
        }

        [HttpPut]
        public async Task<SupplierTypeDto> UpdateSupplierCatalog([FromBody]  SupplierTypeSavedDto supplierTypeSavedDto)
        {
            return await this._SupplierAppService.UpdateSupplierCatalogAsync(supplierTypeSavedDto);
        }

    }
}
