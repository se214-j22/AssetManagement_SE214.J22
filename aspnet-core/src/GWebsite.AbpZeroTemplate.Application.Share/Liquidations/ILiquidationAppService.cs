using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Liquidations.Dto;
using System.Collections.Generic;

namespace GWebsite.AbpZeroTemplate.Application.Share.Liquidations
{
    public interface ILiquidationAppService
    {
        void CreateOrEditLiquidation(LiquidationInput liquidationInput);
        LiquidationInput GetLiquidationForEdit(int id);
        void DeleteLiquidation(int id);
        PagedResultDto<LiquidationDto> GetLiquidations(LiquidationFilter input);
        LiquidationForViewDto GetLiquidationForView(int id);
        LiquidationForViewDto GetLiquidationByAssetID(string assetId);
        List<LiquidationDto> GetListLiquidationNoteApproved();
        void ApproveLiquidation(int id);
    }
}
