using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.LiquidationDetails;
using GWebsite.AbpZeroTemplate.Application.Share.LiquidationDetails.Dto;
using Microsoft.AspNetCore.Mvc;

namespace GWebsite.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class LiquidationDetailController : GWebsiteControllerBase
    {
        private readonly ILiquidationDetailAppService liquidationDetailAppService;

        public LiquidationDetailController(ILiquidationDetailAppService liquidationDetailAppService)
        {
            this.liquidationDetailAppService = liquidationDetailAppService;
        }

        [HttpGet]
        public PagedResultDto<LiquidationDetailDto> GetLiquidationDetailsByFilter(LiquidationDetailFilter liquidationDetailFilter)
        {
            return liquidationDetailAppService.GetLiquidationDetails(liquidationDetailFilter);
        }

        [HttpGet]
        public LiquidationDetailInput GetLiquidationDetailForEdit(int id)
        {
            return liquidationDetailAppService.GetLiquidationDetailForEdit(id);
        }

        [HttpPost]
        public void CreateOrEditLiquidationDetail([FromBody] LiquidationDetailInput input)
        {
            liquidationDetailAppService.CreateOrEditLiquidationDetail(input);
        }

        [HttpDelete("{id}")]
        public void DeleteLiquidationDetail(int id)
        {
            liquidationDetailAppService.DeleteLiquidationDetail(id);
        }

        [HttpGet]
        public LiquidationDetailForViewDto GetLiquidationDetailForView(int id)
        {
            return liquidationDetailAppService.GetLiquidationDetailForView(id);
        }
    }
}