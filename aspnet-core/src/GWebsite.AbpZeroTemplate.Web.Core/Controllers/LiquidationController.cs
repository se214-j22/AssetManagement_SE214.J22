using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Liquidations;
using GWebsite.AbpZeroTemplate.Application.Share.Liquidations.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace GWebsite.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class LiquidationController : GWebsiteControllerBase
    {
        private readonly ILiquidationAppService liquidationAppService;

        public LiquidationController(ILiquidationAppService liquidationAppService)
        {
            this.liquidationAppService = liquidationAppService;
        }

        [HttpGet]
        public PagedResultDto<LiquidationDto> GetLiquidationsByFilter(LiquidationFilter liquidationFilter)
        {
            return liquidationAppService.GetLiquidations(liquidationFilter);
        }

        [HttpGet]
        public LiquidationInput GetLiquidationForEdit(int id)
        {
            return liquidationAppService.GetLiquidationForEdit(id);
        }

        [HttpPost]
        public void CreateOrEditLiquidation([FromBody] LiquidationInput input)
        {
            liquidationAppService.CreateOrEditLiquidation(input);
        }

        [HttpDelete("{id}")]
        public void DeleteLiquidation(int id)
        {
            liquidationAppService.DeleteLiquidation(id);
        }

        [HttpGet]
        public LiquidationForViewDto GetLiquidationForView(int id)
        {
            return liquidationAppService.GetLiquidationForView(id);
        }

        [HttpGet]
        public LiquidationForViewDto GetLiquidationByAssetID(string assetId)
        {
            return this.liquidationAppService.GetLiquidationByAssetID(assetId);
        }

        [HttpGet("{id}")]
        public void ApproveLiquidation(int id)
        {
            liquidationAppService.ApproveLiquidation(id);
        }

        [HttpGet]
        public List<LiquidationDto> GetListLiquidationNoteApproved()
        {
            return this.liquidationAppService.GetListLiquidationNoteApproved();
        }
    }
}