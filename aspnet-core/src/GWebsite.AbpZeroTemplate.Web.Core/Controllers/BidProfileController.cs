using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share;
using GWebsite.AbpZeroTemplate.Application.Share.BidProfile;
using GWebsite.AbpZeroTemplate.Application.Share.BidProfile.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public  class BidProfileController : GWebsiteControllerBase
    {
        private readonly IBidProfileAppService _BidProfileAppService;
        public BidProfileController(IBidProfileAppService BidProfileAppService)
        {
            _BidProfileAppService = BidProfileAppService;
        }


        [HttpGet]
        public async Task<PagedResultDto<BidProfileDto>> GetBidProfiles(BidProfileImput input)
        {
            return await this._BidProfileAppService.GetBidProfileWithFilterAsync(input);
        }
        [HttpDelete]
        public async Task<IServiceResult> DeleteBidProfileAsync(int id)
        {
          return  await this._BidProfileAppService.DeleteBidProfileAsync(id);
        }

        [HttpPut]
        public async Task<BidProfileDto> UpdateBidProfileAsync([FromBody] BidProfileSaved dto)
        {
            return await this._BidProfileAppService.UpdateProductCatalogAsync(dto);
        }

        [HttpPost]
        public async Task<BidProfileDto> CreateBidProfileAsync([FromBody] BidProfileSaveForCreate dto)
        {
            return await this._BidProfileAppService.CreateProductCatalogAsync(dto);
        }
        [HttpGet]
        public async Task<BidProfileAllDto> GetBidProfileByIdAsync(int id)
        {
            return await this._BidProfileAppService.GetBidProfileByIdAsync(id);
        }

        [HttpPut]
        public async Task<BidProfileDto> ApprovalBidProfileAsync(int id)
        {
            return await this._BidProfileAppService.ApprovalBidProfileAsync(id);
        }
    }
}
